package com.epicloudtech.erent.adapters;

import android.content.Context;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Filter;
import android.widget.Filterable;
import android.widget.TextView;

import com.epicloudtech.erent.R;
import com.epicloudtech.erent.interfaces.CitySelection;
import com.epicloudtech.erent.models.Entities.City;
import com.epicloudtech.erent.utils.Utils;

import java.util.ArrayList;

public class CitiesAdapter extends RecyclerView.Adapter implements Filterable {

    public Context activity;
    private ArrayList<City> list;
    private ArrayList<City> originalList;
    CitySelection callback;

    public CitiesAdapter(Context activity, ArrayList<City> list, ArrayList<City> originalList, CitySelection callback) {
        this.activity = activity;
        this.list = list;
        this.originalList = originalList;
        this.callback = callback;
    }

    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup viewGroup, int i) {
        Utils.refreshLocal(activity);
        View view;
        view = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.country_row, viewGroup, false);
        return new CitiesAdapter.MakeViewHolder(view);
    }

    @Override
    public void onBindViewHolder(final RecyclerView.ViewHolder viewHolder, final int i) {
        City city = list.get(i);
        ((CitiesAdapter.MakeViewHolder) viewHolder).tvCountryName.setText(Utils.getStringInLang(activity, city.getNameEn(), city.getName()));

        ((CitiesAdapter.MakeViewHolder) viewHolder).itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                callback.onSelect(list.get(i));
            }
        });
    }

    private Filter fRecords;

    @Override
    public Filter getFilter() {
        if (fRecords == null) {
            fRecords = new CitiesAdapter.RecordFilter();
        }
        return fRecords;
    }


    private class RecordFilter extends Filter {

        @Override
        protected FilterResults performFiltering(CharSequence constraint) {

            FilterResults results = new FilterResults();

            //Implement filter logic
            // if edittext is null return the actual list
            if (constraint == null || constraint.length() == 0) {
                //No need for filter
                results.values = list;
                results.count = list.size();

            } else {
                //Need Filter
                // it matches the text  entered in the edittext and set the data in adapter list
                ArrayList<City> fRecords = new ArrayList<>();

                for (City s : list) {
                    if (s.getName().toLowerCase().trim().contains(constraint.toString().toLowerCase().trim()) || s.getNameEn().toLowerCase().trim().contains(constraint.toString().toLowerCase().trim())) {
                        fRecords.add(s);
                    }
                }
                results.values = fRecords;
                results.count = fRecords.size();
            }
            return results;
        }

        @Override
        protected void publishResults(CharSequence constraint, FilterResults results) {
            //it set the data from filter to adapter list and refresh the recyclerview adapter
            list = (ArrayList<City>) results.values;
            if (list.size() > 0)
                notifyDataSetChanged();
            else {
                list = originalList;
            }
        }
    }

    @Override
    public int getItemCount() {
        return list.size();
    }

    public class MakeViewHolder extends RecyclerView.ViewHolder {

        TextView tvCountryName;

        public MakeViewHolder(View itemView) {
            super(itemView);
            this.tvCountryName = itemView.findViewById(R.id.tvCountryName);
        }
    }
}
