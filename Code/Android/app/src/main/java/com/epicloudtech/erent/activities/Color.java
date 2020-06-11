package com.epicloudtech.erent.activities;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.epicloudtech.erent.R;
import com.epicloudtech.erent.adapters.ColorsAdapter;
import com.epicloudtech.erent.interfaces.ColorSelection;
import com.epicloudtech.erent.utils.Utils;

import java.util.ArrayList;

import butterknife.BindView;
import butterknife.ButterKnife;

public class Color extends Base {

    @BindView(R.id.ll_back)
    LinearLayout ll_back;
    @BindView(R.id.ll_search_option)
    LinearLayout ll_search_option;

    @BindView(R.id.tv_title)
    TextView tv_title;

    @BindView(R.id.rvCountries)
    RecyclerView rvCountries;

    private ArrayList<String> colors = new ArrayList<>();
    ColorsAdapter adapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        Utils.refreshLocal(this);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_color);
        ButterKnife.bind(this);
        tv_title.setText(getString(R.string.choose_color));
        ll_search_option.setVisibility(View.GONE);

        setupColors();

        getColors();

        ll_back.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });

    }

    private void getColors() {

        LinearLayoutManager layoutManager = new LinearLayoutManager(this);
        rvCountries.setLayoutManager(layoutManager);
        adapter = new ColorsAdapter(Color.this, colors, new ColorSelection() {

            @Override
            public void onSelect(String color) {
                Intent returnIntent = new Intent();
                returnIntent.putExtra("selectedColor", color);
                setResult(Activity.RESULT_OK, returnIntent);
                finish();
            }
        });
        rvCountries.setAdapter(adapter);
    }

    private void setupColors() {

        colors.add("#ffffff");
        colors.add("#000000");
        colors.add("#ff0000");
        colors.add("#00ff00");
        colors.add("#0000ff");
        colors.add("#686868");

    }
}
