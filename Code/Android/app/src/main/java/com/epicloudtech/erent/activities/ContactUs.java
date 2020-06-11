package com.epicloudtech.erent.activities;

import android.os.Bundle;
import android.support.v4.content.ContextCompat;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;

import com.epicloudtech.erent.R;
import com.epicloudtech.erent.models.requests.BaseRequest;
import com.epicloudtech.erent.models.requests.FormRequest;
import com.epicloudtech.erent.models.requests.LoginRequest;
import com.epicloudtech.erent.models.responses.BaseResponse;
import com.epicloudtech.erent.models.responses.LoginResponse;
import com.epicloudtech.erent.utils.Constants;
import com.epicloudtech.erent.utils.Utils;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;
import rx.Observer;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

public class ContactUs extends Base {

    @BindView(R.id.edtSubject)
    EditText edtSubject;
    @BindView(R.id.edtBody)
    EditText edtBody;
    @BindView(R.id.edtName)
    EditText edtName;
    @BindView(R.id.edtEmail)
    EditText edtEmail;
    @BindView(R.id.edtPhone)
    EditText edtPhone;
    @BindView(R.id.tvSend)
    TextView tvSend;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_contact_us);
        ButterKnife.bind(this);
    }

    @OnClick({R.id.tvSend})
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.tvSend:
                if (validateFields()) {
                    sendForm();
                }
                break;
        }
    }

    private void sendForm() {
        showLoading();
        BaseRequest<FormRequest> baseRequest = new BaseRequest<>();
        FormRequest request = new FormRequest();
        request.setSubject(edtSubject.getText().toString());
        request.setBody(edtBody.getText().toString().trim());
        request.setName(edtName.getText().toString().trim());
        request.setEmail(edtEmail.getText().toString().trim());
        request.setMobile(edtPhone.getText().toString().trim());

        baseRequest.setData(request);
        baseRequest.setLanguage(getLanguageEnum());
        baseRequest.setToken(getToken());
        baseRequest.setApplicationKey(Constants.APPLICATION_KEY);

        mAPIInterface.sendForm(baseRequest)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new Observer<BaseResponse<String>>() {
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
                    public void onNext(BaseResponse<String> response) {
                        hideLoading();
                        if (response.isSuccess()) {
                            showToast(getString(R.string.form_sent), SUCCESS);
                        } else {
                            showToast(response.getMessage(), ERROR);
                        }
                    }
                });
    }

    private boolean validateFields() {
        if (!Utils.isValidInput(edtSubject)) {
            edtSubject.setError(getString(R.string.username_error));
            showToast(getString(R.string.subject_error), ERROR);
            return false;
        }
        if (!Utils.isValidInput(edtBody)) {
            edtBody.setError(getString(R.string.password_error));
            showToast(getString(R.string.body_error), ERROR);
            return false;
        }
        if (!Utils.isValidInput(edtName)) {
            edtName.setError(getString(R.string.password_error));
            showToast(getString(R.string.name_error), ERROR);
            return false;
        }
        if (!Utils.isValidInput(edtEmail)) {
            edtEmail.setError(getString(R.string.password_error));
            showToast(getString(R.string.email_error2), ERROR);
            return false;
        }
        if (!Utils.isValidInput(edtPhone)) {
            edtPhone.setError(getString(R.string.password_error));
            showToast(getString(R.string.phone_error2), ERROR);
            return false;
        }
        return true;
    }

}
