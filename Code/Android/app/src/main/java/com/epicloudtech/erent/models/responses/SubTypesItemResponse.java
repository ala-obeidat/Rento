package com.epicloudtech.erent.models.responses;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class SubTypesItemResponse implements Serializable{

    @SerializedName("ExternalData")
    @Expose
    private String ExternalData;

    @SerializedName("NameEn")
    @Expose
    private String NameEn;

    @SerializedName("Id")
    @Expose
    private String Id;

    @SerializedName("Name")
    @Expose
    private String Name;

    public String getExternalData() {
        return ExternalData;
    }

    public void setExternalData(String externalData) {
        ExternalData = externalData;
    }

    public String getNameEn() {
        return NameEn;
    }

    public void setNameEn(String nameEn) {
        NameEn = nameEn;
    }

    public String getId() {
        return Id;
    }

    public void setId(String id) {
        Id = id;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }
}
