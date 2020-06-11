package com.epicloudtech.erent.activities;

import android.Manifest;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.ActivityCompat;
import android.view.View;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.RadioButton;
import android.widget.TextView;

import com.epicloudtech.erent.R;
import com.epicloudtech.erent.models.requests.BaseRequest;
import com.epicloudtech.erent.models.requests.BookRequest;
import com.epicloudtech.erent.models.requests.Location;
import com.epicloudtech.erent.models.responses.BaseResponse;
import com.epicloudtech.erent.utils.Constants;
import com.epicloudtech.erent.utils.GPSTracker;
import com.epicloudtech.erent.utils.Utils;
import com.wdullaer.materialdatetimepicker.date.DatePickerDialog;

import java.util.Calendar;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;
import info.hoang8f.android.segmented.SegmentedGroup;
import rx.Observer;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

public class Booking extends Base implements DatePickerDialog.OnDateSetListener {

    @BindView(R.id.edtFromDate)
    EditText edtFromDate;
    @BindView(R.id.edtToDate)
    EditText edtToDate;
    @BindView(R.id.rdGroup)
    SegmentedGroup rdGroup;
    @BindView(R.id.rdPickup)
    RadioButton rdPickup;
    @BindView(R.id.rvDelivery)
    RadioButton rvDelivery;
    @BindView(R.id.chkAgree)
    CheckBox chkAgree;
    @BindView(R.id.tvConfirmBooking)
    TextView tvConfirmBooking;
    @BindView(R.id.ivBack)
    ImageView ivBack;
    @BindView(R.id.tvTerms)
    TextView tvTerms;

    int dayCost = 0;
    int weekCost = 0;
    int monthCost = 0;
    String officeFlag = "";

    GPSTracker gps;
    double longitude = 0;
    double latitude = 0;

    private DatePickerDialog dpd;

    public boolean resumed = false;

    private int clickIndex = 0;

