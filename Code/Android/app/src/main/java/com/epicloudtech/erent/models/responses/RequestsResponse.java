package com.epicloudtech.erent.models.responses;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

public class RequestsResponse extends BasicResponse {

    @SerializedName("Data")
    @Expose
    private ArrayList<RequestItemResponse> Data;


    public ArrayList<RequestItemResponse> getData() {
        return Data;
    }

    public void setData(ArrayList<RequestItemResponse> data) {
        Data = data;
    }

}
