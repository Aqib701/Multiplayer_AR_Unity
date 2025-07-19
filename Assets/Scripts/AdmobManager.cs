using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdmobManager : MonoBehaviour
{
   private BannerView _bannerView;
   private InterstitialAd InterAd;
   [SerializeField] private string AppID = "";
   [SerializeField] private string BannerID = "";
   [SerializeField] private string InterstitialD = "";


 


   public void CallBannerAD()
   {
      RequestBannerAd();
      
   }


   public void CallInterstitialAd()
   {
      RequestInterstitialAd();
      InterAd.Show();
      
   }
   
   
   
   
   

   void RequestBannerAd()
   {
      _bannerView= new BannerView(BannerID,AdSize.Banner, AdPosition.Bottom);
      AdRequest request = new AdRequest.Builder().Build();
      _bannerView.LoadAd(request);


   }

   void RequestInterstitialAd()
   {
      
       InterAd=new InterstitialAd(InterstitialD);
      AdRequest request = new AdRequest.Builder().Build();
      InterAd.LoadAd(request);
      


   }
   
   
}
