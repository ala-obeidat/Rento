package com.epicloudtech.erent.fragments;


import android.content.Context;
import android.os.Bundle;
import android.app.Fragment;
import android.support.annotation.Nullable;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;

import com.epicloudtech.erent.R;
import com.epicloudtech.erent.activities.Base;
import com.epicloudtech.erent.adapters.FavouritesAdapter;
import com.epicloudtech.erent.adapters.HistoryAdapter;
import com.epicloudtech.erent.dialogs.LoginDialog;
import com.epicloudtech.erent.helpers.SpacesItemDecoration;
import com.epicloudtech.erent.models.requests.BaseRequest;
import com.epicloudtech.erent.models.responses.RequestItemResponse;
import com.epicloudtech.erent.models.responses.RequestsResponse;
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
public class History extends Fragment {

    @BindView(R.id.rvHistory)
    RecyclerView rvHistory;

    Context activity;

    private HistoryAdapter mAdapter;

    ArrayList<RequestItemResponse> requests = new ArrayList<>();
    public History() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        Utils.refreshLocal(getActivity());
        return inflater.inflate(R.layout.fragment_history, container, false);
    }


    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        ButterKnife.bind(this, view);
        activity = getActivity();
        if (Utils.getValue(getActivity(), Constants.IS_REGISTERED, false)) {
            userHistory();
        }
    }


    private void userHistory() {
        ((Base) activity).showLoading();
        BaseRequest<Boolean> baseRequest = new BaseRequest<>();
        baseRequest.setData(true);
        baseRequest.setLanguage(((Base) activity).getLanguageEnum());
        baseRequest.setToken(((Base) activity).getToken());
        baseRequest.setApplicationKey(Constants.APPLICATION_KEY);

        ((Base) activity).mAPIInterface.userRequests(baseRequest)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new Observer<RequestsResponse>() {
                    @Override
                    public void onCompleted() {
                        ((Base) activity).hideLoading();
                        if (requests.size() > 0)
                            setupHistoryAdapter();
                    }

                    @Override
                    public void onError(Throwable throwable) {
                        ((Base) activity).hideLoading();
                        ((Base) activity).showToast(throwable.getMessage(), ((Base) activity).ERROR);
                    }

                    @Override
                    public void onNext(RequestsResponse response) {
                        ((Base) activity).hideLoading();
                        if (response.isSuccess()) {
                            requests = response.getData();
                        } else {
                            ((Base) activity).showToast(response.getMessage(), ((Base) activity).ERROR);
                        }
                    }
                });
    }


    private void setupHistoryAdapter() {
        rvHistory.setLayoutManager(new LinearLayoutManager(getActivity()));
        rvHistory.setNestedScrollingEnabled(false);
        SpacesItemDecoration decoration = new SpacesItemDecoration(0, 25, 0, 0);

        rvHistory.addItemDecoration(decoration);

        mAdapter = new HistoryAdapter(getActivity(),requests);

        rvHistory.setAdapter(mAdapter);
    }

}
