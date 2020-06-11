package com.epicloudtech.erent.database;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.database.sqlite.SQLiteStatement;
import android.util.Log;

import com.epicloudtech.erent.models.Entities.NotificationData;
import com.epicloudtech.erent.models.responses.SearchItemResponse;

import java.util.ArrayList;

/**
 * Created by ssprog9 on 5/7/2017.
 */

public class DatabaseOperation extends SQLiteOpenHelper {

    public static final int Database_Version = 1;

    public static final String Database_name = "eRentDB";

    public static final String Table_items = "Cars";
    public static final String Table_notification = "notification";

    private static DatabaseOperation sInstance; // object of this class

    public String Query = "CREATE TABLE " + Table_items + "("
            + ItemsTable.Tablevalue.Id + " TEXT,"
            + ItemsTable.Tablevalue.Action + " TEXT,"
            + ItemsTable.Tablevalue.favourite + " TEXT,"
            + ItemsTable.Tablevalue.Color + " TEXT,"
            + ItemsTable.Tablevalue.CreateDate + " TEXT,"
            + ItemsTable.Tablevalue.DayCost + " TEXT,"
            + ItemsTable.Tablevalue.Model + " TEXT,"
            + ItemsTable.Tablevalue.OfficeMobile + " TEXT,"
            + ItemsTable.Tablevalue.OfficeName + " TEXT,"
            + ItemsTable.Tablevalue.Price + " TEXT,"
            + ItemsTable.Tablevalue.Status + " TEXT,"
            + ItemsTable.Tablevalue.SubType + " TEXT,"
            + ItemsTable.Tablevalue.SubTypeNameAr + " TEXT,"
            + ItemsTable.Tablevalue.SubTypeNameEn + " TEXT,"
            + ItemsTable.Tablevalue.Type + " TEXT,"
            + ItemsTable.Tablevalue.TypeNameAr + " TEXT,"
            + ItemsTable.Tablevalue.TypeNameEn + " TEXT" + ");";


    public String Notification_Query = "CREATE TABLE " + Table_notification + "("
            + NotificationData.NotificationValue.Id + " INTEGER PRIMARY KEY" + " AUTOINCREMENT,"
            + " TEXT," + NotificationData.NotificationValue.TitleAr
            + " TEXT," + NotificationData.NotificationValue.TitleEn
            + " TEXT," + NotificationData.NotificationValue.BodyAr
            + " TEXT," + NotificationData.NotificationValue.BodyEn + " TEXT" + ");";


    public static DatabaseOperation getInstance(Context context) {

        if (sInstance == null) {
            sInstance = new DatabaseOperation(context.getApplicationContext());
        }
        return sInstance;
    }

    public DatabaseOperation(Context context) {
        super(context, Database_name, null, Database_Version);
        // TODO Auto-generated constructor stub
        Log.d("Database Operations", "Database Created");
    }


    ArrayList<NotificationData> listNotification;

    public ArrayList<NotificationData> getNotifications() {

        SQLiteDatabase db = this.getWritableDatabase();

        listNotification = new ArrayList<NotificationData>();

        Cursor cr = db
                .rawQuery(
                        "SELECT TitleAr,TitleEn,BodyAr,BodyEn FROM notification",
                        null);

        if (cr.moveToFirst()) {
            do {
                listNotification.add(new NotificationData
                        (cr.getString(cr.getColumnIndex("TitleAr")),
                                cr.getString(cr.getColumnIndex("TitleEn")),
                                cr.getString(cr.getColumnIndex("BodyAr")),
                                cr.getString(cr.getColumnIndex("BodyEn"))));
            }

            while (cr.moveToNext());
        }

        cr.close();
        db.close();

        return listNotification;
    }

    public void deleteAllNotifications(DatabaseOperation db) {

        db.getReadableDatabase().delete(Table_notification, null, null);

    }

    public long InsertNotification(DatabaseOperation db, String TitleEn, String TitleAr, String BodyEn, String BodyAr) {

        ContentValues values = new ContentValues();

        values.put(NotificationData.NotificationValue.TitleAr, TitleAr);
        values.put(NotificationData.NotificationValue.TitleEn, TitleEn);
        values.put(NotificationData.NotificationValue.BodyAr, BodyAr);
        values.put(NotificationData.NotificationValue.BodyEn, BodyEn);

        long row_id = db.getWritableDatabase().insert(Table_notification, null, values);

        return row_id;
    }