    String carId = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        Utils.refreshLocal(this);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_booking);
        ButterKnife.bind(this);
        carId = getIntent().getStringExtra("CarIdFromBooking");
        dayCost = Integer.parseInt(getIntent().getStringExtra("dayCost"));
        weekCost = Integer.parseInt(getIntent().getStringExtra("weekCost"));
        monthCost = Integer.parseInt(getIntent().getStringExtra("monthCost"));
        officeFlag = getIntent().getStringExtra("officeFlag");

        tvTerms.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                getTerms();
            }
        });


        if (officeFlag.equalsIgnoreCase("0")) {
            rvDelivery.setVisibility(View.GONE);
        }

    }

    @OnClick({R.id.ivBack, R.id.tvConfirmBooking, R.id.edtFromDate, R.id.edtToDate})
    public void onClick(View v) {
        switch (v.getId()) {

            case R.id.ivBack:
                finish();
                break;
            case R.id.tvConfirmBooking:
                if (validateFields())
                    bookCar();
                break;
            case R.id.edtFromDate:
                clickIndex = 0;
                showDatePicker();
                break;
            case R.id.edtToDate:
                clickIndex = 1;
                showDatePicker();
                break;

        }
    }


    private void getTerms() {
        showLoading();

        BaseRequest<String> baseRequest = new BaseRequest<>();
        baseRequest.setApplicationKey(Constants.APPLICATION_KEY);
        baseRequest.setLanguage(getLanguageEnum());
        baseRequest.setToken(getToken());

        baseRequest.setData(getIntent().getStringExtra("OfficeId"));

        mAPIInterface.getTerms(baseRequest)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new Observer<BaseResponse<String>>() {
                    @Override
                    public void onCompleted() {
                        //for testing
                        //  Utils.goToActivityWithAnimation(Register.this, PhoneAuth.class, true);
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
                            String terms = response.getData();
                            Intent goToTerms = new Intent(Booking.this, Terms.class);
                            goToTerms.putExtra("terms_text", terms);
                            startActivity(goToTerms);
                        } else {
                            showToast(response.getMessage(), ERROR);
                        }
                    }
                });
    }


    private boolean validateFields() {

        if (!Utils.isValidInput(edtFromDate)) {
            edtFromDate.setError(getString(R.string.from_date_error));
            showToast(getString(R.string.from_date_error), ERROR);
            return false;
        }
        if (!Utils.isValidInput(edtToDate)) {
            edtToDate.setError(getString(R.string.to_date_error));
            showToast(getString(R.string.to_date_error), ERROR);
            return false;
        }
//        if (!chkAgree.isChecked()) {
//            showToast(getString(R.string.accept_agreement), ERROR);
//            return false;
//        }
        if (!rdPickup.isChecked() && !rvDelivery.isChecked()) {
            showToast(getString(R.string.choose_method), ERROR);
            return false;
        }

        return true;
    }

    private void bookCar() {
        String price = "";
        int priceValue = 0;
        showLoading();
        int diff = (int) Utils.getDaysDifference(edtFromDate.getText().toString(), edtToDate.getText().toString());
        if (diff <= 7) {
            priceValue = diff * dayCost;
        } else if (diff <= 30) {
            priceValue = diff * weekCost;
        } else {
            priceValue = diff * monthCost;
        }

        price = String.valueOf(priceValue);

        BaseRequest<BookRequest> baseRequest = new BaseRequest<>();
        BookRequest request = new BookRequest();
        request.setCarId(carId);
        request.setCityId(Utils.getValue(Booking.this, Constants.USER_CITY_ID, "1"));
        if (rdPickup.isChecked()) {
            request.setFlag("0");
        } else {
            if (longitude == 0 || latitude == 0) {
                showToast(getString(R.string.location_required), INFO);
                loadUserLocation();
                hideLoading();
                return;
            } else {
                Location location = new Location();
                location.setLatitude(String.valueOf(latitude));
                location.setLongitude(String.valueOf(longitude));
                request.setLocation(location);
                request.setFlag("1");
            }

        }
        request.setFrom(Utils.convertStringToDateTime(edtFromDate.getText().toString().trim()));
        request.setTo(Utils.convertStringToDateTime(edtToDate.getText().toString().trim()));
        request.setPrice(price);

        baseRequest.setData(request);
        baseRequest.setLanguage(getLanguageEnum());
        baseRequest.setToken(getToken());
        baseRequest.setApplicationKey(Constants.APPLICATION_KEY);

        mAPIInterface.bookCar(baseRequest)
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
                            showToast(getString(R.string.car_booked_successfully), SUCCESS);
                            final Handler handler = new Handler();
                            handler.postDelayed(new Runnable() {
                                @Override
                                public void run() {
                                    Utils.goToActivityWithAnimation(Booking.this, Main.class, true);
                                }
                            }, 1500);
                        } else {
                            showToast(response.getMessage(), ERROR);
                        }
                    }
                });
    }

    private void showDatePicker() {

        Calendar now = Calendar.getInstance();
                /*
                It is recommended to always create a new instance whenever you need to show a Dialog.
                The sample app is reusing them because it is useful when looking for regressions
                during testing
                 */

                int day;
                if (clickIndex == 0){
                    day = now.get(Calendar.DAY_OF_MONTH);
                }else {
                    day = now.get(Calendar.DAY_OF_MONTH) + 1;
                }

        if (dpd == null) {
            dpd = DatePickerDialog.newInstance(
                    Booking.this,
                    now.get(Calendar.YEAR),
                    now.get(Calendar.MONTH),
                    day
            );
        } else {
            dpd.initialize(
                    Booking.this,
                    now.get(Calendar.YEAR),
                    now.get(Calendar.MONTH),
                    day
            );
        }
        dpd.setVersion(DatePickerDialog.Version.VERSION_1);

        dpd.setTitle(getString(R.string.birthdate));


        dpd.show(getFragmentManager(), "");
    }

    @Override
    protected void onResume() {
        if (resumed) {
            loadUserLocation();
        }
        super.onResume();
    }


    private void loadUserLocation() {
        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(Booking.this,
                    new String[]{Manifest.permission.ACCESS_FINE_LOCATION, Manifest.permission.ACCESS_COARSE_LOCATION},
                    1);
            return;
        }
//        locationManager.requestLocationUpdates(
//                LocationManager.GPS_PROVIDER, 5000, 10, locationListener);
        gps = new GPSTracker(Booking.this);

        if (gps.canGetLocation()) {
            longitude = gps.getLongitude();
            latitude = gps.getLatitude();
        } else {
            resumed = true;
            gps.showSettingsAlert();
        }
    }


    @Override
    public void onRequestPermissionsResult(int requestCode, String permissions[], int[] grantResults) {
        switch (requestCode) {
            case 1: {
                if (grantResults.length > 0
                        && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                    loadUserLocation();
                } else {
                }
                return;
            }
        }
    }

    @Override
    public void onDateSet(DatePickerDialog view, int year, int monthOfYear, int dayOfMonth) {
        String date = dayOfMonth + "/" + (monthOfYear + 1) + "/" + year;
        if (clickIndex == 0)
            edtFromDate.setText(date);
        else
            edtToDate.setText(date);
    }

}
