package com.epicloudtech.erent.models.requests;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class ResetPasswordRequest {

    @SerializedName("Code")
    @Expose
    private String Code;

    @SerializedName("NewPassword")
    @Expose
    private String NewPassword;

    @SerializedName("Customer")
    @Expose
    private boolean Customer;

    public String getCode() {
        return Code;
    }

    public void setCode(String code) {
        Code = code;
    }

    public String getNewPassword() {
        return NewPassword;
    }

    public void setNewPassword(String newPassword) {
        NewPassword = newPassword;
    }

    public boolean isCustomer() {
        return Customer;
    }

    public void setCustomer(boolean customer) {
        Customer = customer;
    }
}
