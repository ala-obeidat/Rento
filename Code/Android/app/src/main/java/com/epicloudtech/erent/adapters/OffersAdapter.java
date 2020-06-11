package com.epicloudtech.erent.adapters;

import android.annotation.TargetApi;
import android.content.Context;
import android.content.Intent;
import android.os.Build;
import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
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
import com.epicloudtech.erent.models.responses.OfferItemResponse;
import com.epicloudtech.erent.utils.Constants;
import com.epicloudtech.erent.utils.Utils;

import java.util.ArrayList;

public class OffersAdapter extends RecyclerView.Adapter<OffersAdapter.ViewHolder> {

    private Context context;
    private int lastPosition = -1;
    ArrayList<OfferItemResponse> offers;

    public OffersAdapter(Context context, ArrayList<OfferItemResponse> offers) {
        super();
        this.context = context;
        this.offers = offers;
    }

    @Override
    public OffersAdapter.ViewHolder onCreateViewHolder(ViewGroup viewGroup, int i) {
        Utils.refreshLocal(context);
        View v = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.offer_card, viewGroup, false);

        return new OffersAdapter.ViewHolder(v);
    }

    @TargetApi(Build.VERSION_CODES.JELLY_BEAN)
    @Override
    public void onBindViewHolder(OffersAdapter.ViewHolder viewHolder, final int position) {

        Animation animation = AnimationUtils.loadAnimation(context,
                (position > lastPosition) ? R.anim.up_from_bottom
                        : R.anim.down_from_top);
        viewHolder.itemView.startAnimation(animation);
        lastPosition = position;
        final OfferItemResponse offer = offers.get(position);

        if (Utils.getValue(context, Constants.USER_LANGUAGE, Constants.ENGLISH_LANGUAGE).equalsIgnoreCase(Constants.ENGLISH_LANGUAGE)) {
            viewHolder.carName.setText(offer.getTypeNameEn() + " " + offer.getSubTypeNameEn());
        } else {
            viewHolder.carName.setText(offer.getTypeNameAr() + " " + offer.getSubTypeNameAr());
        }

        RequestOptions options = new RequestOptions();
        options.fitCenter();
        String url = Constants.IMAGE_URL + offer.getTypeNameEn().trim() + "/" + offer.getSubTypeNameEn().trim() + ".png";
        Glide.with(context).load(url).apply(options).into(viewHolder.carImage);

        viewHolder.tvPrice.setText(offer.getCost() + " " + context.getString(R.string.SAR));
        viewHolder.tvDate.setText(offer.getCreateDate().substring(0, 10));
        viewHolder.tvModel.setText(offer.getCarModel());
        viewHolder.tvOffice.setText(offer.getProviderName());

        viewHolder.frame.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
//                Intent goToDetails = new Intent(context, CarDetails.class);
//                goToDetails.putExtra("CarId", offer.getId());
//                context.startActivity(goToDetails);
            }
        });


    }

    @Override
    public void onViewDetachedFromWindow(OffersAdapter.ViewHolder holder) {
        super.onViewDetachedFromWindow(holder);
        holder.itemView.clearAnimation();
    }

    @Override
    public int getItemCount() {
        return offers.size();
    }

    public class ViewHolder extends RecyclerView.ViewHolder {

        private final TextView carName, tvDate, tvModel, tvPrice, tvOffice;
        private ImageView carImage;
        private CardView frame;

        ViewHolder(View v) {
            super(v);
            frame = v.findViewById(R.id.frame);
            carName = v.findViewById(R.id.carName);
            tvDate = v.findViewById(R.id.tvDate);
            tvModel = v.findViewById(R.id.tvModel);
            tvPrice = v.findViewById(R.id.tvPrice);
            tvOffice = v.findViewById(R.id.tvOffice);
            carImage = v.findViewById(R.id.carImage);
        }
    }
}
