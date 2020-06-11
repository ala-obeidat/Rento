package com.epicloudtech.erent.fragments;


import android.app.Fragment;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;

import com.epicloudtech.erent.R;
import com.epicloudtech.erent.adapters.FavouritesAdapter;
import com.epicloudtech.erent.database.DatabaseOperation;
import com.epicloudtech.erent.helpers.SpacesItemDecoration;
import com.epicloudtech.erent.utils.Utils;

import butterknife.BindView;
import butterknife.ButterKnife;

/**
 * A simple {@link Fragment} subclass.
 */
public class Favourites extends Fragment {

    @BindView(R.id.rvFavourites)
    RecyclerView rvFavourites;

    private FavouritesAdapter mAdapter;

    DatabaseOperation db;

    public Favourites() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        Utils.refreshLocal(getActivity());
        return inflater.inflate(R.layout.fragment_favourites, container, false);
    }


    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        ButterKnife.bind(this, view);
        db = DatabaseOperation.getInstance(getActivity());
        setupFavouritesAdapter();
    }


    private void setupFavouritesAdapter() {
        rvFavourites.setLayoutManager(new LinearLayoutManager(getActivity()));
        rvFavourites.setNestedScrollingEnabled(false);
        SpacesItemDecoration decoration = new SpacesItemDecoration(0, 25, 0, 0);

        rvFavourites.addItemDecoration(decoration);

        mAdapter = new FavouritesAdapter(getActivity(),db.getItemsByFav());

        rvFavourites.setAdapter(mAdapter);
    }

}
