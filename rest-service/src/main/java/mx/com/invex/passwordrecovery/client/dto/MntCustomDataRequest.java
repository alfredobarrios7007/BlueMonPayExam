package mx.com.invex.passwordrecovery.client.dto;

public class MntCustomDataRequest {

    private final MntCustmData mntCustmData;

    public MntCustomDataRequest(MntCustmData mntCustmData) {
        this.mntCustmData = mntCustmData;
    }

    public MntCustmData getMntCustmData() {
        return mntCustmData;
    }
}
