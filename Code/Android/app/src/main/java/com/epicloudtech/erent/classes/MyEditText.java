package com.epicloudtech.erent.classes;

import android.annotation.SuppressLint;
import android.content.Context;
import android.graphics.Typeface;
import android.util.AttributeSet;
import android.widget.EditText;

import com.epicloudtech.erent.utils.Constants;
import com.epicloudtech.erent.utils.Utils;


@SuppressLint("AppCompatCustomView")
public class MyEditText extends EditText {

    public MyEditText(Context context, AttributeSet attrs, int defStyle) {
        super(context, attrs, defStyle);
        init();
    }

    public MyEditText(Context context, AttributeSet attrs) {
        super(context, attrs);
        init();
    }

    public MyEditText(Context context) {
        super(context);
        init();
    }

    public void init() {
        if (Utils.getValue(getContext(), Constants.USER_LANGUAGE, "en").equalsIgnoreCase(Constants.ENGLISH_LANGUAGE)) {
            Typeface tf = Typeface.createFromAsset(getContext().getAssets(), "fonts/english_font.ttf");
            setTypeface(tf, 1);
        } else if (Utils.getValue(getContext(), Constants.USER_LANGUAGE, "en").equalsIgnoreCase(Constants.ARABIC_LANGUAGE)) {
            Typeface tf = Typeface.createFromAsset(getContext().getAssets(), "fonts/arabic_font.ttf");
            setTypeface(tf, 1);
        }
    }
}
