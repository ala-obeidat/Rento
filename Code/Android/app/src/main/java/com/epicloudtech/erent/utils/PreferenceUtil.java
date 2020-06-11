package com.epicloudtech.erent.utils;

import android.content.Context;
import android.content.SharedPreferences;
import android.preference.PreferenceManager;

import com.epicloudtech.erent.MyApp;


public class PreferenceUtil {

    private static SharedPreferences getDefaultSharedPreference(Context context) {
        if (PreferenceManager.getDefaultSharedPreferences(MyApp.getInstance().getApplicationContext()) != null)
            return PreferenceManager.getDefaultSharedPreferences(MyApp.getInstance().getApplicationContext());
        else
            return null;
    }

    public static void setSelectedLanguageId(String id){
        final SharedPreferences prefs = getDefaultSharedPreference(MyApp.getInstance().getApplicationContext());
        SharedPreferences.Editor editor = prefs.edit();
        editor.putString("app_language_id", id);
        editor.apply();
    }

    public static String getSelectedLanguageId(){
        return getDefaultSharedPreference(MyApp.getInstance().getApplicationContext())
                .getString("app_language_id", "en");
    }

}
