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
import com.epicloudtech.erent.adapters.CountriesAdapter;
import com.epicloudtech.erent.interfaces.CountrySelection;
import com.epicloudtech.erent.models.Entities.Country;
import com.epicloudtech.erent.models.requests.BaseRequest;
import com.epicloudtech.erent.models.requests.CountryRequest;
import com.epicloudtech.erent.models.responses.CountriesResponse;
import com.epicloudtech.erent.utils.Constants;
import com.epicloudtech.erent.utils.Utils;

import java.util.ArrayList;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;
import rx.Observer;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

public class Countries extends Base {


    @BindView(R.id.ll_back)
    LinearLayout ll_back;

    @BindView(R.id.edt_search)
    EditText edt_search;

    @BindView(R.id.tv_title)
    TextView tv_title;

    @BindView(R.id.ll_search_option)
    LinearLayout ll_search_option;

    @BindView(R.id.rvCountries)
    RecyclerView rvCountries;

    @BindView(R.id.iv_search)
    ImageView iv_search;

    private boolean isSearchMode = false;
    private ArrayList<Country> countries = new ArrayList<>();
    public ArrayList<Country> originalCountries = new ArrayList<>();
    CountriesAdapter adapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        Utils.refreshLocal(this);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_countries);
        ButterKnife.bind(this);
        tv_title.setText(getString(R.string.choose_country));

        search(edt_search);

        getCountries();
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
        if (countries.size() > 0) {
            LinearLayoutManager layoutManager = new LinearLayoutManager(this);
            rvCountries.setLayoutManager(layoutManager);
            adapter = new CountriesAdapter(this, countries, originalCountries, new CountrySelection() {
                @Override
                public void onSelect(Country country) {
                    Intent returnIntent = new Intent();
                    returnIntent.putExtra("selectedCountry", country);
                    setResult(Activity.RESULT_OK, returnIntent);
                    finish();
                }
            });
            rvCountries.setAdapter(adapter);
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
                        adapter = new CountriesAdapter(Countries.this, countries, originalCountries, new CountrySelection() {

                            @Override
                            public void onSelect(Country country) {
                                Intent returnIntent = new Intent();
                                returnIntent.putExtra("selectedCountry", country);
                                setResult(Activity.RESULT_OK, returnIntent);
                                finish();
                            }
                        });
                        LinearLayoutManager llm = new LinearLayoutManager(Countries.this);
                        rvCountries.setLayoutManager(llm);
                        rvCountries.setAdapter(adapter);
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


    private void getCountries() {


        showLoading();
        BaseRequest<String> request = new BaseRequest<>();
        request.setToken(Utils.getValue(Countries.this, Constants.USER_TOKEN, ""));
        request.setData("Country");
        request.setApplicationKey(Constants.APPLICATION_KEY);
        request.setLanguage(getLanguageEnum());

        mAPIInterface.getCountries(request)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new Observer<CountriesResponse>() {
                    @Override
                    public void onCompleted() {
                        hideLoading();
                        if (countries.size() > 0) {
                            rvCountries.setLayoutManager(new LinearLayoutManager(Countries.this));

                            adapter = new CountriesAdapter(Countries.this, countries, originalCountries, new CountrySelection() {
                                @Override
                                public void onSelect(Country country) {
                                    Intent returnIntent = new Intent();
                                    returnIntent.putExtra("selectedCountry", country);
                                    setResult(Activity.RESULT_OK, returnIntent);
                                    finish();
                                }
                            });
                            rvCountries.setAdapter(adapter);
                            handleResults();
                        }

                    }

                    @Override
                    public void onError(Throwable throwable) {
                        hideLoading();
                        showToast(throwable.getMessage(), ERROR);
                    }

                    @Override
                    public void onNext(CountriesResponse response) {
                        hideLoading();
                        if (response.isSuccess()) {
                            countries = response.getCountries();
                        } else {
                            showToast(response.getMessage(), ERROR);
                        }
                    }
                });


    }

}
