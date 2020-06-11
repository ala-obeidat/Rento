package com.epicloudtech.erent.fragments;


import android.app.Fragment;
import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;

import com.epicloudtech.erent.R;
import com.epicloudtech.erent.activities.Base;
import com.epicloudtech.erent.activities.Search;
import com.epicloudtech.erent.adapters.CarsAdapter;
import com.epicloudtech.erent.database.DatabaseOperation;
import com.epicloudtech.erent.helpers.SpacesItemDecoration;
import com.epicloudtech.erent.models.requests.BaseRequest;
import com.epicloudtech.erent.models.requests.SearchRequest;
import com.epicloudtech.erent.models.responses.SearchItemResponse;
import com.epicloudtech.erent.models.responses.SearchResponse;
import com.epicloudtech.erent.utils.Constants;
import com.epicloudtech.erent.utils.Utils;

import java.util.ArrayList;

import butterknife.BindView;
import butterknife.ButterKnife;
import rx.Observer;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

/**
 * A simple {@link Fragment} subclass.
 */
public class Home extends Fragment {

    @BindView(R.id.rvCars)
    RecyclerView rvCars;

    @BindView(R.id.ivSearch)
    ImageView ivSearch;

    ArrayList<SearchItemResponse> cars = new ArrayList<>();
    Context activity;

    DatabaseOperation db;


    public Home() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        activity = getActivity();
        Utils.refreshLocal(getActivity());
        db = DatabaseOperation.getInstance(getActivity());
        return inflater.inflate(R.layout.fragment_home, container, false);
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        ButterKnife.bind(this, view);
        ivSearch.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Utils.goToActivityWithAnimation(getActivity(), Search.class, false);
            }
        });

        searchCars();

    }


    private void searchCars() {
        ((Base) activity).showLoading();
        BaseRequest<SearchRequest> baseRequest = new BaseRequest<>();
        SearchRequest request = new SearchRequest();
        request.setCityId(Utils.getValue(getActivity(), Constants.USER_CITY_ID, "1"));

        baseRequest.setData(request);
        baseRequest.setLanguage(((Base) activity).getLanguageEnum());
        baseRequest.setToken(((Base) activity).getToken());
        baseRequest.setApplicationKey(Constants.APPLICATION_KEY);

        ((Base) activity).mAPIInterface.searchCars(baseRequest)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new Observer<SearchResponse>() {
                    @Override
                    public void onCompleted() {
                        ((Base) activity).hideLoading();
                        if (cars.size() > 0) {
                            setupCarsAdapter();
                        }
                    }

                    @Override
                    public void onError(Throwable throwable) {
                        ((Base) activity).hideLoading();
                        ((Base) activity).showToast(throwable.getMessage(), ((Base) activity).ERROR);
                    }

                    @Override
                    public void onNext(SearchResponse response) {
                        ((Base) activity).hideLoading();
                        if (response.isSuccess()) {
                            cars = response.getData();

                            for (SearchItemResponse car : cars) {
                                if (db.isTableEmpty(car.getId())) {
                                    db.insertItem(car);
                                }
                            }

                        } else {
                            ((Base) activity).showToast(response.getMessage(), ((Base) activity).ERROR);
                        }
                    }
                });
    }


    private void setupCarsAdapter() {
        rvCars.setLayoutManager(new LinearLayoutManager(getActivity()));
        rvCars.setNestedScrollingEnabled(false);
        SpacesItemDecoration decoration = new SpacesItemDecoration(0, 25, 0, 0);

        rvCars.addItemDecoration(decoration);

        CarsAdapter mAdapter = new CarsAdapter(getActivity(), cars);

        rvCars.setAdapter(mAdapter);
    }


}
