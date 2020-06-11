package com.epicloudtech.erent.utils;

import android.content.Context;
import android.content.res.Configuration;
import android.content.res.Resources;
import android.text.TextUtils;

import java.util.Locale;

public class LanguageUtils {

    public static void updateLanguage(Context ctx, String lang) {

        Resources resources = ctx.getResources();
        Configuration cfg = resources.getConfiguration();
        if (!TextUtils.isEmpty(lang)) {
            cfg.locale = new Locale(lang);
        } else {
            cfg.locale = Locale.getDefault();
        }
        Locale.setDefault(cfg.locale);
        resources.updateConfiguration(cfg, resources.getDisplayMetrics());

    }

}
