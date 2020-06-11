package com.epicloudtech.erent.adapters;

import android.annotation.TargetApi;
import android.content.Context;
import android.os.Build;
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
import com.epicloudtech.erent.models.responses.RequestItemResponse;
import com.epicloudtech.erent.utils.Constants;
import com.epicloudtech.erent.utils.Utils;

import java.util.ArrayList;

public class HistoryAdapter extends RecyclerView.Adapter<HistoryAdapter.ViewHolder> {

    private Context context;
    private int lastPosition = -1;
    ArrayList<RequestItemResponse> requests;

    public HistoryAdapter(Context context, ArrayList<RequestItemResponse> requests) {
        super();
        this.context = context;
        this.requests = requests;
    }

    @Override
    public HistoryAdapter.ViewHolder onCreateViewHolder(ViewGroup viewGroup, int i) {
        Utils.refreshLocal(context);
        View v = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.car_card, viewGroup, false);

        return new HistoryAdapter.ViewHolder(v);
    }

    @TargetApi(Build.VERSION_CODES.JELLY_BEAN)
    @Override
    public void onBindViewHolder(HistoryAdapter.ViewHolder viewHolder, final int position) {

        Animation animation = AnimationUtils.loadAnimation(context,
                (position > lastPosition) ? R.anim.up_from_bottom
                        : R.anim.down_from_top);
        viewHolder.itemView.startAnimation(animation);
        lastPosition = position;

        RequestItemResponse item = requests.get(position);
        if (Utils.getValue(context, Constants.USER_LANGUAGE, Constants.ENGLISH_LANGUAGE).equalsIgnoreCase(Constants.ENGLISH_LANGUAGE)) {
            viewHolder.carName.setText(item.getTypeNameEn() + " " + item.getSubTypeNameEn());
        } else {
            viewHolder.carName.setText(item.getTypeNameAr() + " " + item.getSubTypeNameAr());
        }
        RequestOptions options = new RequestOptions();
        options.fitCenter();
        String url = Constants.IMAGE_URL + item.getTypeNameEn().trim() + "/" + item.getSubTypeNameEn().trim() + ".png";
        Glide.with(context).load(url).apply(options).into(viewHolder.carImage);

        viewHolder.tvYear.setText(item.getModel());
        viewHolder.tvPrice.setText(item.getPrice() + " " + context.getString(R.string.SAR));

        viewHolder.tvType.setText(Utils.getStringInLang(context, item.getTypeNameEn(), item.getTypeNameAr()));
        viewHolder.tvModel.setText(Utils.getStringInLang(context, item.getSubTypeNameEn(), item.getSubTypeNameAr()));
        viewHolder.tvProvider.setText(item.getOfficeName());


    }

    @Override
    public void onViewDetachedFromWindow(HistoryAdapter.ViewHolder holder) {
        super.onViewDetachedFromWindow(holder);
        holder.itemView.clearAnimation();
    }

    @Override
    public int getItemCount() {
        return requests.size();
    }

    public class ViewHolder extends RecyclerView.ViewHolder {

        private final TextView carName, tvType, tvModel, tvYear, tvColor, tvProvider, tvBookNow, tvcall, tvFav;
        private ImageView carImage;
        private TextView tvPrice;

        ViewHolder(View v) {
            super(v);
            tvPrice = v.findViewById(R.id.tvPrice);
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
