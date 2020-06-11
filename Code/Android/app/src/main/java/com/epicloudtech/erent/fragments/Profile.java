package com.epicloudtech.erent.fragments;


import android.app.FragmentManager;
import android.content.Intent;
import android.graphics.Typeface;
import android.os.Bundle;
import android.app.Fragment;
import android.support.annotation.Nullable;
import android.support.v13.app.FragmentStatePagerAdapter;
import android.support.v4.view.ViewPager;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.epicloudtech.erent.R;
import com.epicloudtech.erent.activities.Settings;
import com.epicloudtech.erent.utils.Constants;
import com.epicloudtech.erent.utils.Utils;
import com.gigamole.navigationtabstrip.NavigationTabStrip;

import butterknife.BindView;
import butterknife.ButterKnife;

/**
 * A simple {@link Fragment} subclass.
 */
public class Profile extends Fragment {

    @BindView(R.id.ivShare)
    ImageView ivShare;
    @BindView(R.id.userName)
    TextView userName;
    @BindView(R.id.nts_center)
    NavigationTabStrip nts_center;
    @BindView(R.id.viewPager)
    ViewPager viewPager;
    @BindView(R.id.ivSettings)
    ImageView ivSettings;

    public Profile() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        Utils.refreshLocal(getActivity());
        return inflater.inflate(R.layout.fragment_profile, container, false);
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        ButterKnife.bind(this, view);

        setupPager();
        nts_center.setViewPager(viewPager, 0);

        ivSettings.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Utils.goToActivityWithAnimation(getActivity(), Settings.class, false);
            }
        });

        userName.setText(Utils.getValue(getActivity(), Constants.USER_NAME, ""));


        ivShare.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent sendIntent = new Intent();
                sendIntent.setAction(Intent.ACTION_SEND);
                sendIntent.putExtra(Intent.EXTRA_TEXT,
                        getString(R.string.share_text) + "\n\n https://play.google.com/store/apps/details?id=com.chionophile.erent");
                sendIntent.setType("text/plain");
                startActivity(sendIntent);
            }
        });
    }


    private void setupPager() {
        MyPagerAdapter adapterViewPager = new MyPagerAdapter(getActivity().getFragmentManager());
        viewPager.setOffscreenPageLimit(2);
        viewPager.setAdapter(adapterViewPager);
    }


    private class MyPagerAdapter extends FragmentStatePagerAdapter {

        MyPagerAdapter(FragmentManager fragmentManager) {
            super(fragmentManager);
        }

        @Override
        public int getCount() {
            return 3;
        }

        @Override
        public void notifyDataSetChanged() {
            super.notifyDataSetChanged();
        }


        @Override
        public Fragment getItem(int position) {
            switch (position) {
                case 0:
                    return new Contact();
                case 1:
                    return new Favourites();
                case 2:
                    return new History();
                default:
                    return new Contact();
            }

        }

        // Returns the page title for the top indicator
        @Override
        public CharSequence getPageTitle(int position) {
            return "";
        }

    }


}
