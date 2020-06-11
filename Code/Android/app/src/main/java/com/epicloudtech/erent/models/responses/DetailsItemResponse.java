package com.epicloudtech.erent.models.responses;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

public class DetailsItemResponse {

    @SerializedName("KiloNumber")
    @Expose
    private String KiloNumber;


    @SerializedName("AdditinalKiloCost")
    @Expose
    private String AdditinalKiloCost;

    @SerializedName("KiloLimit")
    @Expose
    private String KiloLimit;

    @SerializedName("Description")
    @Expose
    private String Description;

    @SerializedName("WeekCost")
    @Expose
    private String WeekCost;

    @SerializedName("MonthCost")
    @Expose
    private String MonthCost;

    @SerializedName("Images")
    @Expose
    private String Images;

    @SerializedName("DeletedImages")
    @Expose
    private String DeletedImages;

    @SerializedName("ImageIds")
    @Expose
    private ArrayList<String> ImageIds;

    @SerializedName("Flag")
    @Expose
    private String Flag;

    @SerializedName("CityId")
    @Expose
    private String CityId;

    @SerializedName("CountryId")
    @Expose
    private String CountryId;

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

    @SerializedName("CreateDate")
    @Expose
    private String CreateDate;

    @SerializedName("Latitude")
    @Expose
    private String Latitude;
    @SerializedName("Longitude")
    @Expose
    private String Longitude;
    @SerializedName("OfficeFlag")
    @Expose
    private String OfficeFlag;

    public String getOfficeFlag() {
        return OfficeFlag;
    }

    public void setOfficeFlag(String officeFlag) {
        OfficeFlag = officeFlag;
    }

    public String getLatitude() {
        return Latitude;
    }

    public void setLatitude(String latitude) {
        Latitude = latitude;
    }

    public String getLongitude() {
        return Longitude;
    }

    public void setLongitude(String longitude) {
        Longitude = longitude;
    }

    public String getKiloLimit() {
        return KiloLimit;
    }

    public void setKiloLimit(String kiloLimit) {
        KiloLimit = kiloLimit;
    }

    public String getKiloNumber() {
        return KiloNumber;
    }

    public void setKiloNumber(String kiloNumber) {
        KiloNumber = kiloNumber;
    }

    public String getAdditinalKiloCost() {
        return AdditinalKiloCost;
    }

    public void setAdditinalKiloCost(String additinalKiloCost) {
        AdditinalKiloCost = additinalKiloCost;
    }

    public String getDescription() {
        return Description;
    }

    public void setDescription(String description) {
        Description = description;
    }

    public String getWeekCost() {
        return WeekCost;
    }

    public void setWeekCost(String weekCost) {
        WeekCost = weekCost;
    }

    public String getMonthCost() {
        return MonthCost;
    }

    public void setMonthCost(String monthCost) {
        MonthCost = monthCost;
    }

    public String getImages() {
        return Images;
    }

    public void setImages(String images) {
        Images = images;
    }

    public String getDeletedImages() {
        return DeletedImages;
    }

    public void setDeletedImages(String deletedImages) {
        DeletedImages = deletedImages;
    }

    public ArrayList<String> getImageIds() {
        return ImageIds;
    }

    public void setImageIds(ArrayList<String> imageIds) {
        ImageIds = imageIds;
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

    public String getCountryId() {
        return CountryId;
    }

    public void setCountryId(String countryId) {
        CountryId = countryId;
    }

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


}
