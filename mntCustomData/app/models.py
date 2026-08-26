from pydantic import BaseModel, ConfigDict, Field


class MntCustmData(BaseModel):
    cardNbr: str
    code: str
    value: str
    valueType: str


class MntCustomDataRequest(BaseModel):
    mntCustmData: MntCustmData


class MntCustomDataResult(BaseModel):
    model_config = ConfigDict(populate_by_name=True)

    status: str = Field(alias="@status")
    statusMsg: str = Field(alias="@statusMsg")
    cardNbr: str = Field(alias="@cardNbr")
    version: str = Field(alias="@version")


class MntCustomDataResponseBody(BaseModel):
    mntCustomDataResult: MntCustomDataResult


class MntCustomDataResponse(BaseModel):
    mntCustomDataResponse: MntCustomDataResponseBody
