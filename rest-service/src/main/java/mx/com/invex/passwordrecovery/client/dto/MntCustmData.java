package mx.com.invex.passwordrecovery.client.dto;

public class MntCustmData {

    private final String cardNbr;
    private final String code;
    private final String value;
    private final String valueType;

    public MntCustmData(String cardNbr, String code, String value, String valueType) {
        this.cardNbr = cardNbr;
        this.code = code;
        this.value = value;
        this.valueType = valueType;
    }

    public String getCardNbr() {
        return cardNbr;
    }

    public String getCode() {
        return code;
    }

    public String getValue() {
        return value;
    }

    public String getValueType() {
        return valueType;
    }
}
