package com.epicloudtech.erent.adapters;

import android.annotation.TargetApi;
import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.os.Build;
import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.ImageView;
import android.widget.TextView;

import com.bumptech.glide.Glide;
import com.bumptech.glide.request.RequestOptions;
import com.epicloudtech.erent.R;
import com.epicloudtech.erent.activities.CarDetails;
import com.epicloudtech.erent.activities.LoginRegister;
import com.epicloudtech.erent.dialogs.LoginDialog;
import com.epicloudtech.erent.models.responses.SearchItemResponse;
import com.epicloudtech.erent.utils.Constants;
import com.epicloudtech.erent.utils.Utils;

import java.util.ArrayList;

public class FavouritesAdapter extends RecyclerView.Adapter<FavouritesAdapter.ViewHolder> {

    private Context context;
    private int lastPosition = -1;
    ArrayList<SearchItemResponse> cars;

    public FavouritesAdapter(Context context, ArrayList<SearchItemResponse> cars) {
        super();
        this.context = context;
        this.cars = cars;
    }

    @Override
    public FavouritesAdapter.ViewHolder onCreateViewHolder(ViewGroup viewGroup, int i) {
        Utils.refreshLocal(context);
        View v = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.car_card, viewGroup, false);

        return new FavouritesAdapter.ViewHolder(v);
    }

    @TargetApi(Build.VERSION_CODES.JELLY_BEAN)
    @Override
    public void onBindViewHolder(FavouritesAdapter.ViewHolder viewHolder, final int position) {

        Animation animation = AnimationUtils.loadAnimation(context,
                (position > lastPosition) ? R.anim.up_from_bottom
                        : R.anim.down_from_top);
        viewHolder.itemView.startAnimation(animation);
        lastPosition = position;

        final SearchItemResponse car = cars.get(position);

        if (Utils.getValue(context, Constants.USER_LANGUAGE, Constants.ENGLISH_LANGUAGE).equals(Constants.ENGLISH_LANGUAGE)) {
            viewHolder.tvType.setText(car.getTypeNameEn());
            viewHolder.tvModel.setText(car.getSubTypeNameEn());
            viewHolder.carName.setText(car.getTypeNameEn().trim() + " " + car.getSubTypeNameEn().trim());
        } else {
            viewHolder.tvType.setText(car.getTypeNameAr());
            viewHolder.tvModel.setText(car.getSubTypeNameAr());
            viewHolder.carName.setText(car.getTypeNameAr().trim() + " " + car.getSubTypeNameAr().trim());
        }

        viewHolder.tvYear.setText(car.getModel());
        viewHolder.tvProvider.setText(car.getOfficeName());
        try {
            viewHolder.tvColor.setBackgroundColor(Color.parseColor(car.getColor().trim()));
        } catch (Exception e) {
            Log.e("color parse", "onBindViewHolder: ");
        }

        viewHolder.tvProvider.setText(car.getOfficeName());


        RequestOptions options = new RequestOptions();
        options.fitCenter();
        String url = Constants.IMAGE_URL + car.getTypeNameEn().trim() + "/" + car.getSubTypeNameEn().trim() + ".png";
        Glide.with(context).load(url).apply(options).into(viewHolder.carImage);

        viewHolder.frame.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (Utils.getValue(context, Constants.IS_REGISTERED, false)) {
                    Intent goToDetails = new Intent(context, CarDetails.class);
                    goToDetails.putExtra("CarId", car.getId());
                    context.startActivity(goToDetails);
                }else {
                    LoginDialog dialog = new LoginDialog(context);
                    dialog.show();
                }
            }
        });


    }

    @Override
    public void onViewDetachedFromWindow(FavouritesAdapter.ViewHolder holder) {
        super.onViewDetachedFromWindow(holder);
        holder.itemView.clearAnimation();
    }

    @Override
    public int getItemCount() {
        return cars.size();
    }

    public class ViewHolder extends RecyclerView.ViewHolder {

        private final TextView carName, tvType, tvModel, tvYear, tvColor, tvProvider, tvBookNow, tvcall, tvFav;
        private ImageView carImage;
        private CardView frame;

        ViewHolder(View v) {
            super(v);
            frame = v.findViewById(R.id.frame);
            carName = v.findViewById(R.id.carName);
            tvType = v.findViewById(R.id.tvType);
            tvModel = v.findViewById(R.id.tvModel);
            tvYear = v.findViewById(R.id.tvYear);
            tvColor = v.findViewById(R.id.tvColor);
            tvProvider = v.findViewById(R.id.tvProvider);
            tvBookNow = v.findViewById(R.id.tvBookNow);
            tvcall = v.findViewById(R.id.tvcall);
            tvFav = v.findViewById(R.id.tvFav);
            carImage = v.findViewById(R.id.carImage);

        }
    }
}
