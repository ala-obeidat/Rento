package com.epicloudtech.erent.models.responses;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class DetailsResponse extends BasicResponse {


    @SerializedName("Data")
    @Expose
    private DetailsItemResponse Data;

    public DetailsItemResponse getData() {
        return Data;
    }

    public void setData(DetailsItemResponse data) {
        Data = data;
    }
}
