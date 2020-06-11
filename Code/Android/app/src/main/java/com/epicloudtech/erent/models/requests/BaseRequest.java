package com.epicloudtech.erent.models.requests;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class BaseRequest<T>  extends BasicRequest{

    @SerializedName("Data")
    @Expose
    private T Data;

    public T getData() {
        return Data;
    }

    public void setData(T data) {
        this.Data = data;
    }
}
