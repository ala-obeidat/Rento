package com.epicloudtech.erent.helpers;

import android.graphics.Rect;
import android.support.v7.widget.RecyclerView;
import android.view.View;

/**
 * Created by Zaid Khaled on 21/07/2017.
 */
public class SpacesItemDecoration extends RecyclerView.ItemDecoration {
    private final int topSpace;
    private final int bottomSpace;
    private final int rightSpace;
    private final int leftSpace;

    public SpacesItemDecoration(int top, int bottom, int right, int left) {
        this.topSpace = top;
        this.bottomSpace = bottom;
        this.rightSpace = right;
        this.leftSpace = left;
    }

    @Override
    public void getItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state) {
        outRect.left = leftSpace;
        outRect.right = rightSpace;
        outRect.bottom = bottomSpace;
        outRect.top = topSpace;

        // Add top margin only for the first item to avoid double space between items
        if (parent.getChildAdapterPosition(view) == 0) {
            outRect.bottom = bottomSpace;
            outRect.left = leftSpace;
            outRect.right = rightSpace;
        }
    }
}
