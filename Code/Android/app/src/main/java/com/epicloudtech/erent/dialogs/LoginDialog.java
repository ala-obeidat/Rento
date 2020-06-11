package com.epicloudtech.erent.dialogs;

import android.app.Dialog;
import android.content.Context;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.view.View;
import android.view.Window;
import android.widget.Button;

import com.epicloudtech.erent.R;
import com.epicloudtech.erent.activities.LoginRegister;
import com.epicloudtech.erent.utils.Utils;

public class LoginDialog extends Dialog {


    private Button btn_cancel;
    private Button btn_send;
    private Context context;

    public LoginDialog(@NonNull Context context) {
        super(context);
        this.context = context;
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.dialog_login);
        setCancelable(false);

        btn_send = findViewById(R.id.btn_send);
        btn_cancel = findViewById(R.id.btn_cancel);


        btn_cancel.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                dismiss();
            }
        });
        btn_send.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //send
                Utils.goToActivityWithAnimation(context, LoginRegister.class,true);
               dismiss();
            }
        });


    }
}
