package com.epicloudtech.erent.adapters;

import android.content.Context;
import android.graphics.Color;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.epicloudtech.erent.R;
import com.epicloudtech.erent.interfaces.ColorSelection;
import com.epicloudtech.erent.utils.Utils;

import java.util.ArrayList;

public class ColorsAdapter extends RecyclerView.Adapter {

    public Context activity;
    private ArrayList<String> list;
    ColorSelection callback;

    public ColorsAdapter(Context activity, ArrayList<String> list, ColorSelection callback) {
        this.activity = activity;
        this.list = list;
        this.callback = callback;
    }

    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup viewGroup, int i) {
        Utils.refreshLocal(activity);
        View view;
        view = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.color_row, viewGroup, false);
        return new ColorsAdapter.MakeViewHolder(view);
    }

    @Override
    public void onBindViewHolder(final RecyclerView.ViewHolder viewHolder, final int i) {
        String color = list.get(i);
        try {
            ((MakeViewHolder) viewHolder).tvColor.setBackgroundColor(Color.parseColor(color.trim()));
        } catch (Exception e) {
            Log.e("color parse", "onBindViewHolder: ");
        }

        ((ColorsAdapter.MakeViewHolder) viewHolder).itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                callback.onSelect(list.get(i));
            }
        });
    }

    @Override
    public int getItemCount() {
        return list.size();
    }

    public class MakeViewHolder extends RecyclerView.ViewHolder {

        TextView tvColor;

        public MakeViewHolder(View itemView) {
            super(itemView);
            this.tvColor = itemView.findViewById(R.id.tvColor);
        }
    }
}
