# rest-service

A Quarkus REST service exposing a `passwordRecovery` operation. On a successful
credential check it notifies a downstream `mntCustomData` service and writes an
audit trail entry to a dedicated Log4j2-managed log file.

## Architecture

```
PasswordRecoveryResource        POST /passwordRecovery
        |
        v
PasswordRecoveryService         validates credentials, orchestrates the
        |         \             side effects below
        |          \
        v           v
MntCustomDataClient   MntCustomDataAuditLogger
(MicroProfile REST     (Log4j2 logger "mntCustomDataAudit",
 Client, JSON POST      independent from Quarkus/JBoss logging,
 to the mntCustomData   writes to a daily-rotated
 service)               mntCustomData yyyy-MM-dd.log file)
```

- **`PasswordRecoveryResource`** (`src/main/java/mx/com/invex/passwordrecovery`) — JAX-RS
  resource exposing `POST /passwordRecovery`. Deserializes the request body and
  delegates to the service. Request/response mapping (nested `data` object,
  hyphenated field names, `@`-prefixed response keys) is handled by Jackson via
  `quarkus-rest-jackson`.
- **`PasswordRecoveryService`** — business logic. Validates
  `cui` / `old-password` / `new-password` / `confirmation` against the expected
  values. On success it calls `mntCustomData` and writes the audit log entry,
  then returns HTTP 200; otherwise it returns HTTP 401. A failure to reach the
  downstream `mntCustomData` service is logged as a warning but does not fail
  the password recovery response, since the credential check already
  succeeded.
- **`client/MntCustomDataClient`** — MicroProfile REST Client (`quarkus-rest-client`)
  interface that POSTs the `mntCustmData` payload to the configured
  `mnt-custom-data-api` base URL (`quarkus.rest-client.mnt-custom-data-api.url`,
  see `application.properties`, overridable via the `MNT_CUSTOM_DATA_URL` env var).
- **`audit/MntCustomDataAuditLogger`** — writes one line per successful recovery
  to a Log4j2 logger named `mntCustomDataAudit`, configured in
  `src/main/resources/log4j2.xml` with the pattern `date time|B2|cui`. This is a
  plain Apache Log4j2 pipeline, separate from Quarkus's own JBoss-LogManager-based
  console/application logging, so the audit file only ever contains these
  entries. The appender uses a `DirectWriteRolloverStrategy`, so it writes
  straight to a file named after the current date — `mntCustomData yyyy-MM-dd.log`
  — and automatically starts a new file at midnight, keeping the last 30 daily
  files (older ones are deleted). The log directory is controlled by the
  `MNT_CUSTOM_DATA_LOG_DIR` environment variable (defaults to `logs`, relative
  to the working directory).

### Request / response contract

Request:

```json
{
  "data": {
    "cui": "abc7007",
    "old-password": "q1w2e3r4",
    "new-password": "a1s2d3f4",
    "confirmation": "a1s2d3f4"
  }
}
```

Success (HTTP 200) when `cui=abc7007`, `old-password=q1w2e3r4`,
`new-password=a1s2d3f4` and `confirmation=a1s2d3f4`:

```json
{
  "passwordRecoveryResponse": {
    "passwordRecoveryResult": {
      "@status": "000",
      "@statusMsg": "success",
      "@cui": "abc7007",
      "@version": "1.0.0"
    }
  }
}
```

Any other combination (HTTP 401):

```json
{
  "passwordRecoveryResponse": {
    "passwordRecoveryResult": {
      "@status": "401",
      "@statusMsg": "wrong password",
      "@cui": "abc7007",
      "@version": "1.0.0"
    }
  }
}
```

On success, the service also:

1. POSTs to the configured `mntCustomData` endpoint (`/mntCustomData`):
   ```json
   {
     "mntCustmData": {
       "cardNbr": "abc7007",
       "code": "B1",
       "value": "10.00",
       "valueType": "STRING"
     }
   }
   ```
2. Appends a line to that day's audit file, e.g. `mntCustomData 2026-08-25.log`:
   ```
   2026-08-25 12:34:56.789|B2|abc7007
   ```

## Prerequisites

- JDK matching `maven.compiler.release` in `pom.xml` (currently 25)
- Maven (or use the bundled `./mvnw` / `mvnw.cmd` wrapper)
- Docker + Docker Compose, if running containerized

## Running standalone (no Docker)

Dev mode (live reload):

```shell
./mvnw quarkus:dev
```

Or build and run the packaged jar:

```shell
./mvnw package
java -jar target/quarkus-app/quarkus-run.jar
```

By default:

- The service listens on `http://localhost:8080`.
- `mntCustomData` calls target `http://localhost:4501` (override with the
  `MNT_CUSTOM_DATA_URL` env var, e.g. `MNT_CUSTOM_DATA_URL=http://localhost:9000`).
- The audit log is written to `./logs/mntCustomData yyyy-MM-dd.log` (a new file
  each day), relative to wherever the JVM is started from (override the
  directory with `MNT_CUSTOM_DATA_LOG_DIR`).

Try it:

```shell
curl -X POST http://localhost:8080/passwordRecovery \
  -H "Content-Type: application/json" \
  -d '{"data":{"cui":"abc7007","old-password":"q1w2e3r4","new-password":"a1s2d3f4","confirmation":"a1s2d3f4"}}'
```

## Running with Docker Compose

The root `Dockerfile` is a multi-stage build: it compiles the application from
source with Maven, then packages it into a lightweight JVM runtime image
(`registry.access.redhat.com/ubi9/openjdk-25-runtime`). `docker-compose.yml`
builds and runs that image.

```shell
docker compose up --build
```

This will:

- Expose the API on `http://localhost:8080`.
- Point `mntCustomData` calls at `http://host.docker.internal:4501`, i.e. a
  service running on your **host machine** on port 4501 (the container's own
  `localhost` is not the host's `localhost`). `host.docker.internal` is
  resolved via the `extra_hosts` entry in `docker-compose.yml`, so this works
  on Docker Desktop (Windows/Mac) as well as Linux. If `mntCustomData` instead
  runs in another container, replace `MNT_CUSTOM_DATA_URL` in
  `docker-compose.yml` with that service's compose network name.
- **Persist the audit log to a local directory on the host: `./logs`**
  (mounted to `/deployments/logs` inside the container, via the `volumes:`
  entry in `docker-compose.yml`). After a successful call you'll find the
  audit trail at `./logs/mntCustomData yyyy-MM-dd.log` on the host (a new file
  per day, last 30 kept), surviving container restarts/recreation.

Stop it with:

```shell
docker compose down
```

### Standalone Docker images (no Compose)

`src/main/docker/` also contains the original Quarkus-generated Dockerfiles
(`Dockerfile.jvm`, `Dockerfile.legacy-jar`, `Dockerfile.native`,
`Dockerfile.native-micro`) for building images from a **pre-built** jar
(`./mvnw package` must be run first). These are kept for reference/native-image
builds; `docker compose up --build` uses the root `Dockerfile` instead, which
builds from source in one step.

## Running tests

```shell
./mvnw test
```

`PasswordRecoveryResourceTest` covers both the success and 401 paths.
