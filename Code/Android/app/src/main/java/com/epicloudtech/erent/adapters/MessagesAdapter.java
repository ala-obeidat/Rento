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
import android.widget.TextView;

import com.epicloudtech.erent.R;
import com.epicloudtech.erent.models.responses.MessageItemResponse;
import com.epicloudtech.erent.utils.Utils;

import java.util.ArrayList;

public class MessagesAdapter extends RecyclerView.Adapter<MessagesAdapter.ViewHolder> {

    private Context context;
    private int lastPosition = -1;
    ArrayList<MessageItemResponse> messages;

    public MessagesAdapter(Context context, ArrayList<MessageItemResponse> messages) {
        super();
        this.context = context;
        this.messages = messages;
    }

    @Override
    public MessagesAdapter.ViewHolder onCreateViewHolder(ViewGroup viewGroup, int i) {
        Utils.refreshLocal(context);
        View v = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.message_card, viewGroup, false);

        return new MessagesAdapter.ViewHolder(v);
    }

    @TargetApi(Build.VERSION_CODES.JELLY_BEAN)
    @Override
    public void onBindViewHolder(MessagesAdapter.ViewHolder viewHolder, final int position) {

        Animation animation = AnimationUtils.loadAnimation(context,
                (position > lastPosition) ? R.anim.up_from_bottom
                        : R.anim.down_from_top);
        viewHolder.itemView.startAnimation(animation);
        lastPosition = position;
        MessageItemResponse message = messages.get(position);

        String date = message.getCreateDate().substring(0,10);
        viewHolder.tvDate.setText(date);
        viewHolder.tvTitle.setText("");
        viewHolder.tvMessage.setText(message.getBody());

    }

    @Override
    public void onViewDetachedFromWindow(MessagesAdapter.ViewHolder holder) {
        super.onViewDetachedFromWindow(holder);
        holder.itemView.clearAnimation();
    }

    @Override
    public int getItemCount() {
        return messages.size();
    }

    public class ViewHolder extends RecyclerView.ViewHolder {

        private final TextView tvTitle, tvMessage, tvDate;

        ViewHolder(View v) {
            super(v);
            tvTitle = v.findViewById(R.id.tvTitle);
            tvDate = v.findViewById(R.id.tvDate);
            tvMessage = v.findViewById(R.id.tvMessage);

        }
    }
}
