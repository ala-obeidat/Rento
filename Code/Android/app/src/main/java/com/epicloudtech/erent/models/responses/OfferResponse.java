package com.epicloudtech.erent.models.responses;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

public class OfferResponse extends BasicResponse {

    @SerializedName("Data")
    @Expose
    private ArrayList<OfferItemResponse> Data;


    public ArrayList<OfferItemResponse> getData() {
        return Data;
    }

    public void setData(ArrayList<OfferItemResponse> data) {
        Data = data;
    }

}
