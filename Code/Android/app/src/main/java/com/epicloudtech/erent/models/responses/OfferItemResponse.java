package com.epicloudtech.erent.models.responses;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class OfferItemResponse {

    @SerializedName("ProviderName")
    @Expose
    private String ProviderName;

    @SerializedName("CarType")
    @Expose
    private String CarType;

    @SerializedName("CarSubType")
    @Expose
    private String CarSubType;

    @SerializedName("CarModel")
    @Expose
    private String CarModel;

    @SerializedName("From")
    @Expose
    private String From;

    @SerializedName("To")
    @Expose
    private String To;

    @SerializedName("Period")
    @Expose
    private String Period;

    @SerializedName("Cost")
    @Expose
    private String Cost;

    @SerializedName("Discount")
    @Expose
    private String Discount;

    @SerializedName("TypeNameAr")
    @Expose
    private String TypeNameAr;

    @SerializedName("TypeNameEn")
    @Expose
    private String TypeNameEn;

    @SerializedName("SubTypeNameAr")
    @Expose
    private String SubTypeNameAr;

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

    @SerializedName("SubTypeNameEn")
    @Expose
    private String SubTypeNameEn;

    public String getProviderName() {
        return ProviderName;
    }

    public void setProviderName(String providerName) {
        ProviderName = providerName;
    }

    public String getCarType() {
        return CarType;
    }

    public void setCarType(String carType) {
        CarType = carType;
    }

    public String getCarSubType() {
        return CarSubType;
    }

    public void setCarSubType(String carSubType) {
        CarSubType = carSubType;
    }

    public String getCarModel() {
        return CarModel;
    }

    public void setCarModel(String carModel) {
        CarModel = carModel;
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

    public String getPeriod() {
        return Period;
    }

    public void setPeriod(String period) {
        Period = period;
    }

    public String getCost() {
        return Cost;
    }

    public void setCost(String cost) {
        Cost = cost;
    }

    public String getDiscount() {
        return Discount;
    }

    public void setDiscount(String discount) {
        Discount = discount;
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

    @SerializedName("Id")
    @Expose

    private String Id;

    @SerializedName("CreateDate")
    @Expose
    private String CreateDate;


}
