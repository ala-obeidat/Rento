package com.epicloudtech.erent.models.requests;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class CloseBookingRequest {

    @SerializedName("CheckoutId")
    @Expose
    private String CheckoutId;

    @SerializedName("Star")
    @Expose
    private String Star;

    @SerializedName("Comment")
    @Expose
    private String Comment;

    @SerializedName("Flag")
    @Expose
    private String Flag;

    public String getCheckoutId() {
        return CheckoutId;
    }

    public void setCheckoutId(String checkoutId) {
        CheckoutId = checkoutId;
    }

    public String getStar() {
        return Star;
    }

    public void setStar(String star) {
        Star = star;
    }

    public String getComment() {
        return Comment;
    }

    public void setComment(String comment) {
        Comment = comment;
    }

    public String getFlag() {
        return Flag;
    }

    public void setFlag(String flag) {
        Flag = flag;
    }

}
