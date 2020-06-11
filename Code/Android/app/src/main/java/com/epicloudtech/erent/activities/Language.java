package com.epicloudtech.erent.activities;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import com.epicloudtech.erent.R;
import com.epicloudtech.erent.utils.Constants;
import com.epicloudtech.erent.utils.LanguageUtils;
import com.epicloudtech.erent.utils.Utils;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;


public class Language extends Base {

    @BindView(R.id.englishButton)
    Button englishButton;

    @BindView(R.id.arabicButton)
    Button arabicButton;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        Utils.refreshLocal(this);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_language);
        ButterKnife.bind(this);
    }

    @OnClick({R.id.englishButton, R.id.arabicButton})
    public void onClick(View view) {

        switch (view.getId()) {
            case R.id.englishButton:
                Utils.setValue(Language.this, Constants.USER_LANGUAGE, Constants.ENGLISH_LANGUAGE);
                Utils.setValue(Language.this, "lang", Constants.ENGLISH_LANGUAGE);
                LanguageUtils.updateLanguage(Language.this, Utils.getValue(Language.this, Constants.USER_LANGUAGE, Constants.ENGLISH_LANGUAGE));
                navigate();
                break;
            case R.id.arabicButton:
                Utils.setValue(Language.this, Constants.USER_LANGUAGE, Constants.ARABIC_LANGUAGE);
                Utils.setValue(Language.this, "lang", Constants.ARABIC_LANGUAGE);
                LanguageUtils.updateLanguage(Language.this, Utils.getValue(Language.this, Constants.USER_LANGUAGE, Constants.ARABIC_LANGUAGE));
                navigate();
                break;
        }

    }

    private void navigate() {
        Utils.setValue(Language.this, Constants.IS_SELECTED_LANGUAGE, true);
        if (Utils.getValue(Language.this, Constants.IS_REGISTERED, false)) {
            Utils.goToActivityWithAnimation(Language.this, Main.class, true);
        } else {
           // Utils.goToActivityWithAnimation(Language.this, LoginRegister.class, true);
            Utils.goToActivityWithAnimation(Language.this, Main.class, true);
        }
    }


}
