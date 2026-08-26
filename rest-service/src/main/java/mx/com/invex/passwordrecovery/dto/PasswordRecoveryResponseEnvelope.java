package mx.com.invex.passwordrecovery.dto;

public class PasswordRecoveryResponseEnvelope {

    private final PasswordRecoveryResponseBody passwordRecoveryResponse;

    public PasswordRecoveryResponseEnvelope(PasswordRecoveryResponseBody passwordRecoveryResponse) {
        this.passwordRecoveryResponse = passwordRecoveryResponse;
    }

    public PasswordRecoveryResponseBody getPasswordRecoveryResponse() {
        return passwordRecoveryResponse;
    }
}
