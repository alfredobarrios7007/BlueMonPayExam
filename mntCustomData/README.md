# mntCustomData API

A minimal Python **FastAPI** service that accepts a `mntCustmData` payload
and echoes back an acknowledgement envelope. Runs on port **4501**.

## Architecture

```
mntCustomData/
├── app/
│   ├── __init__.py
│   ├── main.py            FastAPI app + POST /mntCustomData route
│   └── models.py          Pydantic request/response models
├── requirements.txt       fastapi, uvicorn, pydantic
├── Dockerfile             python:3.12-slim, runs uvicorn on 0.0.0.0:4501
├── docker-compose.yml     Single-service compose file, maps host 4501 -> container 4501
└── README.md
```

- **app/main.py** defines the FastAPI application and the single
  `POST /mntCustomData` endpoint.
- **app/models.py** defines the Pydantic models:
  - `MntCustomDataRequest` / `MntCustmData` — validate the incoming body,
    requiring `cardNbr`, `code`, `value`, and `valueType` as strings.
  - `MntCustomDataResult` / `MntCustomDataResponseBody` /
    `MntCustomDataResponse` — build the response envelope. Since the
    response keys (`@status`, `@statusMsg`, `@cardNbr`, `@version`) aren't
    valid Python identifiers, each field uses a Pydantic `alias` and the
    route returns the model with `response_model_by_alias=True` so FastAPI
    serializes the `@`-prefixed keys as-is.
- **uvicorn** is the ASGI server that runs the app and listens on
  `0.0.0.0:4501`, both standalone and inside the container.

Request validation (missing/invalid fields, malformed JSON) is handled
automatically by FastAPI/Pydantic, returning an HTTP `422` with a JSON body
describing the offending field(s) — this is FastAPI's standard validation
error format and is not further customized.

### Request

`POST /mntCustomData`

```json
{
    "mntCustmData": {
        "cardNbr": "1234567890",
        "code": "ABC",
        "value": "10.00",
        "valueType": "AMOUNT"
    }
}
```

### Response

```json
{
    "mntCustomDataResponse": {
        "mntCustomDataResult": {
            "@status": "000",
            "@statusMsg": "success",
            "@cardNbr": "1234567890",
            "@version": "1.0.0"
        }
    }
}
```

## Running with Docker Compose

Requires Docker and Docker Compose.

```bash
docker compose up --build
```

The API is then available at `http://localhost:4501/mntCustomData`.
Interactive OpenAPI docs are available at `http://localhost:4501/docs`.

Stop it with:

```bash
docker compose down
```

## Running standalone (no Docker)

Requires Python 3.10+.

```bash
python -m venv .venv
.venv\Scripts\activate        # Windows
# source .venv/bin/activate   # macOS/Linux

pip install -r requirements.txt

uvicorn app.main:app --host 0.0.0.0 --port 4501
```

The API is then available at `http://localhost:4501/mntCustomData`.

## Testing the endpoint

```bash
curl -X POST http://localhost:4501/mntCustomData \
  -H "Content-Type: application/json" \
  -d '{
        "mntCustmData": {
          "cardNbr": "1234567890",
          "code": "ABC",
          "value": "10.00",
          "valueType": "AMOUNT"
        }
      }'
```
