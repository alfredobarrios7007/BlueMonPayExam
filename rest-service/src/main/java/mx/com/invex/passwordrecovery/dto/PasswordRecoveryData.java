package mx.com.invex.passwordrecovery.dto;

import com.fasterxml.jackson.annotation.JsonProperty;

public class PasswordRecoveryData {

    private String cui;

    @JsonProperty("old-password")
    private String oldPassword;

    @JsonProperty("new-password")
    private String newPassword;

    private String confirmation;

    public String getCui() {
        return cui;
    }

    public void setCui(String cui) {
        this.cui = cui;
    }

    public String getOldPassword() {
        return oldPassword;
    }

    public void setOldPassword(String oldPassword) {
        this.oldPassword = oldPassword;
    }

    public String getNewPassword() {
        return newPassword;
    }

    public void setNewPassword(String newPassword) {
        this.newPassword = newPassword;
    }

    public String getConfirmation() {
        return confirmation;
    }

    public void setConfirmation(String confirmation) {
        this.confirmation = confirmation;
    }
}
