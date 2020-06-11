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
import com.epicloudtech.erent.adapters.RequestsAdapter;
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
public class Requests extends Fragment {


    @BindView(R.id.rvRequests)
    RecyclerView rvRequests;

    @BindView(R.id.ivSearch)
    ImageView ivSearch;

    private RequestsAdapter mAdapter;

    Context activity;
    ArrayList<RequestItemResponse> requests = new ArrayList<>();

    public Requests() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        activity = getActivity();
        Utils.refreshLocal(getActivity());
        return inflater.inflate(R.layout.fragment_requests, container, false);
    }


    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        ButterKnife.bind(this, view);

        ivSearch.setVisibility(View.GONE);
        if (Utils.getValue(getActivity(), Constants.IS_REGISTERED, false)) {
            userRequests();
        }else {
            LoginDialog dialog = new LoginDialog(getActivity());
            dialog.show();
        }
    }


    private void setupRequestsAdapter() {
        rvRequests.setLayoutManager(new LinearLayoutManager(getActivity()));
        rvRequests.setNestedScrollingEnabled(false);
        SpacesItemDecoration decoration = new SpacesItemDecoration(0, 25, 0, 0);

        rvRequests.addItemDecoration(decoration);

        mAdapter = new RequestsAdapter(getActivity(),requests);

        rvRequests.setAdapter(mAdapter);
    }


    private void userRequests() {
        ((Base) activity).showLoading();
        BaseRequest<Boolean> baseRequest = new BaseRequest<>();
        baseRequest.setData(false);
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
                            setupRequestsAdapter();
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


}
