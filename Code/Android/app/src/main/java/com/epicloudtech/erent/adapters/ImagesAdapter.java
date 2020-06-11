package com.epicloudtech.erent.adapters;

import android.annotation.TargetApi;
import android.app.Activity;
import android.graphics.Bitmap;
import android.os.Build;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;

import com.epicloudtech.erent.R;
import com.epicloudtech.erent.utils.Utils;

import java.util.ArrayList;

public class ImagesAdapter extends RecyclerView.Adapter<ImagesAdapter.ViewHolder> {

    private Activity activity;
    ArrayList<Bitmap> images;

    public ImagesAdapter(Activity activity, ArrayList<Bitmap> images) {
        super();
        this.activity = activity;
        this.images = images;
    }

    @Override
    public ImagesAdapter.ViewHolder onCreateViewHolder(ViewGroup viewGroup, int i) {
        Utils.refreshLocal(activity);
        View v = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.image_card, viewGroup, false);

        return new ImagesAdapter.ViewHolder(v);
    }

    @TargetApi(Build.VERSION_CODES.JELLY_BEAN)
    @Override
    public void onBindViewHolder(final ImagesAdapter.ViewHolder viewHolder, final int i) {

        viewHolder.carImage.setImageBitmap(images.get(i));

    }


    @Override
    public int getItemCount() {
        try {
            return images.size();
        } catch (Exception e) {
            return 0;
        }
    }

    class ViewHolder extends RecyclerView.ViewHolder {

        private final ImageView carImage;

        ViewHolder(View v) {
            super(v);
            carImage = v.findViewById(R.id.carImage);
        }
    }
}
