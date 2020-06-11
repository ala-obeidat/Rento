package com.epicloudtech.erent.activities;

import android.Manifest;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.net.Uri;
import android.os.Bundle;
import android.support.v4.app.ActivityCompat;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Base64;
import android.util.Log;
import android.view.View;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.bumptech.glide.Glide;
import com.bumptech.glide.request.RequestOptions;
import com.epicloudtech.erent.R;
import com.epicloudtech.erent.adapters.ImagesAdapter;
import com.epicloudtech.erent.helpers.SpacesItemDecoration;
import com.epicloudtech.erent.models.requests.BaseRequest;
import com.epicloudtech.erent.models.responses.DetailsItemResponse;
import com.epicloudtech.erent.models.responses.DetailsResponse;
import com.epicloudtech.erent.models.responses.ImageResponse;
import com.epicloudtech.erent.utils.Constants;
import com.epicloudtech.erent.utils.Utils;

import java.util.ArrayList;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;
import rx.Observer;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

public class CarDetails extends Base {

    @BindView(R.id.rvImages)
    RecyclerView rvImages;
    @BindView(R.id.tvType)
    TextView tvType;
    @BindView(R.id.tvModel)
    TextView tvModel;
    @BindView(R.id.tvYear)
    TextView tvYear;
    @BindView(R.id.tvColor)
    TextView tvColor;
    @BindView(R.id.tvProvider)
    TextView tvProvider;
    @BindView(R.id.ivBack)
    ImageView ivBack;
    @BindView(R.id.ivFav)
    ImageView ivFav;
    @BindView(R.id.tvBook)
    TextView tvBook;
    @BindView(R.id.carName)
    TextView carName;
    @BindView(R.id.tvDescription)
    TextView tvDescription;
    @BindView(R.id.tvKiloNumber)
    TextView tvKiloNumber;
    @BindView(R.id.tvKiloCost)
    TextView tvKiloCost;
    @BindView(R.id.tvKiloFree)
    TextView tvKiloFree;
    @BindView(R.id.tvDailyCost)
    TextView tvDailyCost;
    @BindView(R.id.tvWeeklyCost)
    TextView tvWeeklyCost;
    @BindView(R.id.tvMonthlyCost)
    TextView tvMonthlyCost;

    @BindView(R.id.tvNumber)
    TextView tvNumber;
    @BindView(R.id.tvMap)
    TextView tvMap;

    @BindView(R.id.officePhoneLay)
    LinearLayout officePhoneLay;


    @BindView(R.id.carImage)
    ImageView carImage;

    String carId = "";

    ArrayList<String> imageIds = new ArrayList<>();
    ArrayList<Bitmap> images = new ArrayList<>();

