package com.epicloudtech.erent.models.requests;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class BasicRequest {

    @SerializedName("ApplicationKey")
    @Expose
    private String ApplicationKey;

    @SerializedName("Language")
    @Expose
    private int Language;

    @SerializedName("Token")
    @Expose
    private String Token;


    public String getApplicationKey() {
        return ApplicationKey;
    }

    public void setApplicationKey(String applicationKey) {
        ApplicationKey = applicationKey;
    }

    public int getLanguage() {
        return Language;
    }

    public void setLanguage(int language) {
        Language = language;
    }

    public String getToken() {
        return Token;
    }

    public void setToken(String token) {
        Token = token;
    }
}
