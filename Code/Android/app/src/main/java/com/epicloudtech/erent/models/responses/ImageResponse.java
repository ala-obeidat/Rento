package com.epicloudtech.erent.models.responses;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class ImageResponse extends BasicResponse {

    @SerializedName("Data")
    @Expose
    private ImageItemResponse Data;

    public ImageItemResponse getData() {
        return Data;
    }

    public void setData(ImageItemResponse data) {
        Data = data;
    }
}
