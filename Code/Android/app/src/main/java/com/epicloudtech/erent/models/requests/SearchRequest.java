package com.epicloudtech.erent.models.requests;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class SearchRequest implements Serializable{

    @SerializedName("CityId")
    @Expose
    private String CityId;

    @SerializedName("Color")
    @Expose
    private String Color;

    @SerializedName("MinPrice")
    @Expose
    private String MinPrice;

    @SerializedName("MaxPrice")
    @Expose
    private String MaxPrice;

    @SerializedName("Ascending")
    @Expose
    private boolean Ascending;

    @SerializedName("Sort")
    @Expose
    private boolean Sort;

    @SerializedName("Model")
    @Expose
    private String Model;

    @SerializedName("Type")
    @Expose
    private String Type;

    @SerializedName("SubType")
    @Expose
    private String SubType;

    public String getCityId() {
        return CityId;
    }

    public void setCityId(String cityId) {
        CityId = cityId;
    }

    public String getColor() {
        return Color;
    }

    public void setColor(String color) {
        Color = color;
    }

    public String getMinPrice() {
        return MinPrice;
    }

    public void setMinPrice(String minPrice) {
        MinPrice = minPrice;
    }

    public String getMaxPrice() {
        return MaxPrice;
    }

    public void setMaxPrice(String maxPrice) {
        MaxPrice = maxPrice;
    }

    public boolean isAscending() {
        return Ascending;
    }

    public void setAscending(boolean ascending) {
        Ascending = ascending;
    }

    public boolean isSort() {
        return Sort;
    }

    public void setSort(boolean sort) {
        Sort = sort;
    }

    public String getModel() {
        return Model;
    }

    public void setModel(String model) {
        Model = model;
    }

    public String getType() {
        return Type;
    }

    public void setType(String type) {
        Type = type;
    }

    public String getSubType() {
        return SubType;
    }

    public void setSubType(String subType) {
        SubType = subType;
    }
}
