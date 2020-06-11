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
import com.epicloudtech.erent.adapters.OffersAdapter;
import com.epicloudtech.erent.helpers.SpacesItemDecoration;
import com.epicloudtech.erent.models.requests.BaseRequest;
import com.epicloudtech.erent.models.responses.OfferItemResponse;
import com.epicloudtech.erent.models.responses.OfferResponse;
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
public class Offers extends Fragment {

    @BindView(R.id.rvOffers)
    RecyclerView rvOffers;

    @BindView(R.id.ivSearch)
    ImageView ivSearch;

    Context activity;

    private OffersAdapter mAdapter;

    ArrayList<OfferItemResponse> offers = new ArrayList<>();

    public Offers() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        activity = getActivity();
        Utils.refreshLocal(getActivity());
        return inflater.inflate(R.layout.fragment_offers, container, false);
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        ButterKnife.bind(this, view);

        ivSearch.setVisibility(View.GONE);

        carOffers();

    }


    private void carOffers() {
        ((Base) activity).showLoading();
        BaseRequest<String> baseRequest = new BaseRequest<>();
        baseRequest.setData("");
        baseRequest.setLanguage(((Base) activity).getLanguageEnum());
        baseRequest.setToken(((Base) activity).getToken());
        baseRequest.setApplicationKey(Constants.APPLICATION_KEY);

        ((Base) activity).mAPIInterface.carOffers(baseRequest)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new Observer<OfferResponse>() {
                    @Override
                    public void onCompleted() {
                        ((Base) activity).hideLoading();

                        if (offers.size() > 0)
                            setupOffersAdapter();
                    }

                    @Override
                    public void onError(Throwable throwable) {
                        ((Base) activity).hideLoading();
                        ((Base) activity).showToast(throwable.getMessage(), ((Base) activity).ERROR);
                    }

                    @Override
                    public void onNext(OfferResponse response) {
                        ((Base) activity).hideLoading();
                        if (response.isSuccess()) {
                            offers = response.getData();
                        } else {
                            ((Base) activity).showToast(response.getMessage(), ((Base) activity).ERROR);
                        }
                    }
                });
    }


    private void setupOffersAdapter() {
        rvOffers.setLayoutManager(new LinearLayoutManager(getActivity()));
        rvOffers.setNestedScrollingEnabled(false);
        SpacesItemDecoration decoration = new SpacesItemDecoration(0, 25, 0, 0);

        rvOffers.addItemDecoration(decoration);

        mAdapter = new OffersAdapter(getActivity(), offers);

        rvOffers.setAdapter(mAdapter);
    }


}
