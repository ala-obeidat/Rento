package com.epicloudtech.erent.dialogs;

import android.app.Dialog;
import android.content.Context;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.view.View;
import android.view.Window;
import android.widget.Button;
import android.widget.TextView;

import com.epicloudtech.erent.R;
import com.epicloudtech.erent.interfaces.VerifiyPhoneCallback;

public class VerifyPhoneDialog extends Dialog {


    private Button btn_edit;
    private Button btn_confirm;
    private TextView tvVerifyText;
    private Context context;
    private String phone;
    VerifiyPhoneCallback callback;

    public VerifyPhoneDialog(@NonNull Context context, String phone, VerifiyPhoneCallback callback) {
        super(context);
        this.context = context;
        this.phone = phone;
        this.callback = callback;
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.dialog_phone_verify);
        setCancelable(false);

        btn_edit = findViewById(R.id.btn_edit);
        btn_confirm = findViewById(R.id.btn_confirm);
        tvVerifyText = findViewById(R.id.tvVerifyText);


        tvVerifyText.setText(context.getString(R.string.is_this_phone_correct) + " " + phone);


        btn_edit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                callback.onEdit();
                dismiss();
            }
        });
        btn_confirm.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                callback.onConfirm();
                dismiss();
            }
        });


    }
}
