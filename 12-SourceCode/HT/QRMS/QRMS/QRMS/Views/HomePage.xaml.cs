﻿using System;
using System.Collections.Generic;
using QRMS.Constants;
using Xamarin.Forms;

namespace QRMS.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            row_trencung.Height = 20;

            if (Device.Idiom == TargetIdiom.Phone)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    if (MySettings.h_QRMS >= 812)
                    {
                        row_trencung.Height = 40; 
                    }
                    else
                    { 
                    }
                }
                else
                { 
                    row_trencung.Height = 10 + MySettings.Height_Notch;
                }
            }
            else
            {
                if (Device.RuntimePlatform == Device.iOS)
                { 
                }
                else
                {
                    row_trencung.Height = 10 + MySettings.Height_Notch; 
                }
            }

        }

        void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void BtnNhapKho_CLicked(System.Object sender, System.EventArgs e)
        {
        }

        void BtnDieuChuyenKho_CLicked(System.Object sender, System.EventArgs e)
        {
        }

        void BtnXuatKho_CLicked(System.Object sender, System.EventArgs e)
        {
        }

        void BtnKiemKe_CLicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}
