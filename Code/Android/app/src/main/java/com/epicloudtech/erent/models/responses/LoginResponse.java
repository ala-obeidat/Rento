package com.epicloudtech.erent.models.responses;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class LoginResponse extends BasicResponse {


    @SerializedName("Data")
    @Expose
    private LoginItemResponse Data;

    public LoginItemResponse getData() {
        return Data;
    }

    public void setData(LoginItemResponse data) {
        Data = data;
    }
}
