package com.epicloudtech.erent.models.requests;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class BookRequest {

    @SerializedName("CarId")
    @Expose
    private String CarId;

    @SerializedName("Location")
    @Expose

    private Location location;

    @SerializedName("CityId")
    @Expose
    private String CityId;

    @SerializedName("Price")
    @Expose
    private String Price;


    @SerializedName("From")
    @Expose
    private String From;

    @SerializedName("To")
    @Expose
    private String To;

    @SerializedName("Flag")
    @Expose
    private String Flag;

    public Location getLocation() {
        return location;
    }

    public void setLocation(Location location) {
        this.location = location;
    }


    public String getCarId() {
        return CarId;
    }

    public void setCarId(String carId) {
        CarId = carId;
    }

    public String getPrice() {
        return Price;
    }

    public void setPrice(String price) {
        Price = price;
    }

    public String getFrom() {
        return From;
    }

    public void setFrom(String from) {
        From = from;
    }

    public String getTo() {
        return To;
    }

    public void setTo(String to) {
        To = to;
    }

    public String getFlag() {
        return Flag;
    }

    public void setFlag(String flag) {
        Flag = flag;
    }

    public String getCityId() {
        return CityId;
    }

    public void setCityId(String cityId) {
        CityId = cityId;
    }

}
