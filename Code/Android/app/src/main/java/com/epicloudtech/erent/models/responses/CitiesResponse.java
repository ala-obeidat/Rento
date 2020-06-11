package com.epicloudtech.erent.models.responses;

import com.epicloudtech.erent.models.Entities.City;
import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

public class CitiesResponse extends BasicResponse{

    @SerializedName("Data")
    @Expose
    private ArrayList<City> cities;

    public ArrayList<City> getCities() {
        return cities;
    }

    public void setCities(ArrayList<City> countries) {
        this.cities = countries;
    }


}
