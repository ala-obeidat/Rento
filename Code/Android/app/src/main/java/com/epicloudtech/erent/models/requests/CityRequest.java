package com.epicloudtech.erent.models.requests;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class CityRequest extends BasicRequest {

    public String getData() {
        return Data;
    }

    public void setData(String data) {
        Data = data;
    }

    @SerializedName("Data")
    @Expose
    private String Data;
}
