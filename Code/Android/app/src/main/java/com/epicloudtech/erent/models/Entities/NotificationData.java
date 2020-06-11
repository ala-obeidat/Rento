package com.epicloudtech.erent.models.Entities;

import android.provider.BaseColumns;

/**
 * Created by Chionophile on 8/29/2017.
 */

public class NotificationData {
    public String TitleAr;
    public String TitleEn;
    public String BodyAr;
    public String BodyEn;


    public NotificationData(String TitleAr, String TitleEn, String BodyAr, String BodyEn) {

        this.TitleAr = TitleAr;
        this.TitleEn = TitleEn;
        this.BodyAr = BodyAr;
        this.BodyEn = BodyEn;

    }

    public static abstract class NotificationValue implements BaseColumns {

        public static final String Id = "Id";
        public static final String TitleAr = "TitleAr";
        public static final String TitleEn = "TitleEn";
        public static final String BodyAr = "BodyAr";
        public static final String BodyEn = "BodyEn";

    }


}
