package mx.com.invex.passwordrecovery;

import org.eclipse.microprofile.rest.client.inject.RestClient;
import org.jboss.logging.Logger;

import jakarta.enterprise.context.ApplicationScoped;
import jakarta.inject.Inject;
import jakarta.ws.rs.core.Response;
import mx.com.invex.passwordrecovery.audit.MntCustomDataAuditLogger;
import mx.com.invex.passwordrecovery.client.MntCustomDataClient;
import mx.com.invex.passwordrecovery.client.dto.MntCustmData;
import mx.com.invex.passwordrecovery.client.dto.MntCustomDataRequest;
import mx.com.invex.passwordrecovery.dto.PasswordRecoveryData;
import mx.com.invex.passwordrecovery.dto.PasswordRecoveryResponseBody;
import mx.com.invex.passwordrecovery.dto.PasswordRecoveryResponseEnvelope;
import mx.com.invex.passwordrecovery.dto.PasswordRecoveryResult;

@ApplicationScoped
public class PasswordRecoveryService {

    private static final Logger LOG = Logger.getLogger(PasswordRecoveryService.class);

    private static final String VALID_CUI = "abc7007";
    private static final String VALID_OLD_PASSWORD = "q1w2e3r4";
    private static final String VALID_NEW_PASSWORD = "a1s2d3f4";
    private static final String VALID_CONFIRMATION = "a1s2d3f4";

    private static final String VERSION = "1.0.0";
    private static final String MNT_CUSTOM_DATA_CODE = "B1";
    private static final String MNT_CUSTOM_DATA_VALUE = "10.00";
    private static final String MNT_CUSTOM_DATA_VALUE_TYPE = "STRING";

    @Inject
    @RestClient
    MntCustomDataClient mntCustomDataClient;

    @Inject
    MntCustomDataAuditLogger auditLogger;

    public Response recoverPassword(PasswordRecoveryData data) {
        boolean valid = VALID_CUI.equals(data.getCui())
                && VALID_OLD_PASSWORD.equals(data.getOldPassword())
                && VALID_NEW_PASSWORD.equals(data.getNewPassword())
                && VALID_CONFIRMATION.equals(data.getConfirmation());

        if (!valid) {
            return buildResponse(Response.Status.UNAUTHORIZED, "401", "wrong password", data.getCui());
        }

        notifyMntCustomData(data.getCui());
        auditLogger.logPasswordRecovery(data.getCui());
        return buildResponse(Response.Status.OK, "000", "success", data.getCui());
    }

    private void notifyMntCustomData(String cui) {
        try {
            MntCustmData payload = new MntCustmData(cui, MNT_CUSTOM_DATA_CODE, MNT_CUSTOM_DATA_VALUE,
                    MNT_CUSTOM_DATA_VALUE_TYPE);
            mntCustomDataClient.post(new MntCustomDataRequest(payload));
        } catch (RuntimeException e) {
            LOG.warnf(e, "Failed to notify mntCustomData for cui=%s", cui);
        }
    }

    private Response buildResponse(Response.Status status, String code, String statusMsg, String cui) {
        PasswordRecoveryResult result = new PasswordRecoveryResult(code, statusMsg, cui, VERSION);
        PasswordRecoveryResponseBody body = new PasswordRecoveryResponseBody(result);
        PasswordRecoveryResponseEnvelope envelope = new PasswordRecoveryResponseEnvelope(body);
        return Response.status(status).entity(envelope).build();
    }
}
