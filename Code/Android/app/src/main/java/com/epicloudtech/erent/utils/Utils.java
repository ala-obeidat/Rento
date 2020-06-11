package com.epicloudtech.erent.utils;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.Configuration;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.util.Log;
import android.view.View;
import android.widget.EditText;

import com.epicloudtech.erent.R;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;

public class Utils {

    public static synchronized void setValue(Context context, String key, String value) {
        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(context);
        prefs.edit().putString(key, value).commit();
    }

    public static synchronized String getValue(Context context, String key, String defaultValue) {
        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(context);
        return prefs.getString(key, defaultValue);
    }

    public static synchronized void setValue(Context context, String key, float value) {
        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(context);
        prefs.edit().putFloat(key, value).commit();
    }

    public static synchronized float getValue(Context context, String key, float defaultValue) {
        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(context);
        return prefs.getFloat(key, defaultValue);
    }

    public static synchronized void setValue(Context context, String key, int value) {
        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(context);
        prefs.edit().putInt(key, value).commit();
    }

    public static synchronized int getValue(Context context, String key, int defaultValue) {
        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(context);
        return prefs.getInt(key, defaultValue);
    }

    public static synchronized void setValue(Context context, String key, boolean value) {
        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(context);
        prefs.edit().putBoolean(key, value).commit();
    }

    public static synchronized boolean getValue(Context context, String key, boolean defaultValue) {
        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(context);
        return prefs.getBoolean(key, defaultValue);
    }

    public static synchronized void setValue(Context context, String key, long value) {
        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(context);
        prefs.edit().putLong(key, value).commit();
    }

    public static synchronized long getValue(Context context, String key, long defaultValue) {
        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(context);
        return prefs.getLong(key, defaultValue);
    }


    public static boolean isValidPhoneNumber(EditText editText) {
        boolean status = false;
        if (editText.getText().toString().trim().length() == 10) {
            if (editText.getText().toString().trim().startsWith("05")) {
                status = true;
            }
        }
        return status;
    }

    public static boolean isValidEmail(EditText editText) {
        boolean status = false;
        if (editText.getText().toString().trim().length() > 8) {
            if (editText.getText().toString().trim().contains("@")) {
                status = true;
            }
        }
        return status;
    }

    public static long getDaysDifference(String date1, String date2) {

        SimpleDateFormat dateFormat = new SimpleDateFormat(
                "dd/MM/yyyy", Locale.ENGLISH);
        Date mydate1 = null;
        Date mydate2 = null;
        try {
            mydate1 = dateFormat.parse(date1);
            mydate2 = dateFormat.parse(date2);

        } catch (ParseException e) {
            e.printStackTrace();
        }


        long diff = mydate2.getTime() - mydate1.getTime();
        long seconds = diff / 1000;
        long minutes = seconds / 60;
        long hours = minutes / 60;
        long days = hours / 24;

        return days;
    }

    public static void launchMaps(Context context, double latitude, double longitude) {
//        String uri = String.format(Locale.ENGLISH, "geo:%f,%f", latitude, longitude);
//        Intent intent = new Intent(Intent.ACTION_VIEW, Uri.parse(uri));
//        context.startActivity(intent);
        Uri.Builder directionsBuilder = new Uri.Builder()
                .scheme("https")
                .authority("www.google.com")
                .appendPath("maps")
                .appendPath("dir")
                .appendPath("")
                .appendQueryParameter("api", "1")
                .appendQueryParameter("destination", latitude + "," + longitude);

        context.startActivity(new Intent(Intent.ACTION_VIEW, directionsBuilder.build()));



    }

    public static String convertStringToDateTime(String date) {
        SimpleDateFormat dateFormat = new SimpleDateFormat(
                "dd/MM/yyyy", Locale.ENGLISH);
        Date myDate = null;
        try {
            myDate = dateFormat.parse(date);

        } catch (ParseException e) {
            e.printStackTrace();
        }

        try {
            SimpleDateFormat timeFormat = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss", Locale.ENGLISH);
            String finalDate = timeFormat.format(myDate);
            return finalDate;
        }catch (Exception e){
            return "";
        }




    }


