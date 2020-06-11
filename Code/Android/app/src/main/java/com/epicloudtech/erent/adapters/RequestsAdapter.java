package com.epicloudtech.erent.adapters;

import android.annotation.TargetApi;
import android.content.Context;
import android.content.Intent;
import android.os.Build;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.ImageView;
import android.widget.TextView;

import com.balysv.materialripple.MaterialRippleLayout;
import com.bumptech.glide.Glide;
import com.bumptech.glide.request.RequestOptions;
import com.epicloudtech.erent.R;
import com.epicloudtech.erent.activities.Base;
import com.epicloudtech.erent.activities.CarDetails;
import com.epicloudtech.erent.activities.LoginRegister;
import com.epicloudtech.erent.dialogs.RateDialog;
import com.epicloudtech.erent.interfaces.RateCallback;
import com.epicloudtech.erent.models.requests.BaseRequest;
import com.epicloudtech.erent.models.requests.CloseBookingRequest;
import com.epicloudtech.erent.models.responses.BaseResponse;
import com.epicloudtech.erent.models.responses.RequestItemResponse;
import com.epicloudtech.erent.utils.Constants;
import com.epicloudtech.erent.utils.Utils;

import java.util.ArrayList;

import rx.Observer;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

public class RequestsAdapter extends RecyclerView.Adapter<RequestsAdapter.ViewHolder> {

    private Context context;
    private int lastPosition = -1;
    ArrayList<RequestItemResponse> requests;

    public RequestsAdapter(Context context, ArrayList<RequestItemResponse> requests) {
        super();
        this.context = context;
        this.requests = requests;
    }

    @Override
    public RequestsAdapter.ViewHolder onCreateViewHolder(ViewGroup viewGroup, int i) {
        Utils.refreshLocal(context);
        View v = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.request_card, viewGroup, false);

        return new RequestsAdapter.ViewHolder(v);
    }

    @TargetApi(Build.VERSION_CODES.JELLY_BEAN)
    @Override
    public void onBindViewHolder(RequestsAdapter.ViewHolder viewHolder, final int position) {

        Animation animation = AnimationUtils.loadAnimation(context,
                (position > lastPosition) ? R.anim.up_from_bottom
                        : R.anim.down_from_top);
        viewHolder.itemView.startAnimation(animation);
        lastPosition = position;
        final RequestItemResponse item = requests.get(position);
        if (Utils.getValue(context, Constants.USER_LANGUAGE, Constants.ENGLISH_LANGUAGE).equalsIgnoreCase(Constants.ENGLISH_LANGUAGE)) {
            viewHolder.carName.setText(item.getTypeNameEn() + " " + item.getSubTypeNameEn());
        } else {
            viewHolder.carName.setText(item.getTypeNameAr() + " " + item.getSubTypeNameAr());
        }
        RequestOptions options = new RequestOptions();
        options.fitCenter();
        String url = Constants.IMAGE_URL + item.getTypeNameEn().trim() + "/" + item.getSubTypeNameEn().trim() + ".png";
        Glide.with(context).load(url).apply(options).into(viewHolder.carImage);

        viewHolder.tvModel.setText(item.getModel());

        viewHolder.tvDate.setText(item.getCreateDate().substring(0, 10));
        viewHolder.tvTime.setText(item.getCreateDate().substring(12, 19));
        viewHolder.tvStatus.setText(getStatusName(item.getAction()));

        if (item.getAction().equalsIgnoreCase("2")) {
            viewHolder.bookNowLay.setVisibility(View.VISIBLE);
        } else {
            viewHolder.bookNowLay.setVisibility(View.GONE);
        }

        viewHolder.tvCloseBooking.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {


                if (Utils.getValue(context, Constants.IS_REGISTERED, false)) {
                    RateDialog dialog = new RateDialog(context, new RateCallback() {
                        @Override
                        public void onSend(String status, String rate, String comment) {
                            ((Base) context).showLoading();
                            BaseRequest<CloseBookingRequest> baseRequest = new BaseRequest<>();
                            CloseBookingRequest request = new CloseBookingRequest();
                            request.setCheckoutId(item.getId());
                            request.setComment(comment);
                            request.setFlag(status);
                            request.setStar(rate);

                            baseRequest.setData(request);
                            baseRequest.setLanguage(((Base) context).getLanguageEnum());
                            baseRequest.setToken(((Base) context).getToken());
                            baseRequest.setApplicationKey(Constants.APPLICATION_KEY);

                            ((Base) context).mAPIInterface.closeBooking(baseRequest)
                                    .subscribeOn(Schedulers.io())
                                    .observeOn(AndroidSchedulers.mainThread())
                                    .subscribe(new Observer<BaseResponse>() {
                                        @Override
                                        public void onCompleted() {
                                            ((Base) context).hideLoading();
                                        }

                                        @Override
                                        public void onError(Throwable throwable) {
                                            ((Base) context).hideLoading();
                                            ((Base) context).showToast(throwable.getMessage(), ((Base) context).ERROR);
                                        }

                                        @Override
                                        public void onNext(BaseResponse response) {
                                            ((Base) context).hideLoading();
                                            if (response.isSuccess()) {
                                                ((Base) context).showToast(context.getString(R.string.booking_closed), ((Base) context).SUCCESS);
                                            } else {
                                                ((Base) context).showToast(response.getMessage(), ((Base) context).ERROR);
                                            }
                                        }
                                    });
                        }
                    });
                    dialog.show();
                } else {
                    Utils.goToActivityWithAnimation(context, LoginRegister.class, true);
                }

            }
        });

    }

    @Override
    public void onViewDetachedFromWindow(RequestsAdapter.ViewHolder holder) {
        super.onViewDetachedFromWindow(holder);
        holder.itemView.clearAnimation();
    }

    private String getStatusName(String status) {
        String name = "";
        switch (status) {
            case "0":
                name = context.getString(R.string.pending);
                break;
            case "1":
                name = context.getString(R.string.processing);
                break;
            case "2":
                name = context.getString(R.string.approved);
                break;
            case "3":
                name = context.getString(R.string.on_the_way);
                break;
            case "4":
                name = context.getString(R.string.delivered);
                break;
            case "5":
                name = context.getString(R.string.done);
                break;
            case "6":
                name = context.getString(R.string.rejected);
                break;

        }
        return name;
    }

    @Override
    public int getItemCount() {
        return requests.size();
    }

    public class ViewHolder extends RecyclerView.ViewHolder {

        private final TextView carName, tvDate, tvModel, tvTime, tvStatus, tvCloseBooking, tvAlert;
        private ImageView carImage;
        private MaterialRippleLayout bookNowLay;

        ViewHolder(View v) {
            super(v);
            bookNowLay = v.findViewById(R.id.bookNowLay);
            carName = v.findViewById(R.id.carName);
            tvDate = v.findViewById(R.id.tvDate);
            tvModel = v.findViewById(R.id.tvModel);
            tvTime = v.findViewById(R.id.tvTime);
            tvStatus = v.findViewById(R.id.tvStatus);
            tvCloseBooking = v.findViewById(R.id.tvCloseBooking);
            tvAlert = v.findViewById(R.id.tvAlert);
            carImage = v.findViewById(R.id.carImage);
        }
    }
}
