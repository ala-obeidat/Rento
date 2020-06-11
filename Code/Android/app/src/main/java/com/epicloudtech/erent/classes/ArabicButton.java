package com.epicloudtech.erent.classes;

import android.annotation.SuppressLint;
import android.content.Context;
import android.graphics.Typeface;
import android.util.AttributeSet;
import android.widget.Button;

@SuppressLint("AppCompatCustomView")
public class ArabicButton extends Button {

    public ArabicButton(Context context, AttributeSet attrs, int defStyle) {
        super(context, attrs, defStyle);
        init();
    }

    public ArabicButton(Context context, AttributeSet attrs) {
        super(context, attrs);
        init();
    }

    public ArabicButton(Context context) {
        super(context);
        init();
    }

    public void init() {

        Typeface tf = Typeface.createFromAsset(getContext().getAssets(), "fonts/arabic_font.ttf");
        setTypeface(tf, 1);

    }
}