    public static boolean isValidInput(EditText editText) {
        boolean status = false;
        if (editText.getText().toString().trim().length() > 0) {
            status = true;
        }
        return status;
    }

    public static boolean isValidIdentifier(EditText editText) {
        boolean status = false;
        if (editText.getText().toString().trim().length() == 10) {
            status = true;
        }
        return status;
    }


    public static String getStringInLang(Context context, String english, String arabic) {
        String str = "";
        if (Utils.getValue(context, Constants.USER_LANGUAGE, "en").equalsIgnoreCase(Constants.ENGLISH_LANGUAGE)) {
            str = english;
        } else {
            str = arabic;
        }
        return str;
    }

    public static void refreshLocal(Context context) {
        try {
            SharedPreferences sharedPreferences = PreferenceManager.getDefaultSharedPreferences(context);
            if (sharedPreferences.getString("lang", "en").equals("en")) {
                sharedPreferences = PreferenceManager.getDefaultSharedPreferences(context);
                sharedPreferences.edit().putString("lang", "en").apply();
                updateLanguage(context, sharedPreferences);

                if (android.os.Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
                    ((Activity) context).getWindow().getDecorView().setLayoutDirection(View.LAYOUT_DIRECTION_LTR);
                }
                PreferenceUtil.setSelectedLanguageId("en");

            } else {
                sharedPreferences = PreferenceManager.getDefaultSharedPreferences(context);
                sharedPreferences.edit().putString("lang", "ar").apply();
                updateLanguage(context, sharedPreferences);

                if (android.os.Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
                    ((Activity) context).getWindow().getDecorView().setLayoutDirection(View.LAYOUT_DIRECTION_RTL);
                }
                PreferenceUtil.setSelectedLanguageId("ar");


            }
        } catch (Exception e) {
            Log.e("fff", "refreshLocal: ");
        }

    }

    public static void updateLanguage(Context cxt, SharedPreferences sharedPreferences) {
//        setLocale(sharedPreferences.getString("lang", "en"));
        Locale local = new Locale(sharedPreferences.getString("lang", "en"));
        Locale.setDefault(local);
        Configuration configuration = cxt.getResources().getConfiguration();
        configuration.setLocale(local);
        cxt.getResources().updateConfiguration(configuration, cxt.getResources().getDisplayMetrics());

    }


    public static boolean isValidPassword(EditText password, EditText confirmPassword) {
        boolean status = false;
        if (password.getText().toString().trim().length() > 0 && confirmPassword.getText().toString().trim().length() > 0) {
            if (password.getText().toString().equals(confirmPassword.getText().toString()))
                status = true;
        }
        return status;
    }


    public static void goToActivityWithAnimation(Context context, Class<?> to, boolean finishAfter) {
        Intent i = new Intent(context, to);
        context.startActivity(i);
        ((Activity) context).overridePendingTransition(R.anim.fade_in, R.anim.fade_out);
        if (finishAfter) {
            ((Activity) context).finish();
        }
    }

    public static void goToActivity(Context context, Class<?> to, boolean finishAfter) {
        Intent i = new Intent(context, to);
        context.startActivity(i);
        if (finishAfter) {
            ((Activity) context).finish();
        }
    }


    public static String getCurrentLanguageFontPath(Context context) {
        String fontPath;
        if (Utils.getValue(context, Constants.USER_LANGUAGE, Constants.ENGLISH_LANGUAGE).equalsIgnoreCase(Constants.ENGLISH_LANGUAGE)) {
            fontPath = "fonts/english_font.ttf";
        } else {
            fontPath = "fonts/arabic_font.ttf";
        }
        return fontPath;
    }


}
