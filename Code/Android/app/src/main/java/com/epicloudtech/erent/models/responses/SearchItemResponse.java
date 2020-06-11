package com.epicloudtech.erent.models.responses;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class SearchItemResponse {


    @SerializedName("Status")
    @Expose
    private String Status;

    @SerializedName("Color")
    @Expose
    private String Color;

    @SerializedName("DayCost")
    @Expose
    private String DayCost;

    @SerializedName("OfficeMobile")
    @Expose
    private String OfficeMobile;

    @SerializedName("Type")
    @Expose
    private String Type;

    public String getFavourite() {
        return favourite;
    }

    public void setFavourite(String favourite) {
        this.favourite = favourite;
    }

    private String favourite;

    @SerializedName("TypeNameAr")
    @Expose
    private String TypeNameAr;

    @SerializedName("TypeNameEn")
    @Expose
    private String TypeNameEn;

    @SerializedName("OfficeName")
    @Expose
    private String OfficeName;

    @SerializedName("SubType")
    @Expose
    private String SubType;

    @SerializedName("SubTypeNameAr")
    @Expose
    private String SubTypeNameAr;

    @SerializedName("SubTypeNameEn")
    @Expose
    private String SubTypeNameEn;

    @SerializedName("Model")
    @Expose
    private String Model;

    @SerializedName("Action")
    @Expose
    private String Action;

    @SerializedName("Price")
    @Expose
    private String Price;

    @SerializedName("Id")
    @Expose
    private String Id;

    public String getStatus() {
        return Status;
    }

    public void setStatus(String status) {
        Status = status;
    }

    public String getColor() {
        return Color;
    }

    public void setColor(String color) {
        Color = color;
    }

    public String getDayCost() {
        return DayCost;
    }

    public void setDayCost(String dayCost) {
        DayCost = dayCost;
    }

    public String getOfficeMobile() {
        return OfficeMobile;
    }

    public void setOfficeMobile(String officeMobile) {
        OfficeMobile = officeMobile;
    }

    public String getType() {
        return Type;
    }

    public void setType(String type) {
        Type = type;
    }

    public String getTypeNameAr() {
        return TypeNameAr;
    }

    public void setTypeNameAr(String typeNameAr) {
        TypeNameAr = typeNameAr;
    }

    public String getTypeNameEn() {
        return TypeNameEn;
    }

    public void setTypeNameEn(String typeNameEn) {
        TypeNameEn = typeNameEn;
    }

    public String getOfficeName() {
        return OfficeName;
    }

    public void setOfficeName(String officeName) {
        OfficeName = officeName;
    }

    public String getSubType() {
        return SubType;
    }

    public void setSubType(String subType) {
        SubType = subType;
    }

    public String getSubTypeNameAr() {
        return SubTypeNameAr;
    }

    public void setSubTypeNameAr(String subTypeNameAr) {
        SubTypeNameAr = subTypeNameAr;
    }

    public String getSubTypeNameEn() {
        return SubTypeNameEn;
    }

    public void setSubTypeNameEn(String subTypeNameEn) {
        SubTypeNameEn = subTypeNameEn;
    }

    public String getModel() {
        return Model;
    }

    public void setModel(String model) {
        Model = model;
    }

    public String getAction() {
        return Action;
    }

    public void setAction(String action) {
        Action = action;
    }

    public String getPrice() {
        return Price;
    }

    public void setPrice(String price) {
        Price = price;
    }

    public String getId() {
        return Id;
    }

    public void setId(String id) {
        Id = id;
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
