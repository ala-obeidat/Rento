package com.epicloudtech.erent.retrofit;


import com.epicloudtech.erent.utils.Constants;

public class ApiUtils {

    public static APIInterface getAPIService() {
        return RetrofitClient.getClient(Constants.BASE_URL).create(APIInterface.class);
    }
}