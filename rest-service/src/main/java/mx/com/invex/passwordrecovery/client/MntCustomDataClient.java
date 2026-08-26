package mx.com.invex.passwordrecovery.client;

import org.eclipse.microprofile.rest.client.inject.RegisterRestClient;

import jakarta.ws.rs.Consumes;
import jakarta.ws.rs.POST;
import jakarta.ws.rs.Path;
import jakarta.ws.rs.Produces;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;
import mx.com.invex.passwordrecovery.client.dto.MntCustomDataRequest;

@RegisterRestClient(configKey = "mnt-custom-data-api")
@Path("/mntCustomData")
public interface MntCustomDataClient {

    @POST
    @Consumes(MediaType.APPLICATION_JSON)
    @Produces(MediaType.APPLICATION_JSON)
    Response post(MntCustomDataRequest request);
}