    DetailsItemResponse carDetails;
    ImagesAdapter adapter;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        Utils.refreshLocal(this);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_car_details);
        ButterKnife.bind(this);
        carId = getIntent().getStringExtra("CarId");
        carDetails = new DetailsItemResponse();
        carDetails();


        if (db.isFavourite(carId).equalsIgnoreCase("true")) {
            ivFav.setImageResource(R.drawable.ic_heart_on);
        } else {
            ivFav.setImageResource(R.drawable.ic_heart_off);
        }


    }


    @OnClick({R.id.ivBack, R.id.ivFav, R.id.tvBook, R.id.tvMap, R.id.tvNumber, R.id.officePhoneLay})
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.ivBack:
                finish();
                break;
            case R.id.ivFav:
                if (db.isFavourite(carId).equalsIgnoreCase("true")) {
                    ivFav.setImageResource(R.drawable.ic_heart_off);
                    db.updateItemFav(carId, "false");
                } else {
                    ivFav.setImageResource(R.drawable.ic_heart_on);
                    db.updateItemFav(carId, "true");
                }

                break;
            case R.id.tvBook:
                Intent goToBooking = new Intent(CarDetails.this, Booking.class);
                goToBooking.putExtra("CarIdFromBooking", carId);
                goToBooking.putExtra("dayCost", carDetails.getDayCost());
                goToBooking.putExtra("weekCost", carDetails.getWeekCost());
                goToBooking.putExtra("monthCost", carDetails.getMonthCost());
                goToBooking.putExtra("OfficeId", carDetails.getId());
                goToBooking.putExtra("officeFlag", carDetails.getOfficeFlag());
                startActivity(goToBooking);
                break;
            case R.id.tvMap:
                Utils.launchMaps(CarDetails.this, Double.parseDouble(carDetails.getLatitude()), Double.parseDouble(carDetails.getLongitude()));
                break;
            case R.id.tvNumber:
                Intent callIntent = new Intent(Intent.ACTION_CALL);
                callIntent.setData(Uri.parse("tel:" + carDetails.getOfficeMobile()));
                if (ActivityCompat.checkSelfPermission(this, Manifest.permission.CALL_PHONE) != PackageManager.PERMISSION_GRANTED) {

                    ActivityCompat.requestPermissions(this, new String[]{Manifest.permission.CALL_PHONE}, 102);
                    return;
                }
                startActivity(callIntent);

                break;

            case R.id.officePhoneLay:
                Intent ss = new Intent(Intent.ACTION_CALL);
                ss.setData(Uri.parse("tel:" + carDetails.getOfficeMobile()));
                if (ActivityCompat.checkSelfPermission(this, Manifest.permission.CALL_PHONE) != PackageManager.PERMISSION_GRANTED) {

                    ActivityCompat.requestPermissions(this, new String[]{Manifest.permission.CALL_PHONE}, 102);
                    return;
                }
                startActivity(ss);
                break;
        }
    }


    private void carDetails() {
        showLoading();
        BaseRequest<String> baseRequest = new BaseRequest<>();
        baseRequest.setData(carId);
        baseRequest.setLanguage(getLanguageEnum());
        baseRequest.setToken(getToken());
        baseRequest.setApplicationKey(Constants.APPLICATION_KEY);

        mAPIInterface.carDetails(baseRequest)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new Observer<DetailsResponse>() {
                    @Override
                    public void onCompleted() {
                        hideLoading();
                        RequestOptions options = new RequestOptions();
                        options.centerCrop();
                        String TypeNameEn = "";
                        String SubTypeNameEn = "";
                        if (carDetails.getTypeNameEn() != null) {
                            TypeNameEn = carDetails.getTypeNameEn().trim();
                        }
                        if (carDetails.getSubTypeNameEn() != null) {
                            SubTypeNameEn = carDetails.getSubTypeNameEn().trim();
                        }
                        String url = Constants.IMAGE_URL + TypeNameEn + "/" + SubTypeNameEn + ".png";
                        Glide.with(CarDetails.this).load(url).apply(options).into(carImage);

                        if (Utils.getValue(CarDetails.this, Constants.USER_LANGUAGE, Constants.ENGLISH_LANGUAGE).equals(Constants.ENGLISH_LANGUAGE)) {
                            carName.setText(carDetails.getTypeNameEn() + " " + carDetails.getSubTypeNameEn());
                            tvType.setText(carDetails.getTypeNameEn());
                            tvModel.setText(carDetails.getSubTypeNameEn());
                        } else {
                            carName.setText(carDetails.getTypeNameAr() + " " + carDetails.getSubTypeNameAr());
                            tvType.setText(carDetails.getTypeNameAr());
                            tvModel.setText(carDetails.getSubTypeNameAr());
                        }
                        try {
                            tvColor.setBackgroundColor(Color.parseColor(carDetails.getColor().trim()));
                        } catch (Exception e) {
                            Log.e("color parse", "onCompleted: ");
                        }
                        tvProvider.setText(carDetails.getOfficeName());
                        //    tvStatus.setText(carDetails.getStatus());
                        tvYear.setText(carDetails.getModel());
                        tvDescription.setText(carDetails.getDescription());
                        tvKiloNumber.setText(carDetails.getKiloNumber() + " " + getString(R.string.kilo));
                        tvKiloCost.setText(carDetails.getAdditinalKiloCost() + " " + getString(R.string.HALLAH));
                        tvKiloFree.setText(carDetails.getKiloLimit() + " " + getString(R.string.SAR));
                        tvDailyCost.setText(carDetails.getDayCost() + " " + getString(R.string.SAR));
                        tvWeeklyCost.setText(carDetails.getWeekCost() + " " + getString(R.string.SAR));
                        tvMonthlyCost.setText(carDetails.getMonthCost() + " " + getString(R.string.SAR));
                        tvNumber.setText(carDetails.getOfficeMobile());

                        imageIds = carDetails.getImageIds();

                        startImagesProcess();

                    }

                    @Override
                    public void onError(Throwable throwable) {
                        hideLoading();
                        showToast(throwable.getMessage(), ERROR);
                    }

                    @Override
                    public void onNext(DetailsResponse response) {
                        hideLoading();
                        if (response.isSuccess()) {
                            carDetails = response.getData();
                        } else {
                            showToast(response.getMessage(), ERROR);
                        }
                    }
                });
    }


    private void startImagesProcess() {
        if (imageIds != null && imageIds.size() > 0) {
            for (String s : imageIds) {
                carImage(s);
            }
        }
    }


    private void carImage(String imageId) {
        BaseRequest<String> baseRequest = new BaseRequest<>();
        baseRequest.setData(imageId);
        baseRequest.setLanguage(getLanguageEnum());
        baseRequest.setToken(getToken());
        baseRequest.setApplicationKey(Constants.APPLICATION_KEY);

        mAPIInterface.carImage(baseRequest)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new Observer<ImageResponse>() {
                    @Override
                    public void onCompleted() {


                    }

                    @Override
                    public void onError(Throwable throwable) {

                        showToast(throwable.getMessage(), ERROR);
                    }

                    @Override
                    public void onNext(ImageResponse response) {

                        if (response.isSuccess()) {

                            byte[] decodedString = Base64.decode(response.getData().getContent(), Base64.DEFAULT);
                            Bitmap decodedByte = BitmapFactory.decodeByteArray(decodedString, 0, decodedString.length);
                            images.add(decodedByte);
                            adapter = new ImagesAdapter(CarDetails.this, images);
                            rvImages.setLayoutManager(new LinearLayoutManager(CarDetails.this, LinearLayoutManager.HORIZONTAL, false));
                            rvImages.setNestedScrollingEnabled(false);
                            SpacesItemDecoration decoration = new SpacesItemDecoration(5, 5, 5, 5);
                            rvImages.addItemDecoration(decoration);
                            rvImages.setAdapter(adapter);

                        } else {
                            showToast(response.getMessage(), ERROR);
                        }
                    }
                });
    }


}
