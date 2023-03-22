using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using AudienceNetwork;
public class FaceBookAdManager : SingletonMono<FaceBookAdManager>
{
    // private AdView adView;
    //
    // private InterstitialAd interstitialAd;
    // private bool isLoaded;
    //
    //
    // // Start is called before the first frame update
    // void Start()
    // {
    //     //LoadBanner();
    //
    //     LoadInterstitial();
    // }
    //
    // public void LoadBanner()
    // {
    //     if (this.adView) {
    //         this.adView.Dispose();
    //     }
    //
    //     this.adView = new AdView("IMG_16_9_APP_INSTALL#YOUR_PLACEMENT_ID", AdSize.BANNER_HEIGHT_50);
    //     this.adView.Register(this.gameObject);
    //
    //     // Set delegates to get notified on changes or when the user interacts with the ad.
    //     this.adView.AdViewDidLoad = (delegate() {
    //         Debug.Log("Banner loaded.");
    //         this.adView.Show(AdPosition.BOTTOM);
    //     });
    //     adView.AdViewDidFailWithError = (delegate(string error) {
    //         Debug.Log("Banner failed to load with error: " + error);
    //     });
    //     adView.AdViewWillLogImpression = (delegate() {
    //         Debug.Log("Banner logged impression.");
    //     });
    //     adView.AdViewDidClick = (delegate() {
    //         Debug.Log("Banner clicked.");
    //     });
    //
    //     // Initiate a request to load an ad.
    //     adView.LoadAd();
    // }
    //
    // // Interstrail ad intigration
    // public void LoadInterstitial()
    // {
    //     this.interstitialAd = new InterstitialAd("IMG_16_9_LINK#YOUR_PLACEMENT_ID");
    //     this.interstitialAd.Register(this.gameObject);
    //
    //     // Set delegates to get notified on changes or when the user interacts with the ad.
    //     this.interstitialAd.InterstitialAdDidLoad = (delegate() {
    //         Debug.Log("Interstitial ad loaded.");
    //         this.isLoaded = true;
    //     });
    //     interstitialAd.InterstitialAdDidFailWithError = (delegate(string error) {
    //         Debug.Log("Interstitial ad failed to load with error: " + error);
    //     });
    //     interstitialAd.InterstitialAdWillLogImpression = (delegate() {
    //         Debug.Log("Interstitial ad logged impression.");
    //     });
    //     interstitialAd.InterstitialAdDidClick = (delegate() {
    //         Debug.Log("Interstitial ad clicked.");
    //     });
    //
    //     this.interstitialAd.interstitialAdDidClose = (delegate() {
    //         Debug.Log("Interstitial ad did close.");
    //         if (this.interstitialAd != null) {
    //             this.interstitialAd.Dispose();
    //         }
    //     });
    //
    //     // Initiate the request to load the ad.
    //     this.interstitialAd.LoadAd();
    // }
    // public void ShowInterstitial()
    // {
    //     if (this.isLoaded) {
    //         this.interstitialAd.Show();
    //         this.isLoaded = false;
    //
    //     } else {
    //         Debug.Log("Interstitial Ad not loaded!");
    //     }
    // }
}
