package com.epicloudtech.erent.models.requests;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class FcmRequest {

    @SerializedName("LoginToken")
    @Expose
    private String LoginToken;

    @SerializedName("NotificationToken")
    @Expose
    private String NotificationToken;

    public String getLoginToken() {
        return LoginToken;
    }

    public void setLoginToken(String loginToken) {
        LoginToken = loginToken;
    }

    public String getNotificationToken() {
        return NotificationToken;
    }

    public void setNotificationToken(String notificationToken) {
        NotificationToken = notificationToken;
    }

}
