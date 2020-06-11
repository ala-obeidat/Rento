package com.epicloudtech.erent.activities;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;

import com.epicloudtech.erent.R;
import com.epicloudtech.erent.models.requests.BaseRequest;
import com.epicloudtech.erent.models.requests.LoginRequest;
import com.epicloudtech.erent.models.responses.LoginResponse;
import com.epicloudtech.erent.utils.Constants;
import com.epicloudtech.erent.utils.Utils;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;
import rx.Observer;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

public class LoginRegister extends Base {

    @BindView(R.id.edtUsername)
    EditText edtUsername;

    @BindView(R.id.edtPassword)
    EditText edtPassword;

    @BindView(R.id.tvLogin)
    TextView tvLogin;

    @BindView(R.id.tvRegister)
    TextView tvRegister;

    @BindView(R.id.tvForgotPass)
    TextView tvForgotPass;

    @BindView(R.id.tvGuest)
    TextView tvGuest;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        Utils.refreshLocal(this);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login_register);
        ButterKnife.bind(this);
    }

    @OnClick({R.id.tvLogin, R.id.tvRegister, R.id.tvForgotPass, R.id.tvGuest})
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.tvLogin:
                if (validateFields())
                    loginUser();
                break;
            case R.id.tvRegister:
                Utils.goToActivityWithAnimation(LoginRegister.this, Register.class, false);
                break;
            case R.id.tvForgotPass:
                Utils.goToActivityWithAnimation(LoginRegister.this, ForgotPassword.class, false);
                break;
            case R.id.tvGuest:
                Utils.goToActivityWithAnimation(LoginRegister.this, Main.class, true);
                break;
        }
        edtPassword.setTypeface(getTypeFace());

    }

    private void loginUser() {
        showLoading();
        BaseRequest<LoginRequest> baseRequest = new BaseRequest<>();
        LoginRequest request = new LoginRequest();
        request.setCustomer(true);
        request.setUsername(edtUsername.getText().toString().trim().replace(" ", ""));
        request.setPassword(edtPassword.getText().toString().trim().replace(" ", ""));
        request.setNotificationType("true");

        baseRequest.setData(request);
        baseRequest.setLanguage(getLanguageEnum());
        baseRequest.setToken(getToken());
        baseRequest.setApplicationKey(Constants.APPLICATION_KEY);

        mAPIInterface.loginUser(baseRequest)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new Observer<LoginResponse>() {
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
                    public void onNext(LoginResponse response) {
                        hideLoading();
                        if (response.isSuccess()) {
                            Utils.setValue(LoginRegister.this, Constants.USER_NAME, edtUsername.getText().toString());
                            Utils.setValue(LoginRegister.this, Constants.IS_REGISTERED, true);
                            Utils.setValue(LoginRegister.this, Constants.USER_TOKEN, response.getData().getToken());
                            Utils.goToActivityWithAnimation(LoginRegister.this, Main.class, true);
                        } else {
                            showToast(response.getMessage(), ERROR);
                            if (response.getErrorCode() == 8) {
                                Intent i = new Intent(LoginRegister.this, PhoneAuth.class);
                                i.putExtra("RESEND_VER_CODE", true);
                                startActivity(i);
                                overridePendingTransition(R.anim.fade_in, R.anim.fade_out);
                            }
                        }
                    }
                });
    }


    private boolean validateFields() {
        if (!Utils.isValidInput(edtUsername)) {
            edtUsername.setError(getString(R.string.username_error));
            showToast(getString(R.string.username_error), ERROR);
            return false;
        }
        if (!Utils.isValidInput(edtPassword)) {
            edtUsername.setError(getString(R.string.password_error));
            showToast(getString(R.string.password_error), ERROR);
            return false;
        }
        return true;
    }


}
