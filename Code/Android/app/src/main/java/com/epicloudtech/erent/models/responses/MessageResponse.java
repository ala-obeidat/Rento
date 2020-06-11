package com.epicloudtech.erent.models.responses;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

public class MessageResponse extends BasicResponse {

    @SerializedName("Data")
    @Expose
    private ArrayList<MessageItemResponse> Data;


    public ArrayList<MessageItemResponse> getData() {
        return Data;
    }

    public void setData(ArrayList<MessageItemResponse> data) {
        Data = data;
    }
}
