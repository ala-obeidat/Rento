package com.epicloudtech.erent.models.responses;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class TypeItemResponse implements Serializable{

    @SerializedName("NameEn")
    @Expose
    private String NameEn;

    @SerializedName("Name")
    @Expose
    private String Name;

    public String getNameEn() {
        return NameEn;
    }

    public void setNameEn(String nameEn) {
        NameEn = nameEn;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public String getId() {
        return Id;
    }

    public void setId(String id) {
        Id = id;
    }

    @SerializedName("Id")
    @Expose
    private String Id;
}
