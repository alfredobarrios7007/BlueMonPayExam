package mx.com.invex.passwordrecovery.dto;

public class PasswordRecoveryResponseBody {

    private final PasswordRecoveryResult passwordRecoveryResult;

    public PasswordRecoveryResponseBody(PasswordRecoveryResult passwordRecoveryResult) {
        this.passwordRecoveryResult = passwordRecoveryResult;
    }

    public PasswordRecoveryResult getPasswordRecoveryResult() {
        return passwordRecoveryResult;
    }
}
