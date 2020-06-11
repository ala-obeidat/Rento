package com.epicloudtech.erent.database;

import android.provider.BaseColumns;

/**
 * Created by ssprog9 on 5/7/2017.
 */

public class ItemsTable {


    public String Id;
    public String itemId;
    public String favourite;
    public String exelusive;

    public ItemsTable(String Id, String itemId, String favourite, String exelusive) {

        this.Id = Id;
        this.itemId = itemId;
        this.favourite = favourite;
        this.exelusive = exelusive;

    }

    public static abstract class Tablevalue implements BaseColumns {


        public static final String Action = "Action";
        public static final String favourite = "favourite";
        public static final String Color = "Color";
        public static final String CreateDate = "CreateDate";
        public static final String DayCost = "DayCost";
        public static final String Id = "Id";
        public static final String Model = "Model";
        public static final String OfficeMobile = "OfficeMobile";
        public static final String OfficeName = "OfficeName";
        public static final String Price = "Price";
        public static final String Status = "Status";
        public static final String SubType = "SubType";
        public static final String SubTypeNameAr = "SubTypeNameAr";
        public static final String SubTypeNameEn = "SubTypeNameEn";
        public static final String Type = "Type";
        public static final String TypeNameAr = "TypeNameAr";
        public static final String TypeNameEn = "TypeNameEn";


    }

}
