package com.epicloudtech.erent.models.responses;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class ImageItemResponse {

    public String getContent() {
        return Content;
    }

    public void setContent(String content) {
        Content = content;
    }

    @SerializedName("Content")
    @Expose
    private String  Content;

}
