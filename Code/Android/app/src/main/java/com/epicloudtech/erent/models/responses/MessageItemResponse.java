package com.epicloudtech.erent.models.responses;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class MessageItemResponse {

    @SerializedName("Id")
    @Expose
    private int Id;

    @SerializedName("Body")
    @Expose
    private String Body;

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }

    public String getBody() {
        return Body;
    }

    public void setBody(String body) {
        Body = body;
    }

    public String getCreateDate() {
        return CreateDate;
    }

    public void setCreateDate(String createDate) {
        CreateDate = createDate;
    }

    @SerializedName("CreateDate")
    @Expose
    private String CreateDate;

}
