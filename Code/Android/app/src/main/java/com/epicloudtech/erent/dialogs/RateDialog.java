package com.epicloudtech.erent.dialogs;

import android.app.Dialog;
import android.content.Context;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.design.widget.TextInputLayout;
import android.view.View;
import android.view.Window;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RadioButton;
import android.widget.Toast;

import com.epicloudtech.erent.R;
import com.epicloudtech.erent.interfaces.RateCallback;

import es.dmoral.toasty.Toasty;
import me.zhanghai.android.materialratingbar.MaterialRatingBar;

public class RateDialog extends Dialog {


    private RadioButton rdAccept;
    private RadioButton rdReject;
    private MaterialRatingBar rateView;
    private TextInputLayout commentLay;
    private EditText edtComment;
    private Button btn_cancel;
    private Button btn_send;
    private RateCallback callback;
    private Context context;

    public RateDialog(@NonNull Context context, RateCallback callback) {
        super(context);
        this.context = context;
        this.callback = callback;
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.dialog_rate);
        setCancelable(false);


        btn_send = findViewById(R.id.btn_send);
        btn_cancel = findViewById(R.id.btn_cancel);
        rdAccept = findViewById(R.id.rdAccept);
        rdReject = findViewById(R.id.rdReject);
        rateView = findViewById(R.id.rateView);
        commentLay = findViewById(R.id.commentLay);
        edtComment = findViewById(R.id.edtComment);


        rdReject.setChecked(true);

        rateView.setVisibility(View.VISIBLE);
        commentLay.setVisibility(View.VISIBLE);


        rdAccept.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                rateView.setVisibility(View.VISIBLE);
                commentLay.setVisibility(View.GONE);
            }
        });
        rdReject.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                rateView.setVisibility(View.VISIBLE);
                commentLay.setVisibility(View.VISIBLE);
            }
        });

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
                if (!rdAccept.isChecked() && !rdReject.isChecked()) {
                    showToast(context.getString(R.string.rate_error));
                    return;
                }
                String flag;
                if (rdAccept.isChecked()) {
                    flag = "2";

                    if (rateView.getRating() == 0) {
                        showToast(context.getString(R.string.choose_rating));
                        return;
                    }

                } else {
                    flag = "3";

                    if (edtComment.getText().toString().isEmpty()) {
                        showToast(context.getString(R.string.add_comment));
                        return;
                    }
                }
                callback.onSend(flag, String.valueOf(Math.round(rateView.getRating())), edtComment.getText().toString());
                dismiss();
            }
        });


    }


    public void showToast(String message) {
        Toasty.Config.getInstance()
                .setErrorColor(context.getResources().getColor(R.color.app_red)) // optional
                .setInfoColor(context.getResources().getColor(R.color.app_blue)) // optional
                .setSuccessColor(context.getResources().getColor(R.color.app_green)) // optional
                .setWarningColor(context.getResources().getColor(R.color.app_red)) // optional
                .setTextColor(context.getResources().getColor(R.color.white)) // optional
                .tintIcon(false) // optional (apply textColor also to the icon)// optional
                .setTextSize(16) // optional
                .apply(); // required

        Toasty.error(context, message, Toast.LENGTH_SHORT, true).show();
    }
}
