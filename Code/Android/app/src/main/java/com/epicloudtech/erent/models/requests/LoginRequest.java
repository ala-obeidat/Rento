package com.epicloudtech.erent.models.requests;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class LoginRequest {

    @SerializedName("Username")
    @Expose
    private String Username;

    @SerializedName("Password")
    @Expose
    private String Password;

    @SerializedName("Customer")
    @Expose
    private boolean Customer;
    @SerializedName("NotificationType")
    @Expose
    private String NotificationType;

    public String isNotificationType() {
        return NotificationType;
    }

    public void setNotificationType(String notificationType) {
        NotificationType = notificationType;
    }

    public String getUsername() {
        return Username;
    }

    public void setUsername(String username) {
        Username = username;
    }

    public String getPassword() {
        return Password;
    }

    public void setPassword(String password) {
        Password = password;
    }

    public boolean isCustomer() {
        return Customer;
    }

    public void setCustomer(boolean customer) {
        Customer = customer;
    }
}
