package mx.com.invex.passwordrecovery;

import jakarta.inject.Inject;
import jakarta.ws.rs.Consumes;
import jakarta.ws.rs.POST;
import jakarta.ws.rs.Path;
import jakarta.ws.rs.Produces;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;
import mx.com.invex.passwordrecovery.dto.PasswordRecoveryRequest;

@Path("/passwordRecovery")
public class PasswordRecoveryResource {

    @Inject
    PasswordRecoveryService passwordRecoveryService;

    @POST
    @Consumes(MediaType.APPLICATION_JSON)
    @Produces(MediaType.APPLICATION_JSON)
    public Response passwordRecovery(PasswordRecoveryRequest request) {
        return passwordRecoveryService.recoverPassword(request.getData());
    }
}