    public long insertItem(SearchItemResponse item) {
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues values = new ContentValues();

        values.put(ItemsTable.Tablevalue.Id, item.getId());
        values.put(ItemsTable.Tablevalue.Action, item.getAction());
        values.put(ItemsTable.Tablevalue.favourite, "false");
        values.put(ItemsTable.Tablevalue.Color, item.getColor());
        values.put(ItemsTable.Tablevalue.CreateDate, item.getCreateDate());
        values.put(ItemsTable.Tablevalue.DayCost, item.getDayCost());
        values.put(ItemsTable.Tablevalue.Model, item.getModel());
        values.put(ItemsTable.Tablevalue.OfficeMobile, item.getOfficeMobile());
        values.put(ItemsTable.Tablevalue.OfficeName, item.getOfficeName());
        values.put(ItemsTable.Tablevalue.Price, item.getPrice());
        values.put(ItemsTable.Tablevalue.Status, item.getStatus());
        values.put(ItemsTable.Tablevalue.SubType, item.getSubType());
        values.put(ItemsTable.Tablevalue.SubTypeNameAr, item.getSubTypeNameAr());
        values.put(ItemsTable.Tablevalue.SubTypeNameEn, item.getSubTypeNameEn());
        values.put(ItemsTable.Tablevalue.Type, item.getType());
        values.put(ItemsTable.Tablevalue.TypeNameAr, item.getTypeNameAr());
        values.put(ItemsTable.Tablevalue.TypeNameEn, item.getTypeNameEn());

        long row_id = db.insert(Table_items, null, values);

        return row_id;
    }


    public boolean isTableEmpty(String Id) {
        SQLiteDatabase db = this.getReadableDatabase();
        SQLiteStatement cursor = null;
        String selectQuery = "SELECT  COUNT(*)  FROM " + Table_items + " WHERE " + ItemsTable.Tablevalue.Id + " = '" + Id + "';";

        //  cursor = db.rawQuery(selectQuery, null);
        cursor = db.compileStatement(selectQuery);
        int count = (int) cursor.simpleQueryForLong();

        if (count > 0) {
            return false;
        }
        return true;
    }


    public String isFavourite(String itemID) {
        SQLiteDatabase db = this.getWritableDatabase();
        Cursor cursor = null;
        String itemname = "";
        String selectQuery = "SELECT  *  FROM " + Table_items + " WHERE " + ItemsTable.Tablevalue.Id + " = '" + itemID + "';";

        cursor = db.rawQuery(selectQuery, null);


        if (cursor.moveToFirst()) {

            itemname = cursor.getString(cursor.getColumnIndex(ItemsTable.Tablevalue.favourite));

        }

        return itemname;
    }


    public ArrayList<SearchItemResponse> getItemsByFav() {

        SQLiteDatabase db = this.getWritableDatabase();

        ArrayList<SearchItemResponse> items = new ArrayList<>();

        Cursor cr = db.rawQuery("SELECT " + Table_items + ".*  FROM " + Table_items + " WHERE " + ItemsTable.Tablevalue.favourite + " = 'true' ;", null);

        if (cr.moveToFirst()) {
            do {
                SearchItemResponse item = new SearchItemResponse();
                item.setAction(cr.getString(cr.getColumnIndex("Action")));
                item.setFavourite(cr.getString(cr.getColumnIndex("favourite")));
                item.setColor(cr.getString(cr.getColumnIndex("Color")));
                item.setCreateDate(cr.getString(cr.getColumnIndex("CreateDate")));
                item.setDayCost(cr.getString(cr.getColumnIndex("DayCost")));
                item.setId(cr.getString(cr.getColumnIndex("Id")));
                item.setModel(cr.getString(cr.getColumnIndex("Model")));
                item.setOfficeMobile(cr.getString(cr.getColumnIndex("OfficeMobile")));
                item.setOfficeName(cr.getString(cr.getColumnIndex("OfficeName")));
                item.setPrice(cr.getString(cr.getColumnIndex("Price")));
                item.setStatus(cr.getString(cr.getColumnIndex("Status")));
                item.setSubType(cr.getString(cr.getColumnIndex("SubType")));
                item.setSubTypeNameAr(cr.getString(cr.getColumnIndex("SubTypeNameAr")));
                item.setSubTypeNameEn(cr.getString(cr.getColumnIndex("SubTypeNameEn")));
                item.setType(cr.getString(cr.getColumnIndex("Type")));
                item.setTypeNameAr(cr.getString(cr.getColumnIndex("TypeNameAr")));
                item.setTypeNameEn(cr.getString(cr.getColumnIndex("TypeNameEn")));

                items.add(item);
            }

            while (cr.moveToNext());
        } else {

        }
        cr.close();
        db.close();

        return items;
    }


    public long updateItemFav(String itemID, String value) {

        SQLiteDatabase db = this.getWritableDatabase();

        ContentValues values = new ContentValues();

        values.put(ItemsTable.Tablevalue.favourite, value);

        long St_id = db.update(Table_items, values, ItemsTable.Tablevalue.Id + " = " + itemID,
                null);

        return St_id;
    }

    // -------------------------------------------------------------------------------------

    // execute create query for tables
    @Override
    public void onCreate(SQLiteDatabase db) {
        db.execSQL(Query);
        db.execSQL(Notification_Query);
    }

    // ------------------------------------------------------------------------------------------
    // if the table was created before drop it and recreate
    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        // TODO Auto-generated method stub

    }


}
