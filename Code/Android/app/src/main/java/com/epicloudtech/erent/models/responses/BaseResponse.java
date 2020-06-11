package com.epicloudtech.erent.models.responses;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class BaseResponse<T> extends BasicResponse{

    @SerializedName("Data")
    @Expose
    private T Data;

    public T getData() {
        return Data;
    }

    public void setData(T data) {
        Data = data;
    }
}
