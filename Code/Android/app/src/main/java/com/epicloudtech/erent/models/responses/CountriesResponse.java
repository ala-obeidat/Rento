package com.epicloudtech.erent.models.responses;

import com.epicloudtech.erent.models.Entities.Country;
import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

public class CountriesResponse extends BasicResponse{

    public ArrayList<Country> getCountries() {
        return countries;
    }

    public void setCountries(ArrayList<Country> countries) {
        this.countries = countries;
    }

    @SerializedName("Data")
    @Expose
    private ArrayList<Country> countries;
}
