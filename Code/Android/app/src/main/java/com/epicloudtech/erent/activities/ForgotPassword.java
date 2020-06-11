package com.epicloudtech.erent.activities;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;

import com.epicloudtech.erent.R;
import com.epicloudtech.erent.models.requests.BaseRequest;
import com.epicloudtech.erent.models.responses.BaseResponse;
import com.epicloudtech.erent.utils.Constants;
import com.epicloudtech.erent.utils.Utils;

import butterknife.BindView;
import butterknife.ButterKnife;
import rx.Observer;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

public class ForgotPassword extends Base {
    @BindView(R.id.edtUserName)
    EditText edtUserName;

    @BindView(R.id.tvSend)
    TextView tvSend;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        Utils.refreshLocal(this);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_forgot_password);
        ButterKnife.bind(this);

        tvSend.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (validateFields())
                    forgetPassword();
            }
        });
    }

    private void forgetPassword() {
        showLoading();
        BaseRequest<String> baseRequest = new BaseRequest<>();
        baseRequest.setApplicationKey(Constants.APPLICATION_KEY);
        baseRequest.setLanguage(getLanguageEnum());
        baseRequest.setToken(getToken());

        baseRequest.setData(edtUserName.getText().toString().trim());

        mAPIInterface.forgetPassword(baseRequest)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new Observer<BaseResponse>() {
                    @Override
                    public void onCompleted() {
                        hideLoading();
                    }

                    @Override
                    public void onError(Throwable throwable) {
                        hideLoading();
                        showToast(throwable.getMessage(), ERROR);
                    }

                    @Override
                    public void onNext(BaseResponse response) {
                        hideLoading();
                        if (response.isSuccess()) {
                            String resetPassToken = response.getData().toString();
                            if (resetPassToken.length() > 0) {
                                Intent goToReset = new Intent(ForgotPassword.this, ResetPassword.class);
                                goToReset.putExtra("resetToken", resetPassToken);
                                startActivity(goToReset);
                                overridePendingTransition(R.anim.fade_in, R.anim.fade_out);
                                finish();
                            } else {
                                showToast(getString(R.string.user_doesnt_exist), ERROR);
                            }
                        } else {
                            showToast(getString(R.string.user_doesnt_exist), ERROR);
                        }

                    }
                });
    }


    private boolean validateFields() {
        if (!Utils.isValidInput(edtUserName)) {
            edtUserName.setError(getString(R.string.username_error));
            showToast(getString(R.string.username_error), ERROR);
            return false;
        }

        return true;
    }
}
