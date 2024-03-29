package com.epicloudtech.erent.utils;

import android.content.Context;
import android.content.res.Configuration;
import android.content.res.Resources;
import android.support.annotation.StringDef;

import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.util.Locale;

public class LocaleUtils {

    @Retention(RetentionPolicy.SOURCE)
    @StringDef({ENGLISH, FRENCH, SPANISH})
    public @interface LocaleDef {
        String[] SUPPORTED_LOCALES = {ENGLISH, FRENCH, SPANISH};
    }

    public static final String ENGLISH = "en";
    public static final String FRENCH = "fr";
    public static final String SPANISH = "es";


    public static void initialize(Context context) {
        setLocale(context, ENGLISH);
    }

    public static void initialize(Context context, @LocaleDef String defaultLanguage) {
        setLocale(context, defaultLanguage);
    }


    public static boolean setLocale(Context context, @LocaleDef String language) {
        return updateResources(context, language);
    }

    private static boolean updateResources(Context context, String language) {
        Locale locale = new Locale(language);
        Locale.setDefault(locale);
        Resources resources = context.getResources();
        Configuration configuration = resources.getConfiguration();
        context.createConfigurationContext(configuration);
        configuration.locale = locale;
        resources.updateConfiguration(configuration, resources.getDisplayMetrics());
        return true;
    }

}

