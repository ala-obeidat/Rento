package com.epicloudtech.erent.models.responses;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

public class TypesResponse extends BasicResponse {

    @SerializedName("Data")
    @Expose
    private ArrayList<TypeItemResponse> types;

    public ArrayList<TypeItemResponse> getTypes() {
        return types;
    }

    public void getTypes(ArrayList<TypeItemResponse> types) {
        this.types = types;
    }

}
