from fastapi import FastAPI

from app.models import (
    MntCustomDataRequest,
    MntCustomDataResponse,
    MntCustomDataResponseBody,
    MntCustomDataResult,
)

API_VERSION = "1.0.0"

app = FastAPI(title="mntCustomData API", version=API_VERSION)


@app.post(
    "/mntCustomData",
    response_model=MntCustomDataResponse,
    response_model_by_alias=True,
)
async def mnt_custom_data(payload: MntCustomDataRequest) -> MntCustomDataResponse:
    result = MntCustomDataResult(
        **{
            "@status": "000",
            "@statusMsg": "success",
            "@cardNbr": payload.mntCustmData.cardNbr,
            "@version": API_VERSION,
        }
    )
    return MntCustomDataResponse(
        mntCustomDataResponse=MntCustomDataResponseBody(mntCustomDataResult=result)
    )
