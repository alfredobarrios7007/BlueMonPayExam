package mx.com.invex.passwordrecovery.dto;

import com.fasterxml.jackson.annotation.JsonProperty;

public class PasswordRecoveryResult {

    @JsonProperty("@status")
    private final String status;

    @JsonProperty("@statusMsg")
    private final String statusMsg;

    @JsonProperty("@cui")
    private final String cui;

    @JsonProperty("@version")
    private final String version;

    public PasswordRecoveryResult(String status, String statusMsg, String cui, String version) {
        this.status = status;
        this.statusMsg = statusMsg;
        this.cui = cui;
        this.version = version;
    }

    public String getStatus() {
        return status;
    }

    public String getStatusMsg() {
        return statusMsg;
    }

    public String getCui() {
        return cui;
    }

    public String getVersion() {
        return version;
    }
}
