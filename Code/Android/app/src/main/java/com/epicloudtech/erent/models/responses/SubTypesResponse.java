package com.epicloudtech.erent.models.responses;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

public class SubTypesResponse extends BasicResponse {

    @SerializedName("Data")
    @Expose
    private ArrayList<SubTypesItemResponse> subTypes;

    public ArrayList<SubTypesItemResponse> getSubTypes() {
        return subTypes;
    }

    public void setSubTypes(ArrayList<SubTypesItemResponse> subTypes) {
        this.subTypes = subTypes;
    }

}
