package com.epicloudtech.erent.models.responses;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class LoginItemResponse {

    @SerializedName("Id")
    @Expose
    private int Id;

    @SerializedName("Type")
    @Expose
    private int Type;

    @SerializedName("Token")
    @Expose
    private String Token;

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }

    public int getType() {
        return Type;
    }

    public void setType(int type) {
        Type = type;
    }

    public String getToken() {
        return Token;
    }

    public void setToken(String token) {
        Token = token;
    }

}
