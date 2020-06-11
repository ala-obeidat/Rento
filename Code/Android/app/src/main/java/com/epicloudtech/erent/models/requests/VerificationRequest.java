package com.epicloudtech.erent.models.requests;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class VerificationRequest {

    public String getCode() {
        return Code;
    }

    public void setCode(String code) {
        Code = code;
    }

    @SerializedName("Code")
    @Expose
    private String Code;

}
