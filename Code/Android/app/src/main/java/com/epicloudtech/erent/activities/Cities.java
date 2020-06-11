package com.epicloudtech.erent.activities;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.content.ContextCompat;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.epicloudtech.erent.R;
import com.epicloudtech.erent.adapters.CitiesAdapter;
import com.epicloudtech.erent.interfaces.CitySelection;
import com.epicloudtech.erent.models.Entities.City;
import com.epicloudtech.erent.models.requests.BaseRequest;
import com.epicloudtech.erent.models.responses.CitiesResponse;
import com.epicloudtech.erent.utils.Constants;
import com.epicloudtech.erent.utils.Utils;

import java.util.ArrayList;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;
import rx.Observer;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

public class Cities extends Base {

    @BindView(R.id.ll_back)
    LinearLayout ll_back;

    @BindView(R.id.edt_search)
    EditText edt_search;

    @BindView(R.id.tv_title)
    TextView tv_title;

    @BindView(R.id.ll_search_option)
    LinearLayout ll_search_option;

    @BindView(R.id.rvCities)
    RecyclerView rvCities;

    @BindView(R.id.iv_search)
    ImageView iv_search;

    private boolean isSearchMode = false;
    private ArrayList<City> cities = new ArrayList<>();
    public ArrayList<City> originalCities = new ArrayList<>();
    CitiesAdapter adapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        Utils.refreshLocal(this);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_cities);
        ButterKnife.bind(this);
        tv_title.setText(getString(R.string.choose_city));

        search(edt_search);

        getCities();
    }


    @OnClick({R.id.ll_back, R.id.ll_search_option})
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.ll_back:
                onBackPressed();
                break;
            case R.id.ll_search_option:
                if (isSearchMode) {
                    iv_search.setImageDrawable(ContextCompat.getDrawable(this, R.drawable.ic_search));
                    edt_search.setVisibility(View.INVISIBLE);
                    tv_title.setVisibility(View.VISIBLE);
                    isSearchMode = false;
                    handleResults();
                    showKeyboard(false);
                    edt_search.setText("");

                } else {
                    iv_search.setImageDrawable(ContextCompat.getDrawable(this, R.drawable.ic_x));
                    edt_search.setVisibility(View.VISIBLE);
                    tv_title.setVisibility(View.INVISIBLE);
                    isSearchMode = true;
                    showKeyboard(true);

                }
                break;
        }
    }


    private void handleResults() {
        if (cities.size() > 0) {
            LinearLayoutManager layoutManager = new LinearLayoutManager(this);
            rvCities.setLayoutManager(layoutManager);
            adapter = new CitiesAdapter(this, cities, originalCities, new CitySelection() {
                @Override
                public void onSelect(City city) {
                    Intent returnIntent = new Intent();
                    returnIntent.putExtra("selectedCity", city);
                    setResult(Activity.RESULT_OK, returnIntent);
                    finish();
                }
            });
            rvCities.setAdapter(adapter);
        }
    }

    private void search(EditText searchView) {

        searchView.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {

            }

            @Override
            public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {
                //  ItemsAdapter adapter = new ItemsAdapter(getActivity());
                if (adapter != null) {
                    if (charSequence.length() > 0)
                        adapter.getFilter().filter(charSequence);
                    else {
                        adapter = new CitiesAdapter(Cities.this, cities, originalCities, new CitySelection() {

                            @Override
                            public void onSelect(City city) {
                                Intent returnIntent = new Intent();
                                returnIntent.putExtra("selectedCity", city);
                                setResult(Activity.RESULT_OK, returnIntent);
                                finish();
                            }
                        });
                        LinearLayoutManager llm = new LinearLayoutManager(Cities.this);
                        rvCities.setLayoutManager(llm);
                        rvCities.setAdapter(adapter);
                    }
                }

            }

            @Override
            public void afterTextChanged(Editable editable) {

            }
        });

    }

    private void showKeyboard(boolean status) {
        InputMethodManager imm = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
        if (status) {
            assert imm != null;
            imm.showSoftInput(edt_search, InputMethodManager.SHOW_IMPLICIT);
        } else {
            assert imm != null;
            imm.hideSoftInputFromWindow(edt_search.getWindowToken(), 0);
        }
    }


    private void getCities() {


        showLoading();
        BaseRequest<String> request = new BaseRequest<>();
        request.setToken(Utils.getValue(Cities.this, Constants.USER_TOKEN, ""));
        request.setData("City");
        request.setApplicationKey(Constants.APPLICATION_KEY);
        request.setLanguage(getLanguageEnum());

        mAPIInterface.getCities(request)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new Observer<CitiesResponse>() {
                    @Override
                    public void onCompleted() {
                        hideLoading();
                        if (cities.size() > 0) {
                            rvCities.setLayoutManager(new LinearLayoutManager(Cities.this));

                            adapter = new CitiesAdapter(Cities.this, cities, originalCities, new CitySelection() {
                                @Override
                                public void onSelect(City city) {
                                    Intent returnIntent = new Intent();
                                    returnIntent.putExtra("selectedCity", city);
                                    setResult(Activity.RESULT_OK, returnIntent);
                                    finish();
                                }
                            });
                            rvCities.setAdapter(adapter);

                            handleResults();
                        }
                    }

                    @Override
                    public void onError(Throwable throwable) {
                        hideLoading();
                        showToast(throwable.getMessage(), ERROR);
                    }

                    @Override
                    public void onNext(CitiesResponse response) {
                        hideLoading();
                        if (response.isSuccess()) {
                            cities = response.getCities();
                        } else {
                            showToast(response.getMessage(), ERROR);
                        }
                    }
                });


    }


}
