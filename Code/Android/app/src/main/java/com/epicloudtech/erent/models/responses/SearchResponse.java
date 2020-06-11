package com.epicloudtech.erent.models.responses;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

public class SearchResponse extends BasicResponse{

    @SerializedName("Data")
    @Expose
    private ArrayList<SearchItemResponse> Data;


    public ArrayList<SearchItemResponse> getData() {
        return Data;
    }

    public void setData(ArrayList<SearchItemResponse> data) {
        Data = data;
    }


}
