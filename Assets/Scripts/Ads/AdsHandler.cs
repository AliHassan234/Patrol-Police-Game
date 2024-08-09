//using System;
//using UnityEngine;
//using GoogleMobileAds.Api;
//using System.Collections;
//using UnityEngine.Advertisements;
//using UnityEngine.UI;
//using GoogleMobileAds.Common;
//using Unity.Advertisement.IosSupport.Components;

//public class AdsHandler : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
//{
//    private BannerView bannerView;
//    private BannerView bigBannerView;
//    private InterstitialAd interstitial;
//    private RewardedAd rewardedAd;
//    [HideInInspector] public AppOpenAd appOpenAd;

//    [SerializeField] string Unity_RewardedId;
//    [SerializeField] string Unity_Interstitial;

//    public float frequency = 3f, canShow;
//    [SerializeField] string appOpenId, rewardedAdId, bannerAdId, bigBannerId, interstitialAdId, unityGameId;

//    public ContextScreenView csv;
//    // UnityEngine.Events.UnityAction rewardAction;

//    public static AdsHandler instance;

//    public static int rewardtype = 0;

//    public bool isadstruecounter;
//    public float counternum;
//    public float adsdelaytime;
//    public Text countertext;
//    public GameObject counterPenal;
//    public static int appOpenNumber;
//    private void Awake()
//    {
//        if (instance != null && instance != this)
//        {
//            Destroy(gameObject);
//        }
//        else
//        {
//            instance = this;

//            DontDestroyOnLoad(this.gameObject);
//            DontDestroyOnLoad(counterPenal);
//        }
//    }

//    public IEnumerator Start()
//    {
//        csv.RequestAuthorizationTracking();
//        yield return new WaitUntil(() => RemoteConfigHandler.isFirebaseInitialized && (RemoteConfigHandler.remoteReady || RemoteConfigHandler.remoteNotReady));

//        yield return new WaitUntil(() => Application.internetReachability != NetworkReachability.NotReachable);

//        canShow = frequency;

//        InitializeOnceAgain();

//    }

//    #region RemoteConfigAds
//    public void ShowPlayBtnAd()
//    {
//        Debug.Log("ShowPlayBtnAd");
//        if (!RemoteConfigHandler.Instance.remoteConfigs.configs[3].value)
//        {
//            return;
//        }
//        callInterstitialwithCounter();
//    }

//    public void ShowSellAd()
//    {
//        Debug.Log("ShowSellAd");
//        if (!RemoteConfigHandler.Instance.remoteConfigs.configs[5].value)
//        {
//            return;
//        }
//        callInterstitialwithCounter();
//    }

//    public void ShowSettingAd()
//    {
//        Debug.Log("ShowSettingAd");
//        if (!RemoteConfigHandler.Instance.remoteConfigs.configs[4].value)
//        {
//            return;
//        }
//        callInterstitialwithCounter();
//    }

//    public void ShowBuyAd()
//    {
//        Debug.Log("ShowBuyAd");
//        if (!RemoteConfigHandler.Instance.remoteConfigs.configs[6].value)
//        {
//            return;
//        }
//        callInterstitialwithCounter();
//    }

//    public void ShowTeleportAd()
//    {
//        Debug.Log("ShowTeleportAd");
//        if (!RemoteConfigHandler.Instance.remoteConfigs.configs[7].value)
//        {
//            return;
//        }
//        callInterstitialwithCounter();
//    }
//    #endregion
//    public static bool bannerAvailable, bigBannerAvailable;

//    public void InitializeOnceAgain()
//    {
//        MobileAds.Initialize((InitializationStatus initStatus) =>
//        {
//            if (RemoteConfigHandler.Instance.remoteConfigs.configs[0].value)
//                this.RequestBanner();
//            if (RemoteConfigHandler.Instance.remoteConfigs.configs[1].value)
//                this.RequestBigBanner();
//            this.RequestInterstitial();
//            this.RequestRewarded();

//            if (RemoteConfigHandler.Instance.remoteConfigs.configs[2].value)
//                this.RequestAppOpenAd();

//            Advertisement.Initialize(unityGameId, false, this);

//        });
//    }

//    #region Requesting Ads
//    private void RequestBanner()
//    {
//        if (PlayerPrefs.GetInt("RAD", 0) == 1)
//            return;

//        if (this.bannerView != null)
//        {
//            this.bannerView.Destroy();
//        }

//        this.bannerView = new BannerView(bannerAdId, AdSize.Banner, AdPosition.Top);

//        // Called when an ad request has successfully loaded.
//        this.bannerView.OnAdLoaded += this.HandleOnAdBannerLoaded;
//        // Called when an ad request failed to load.
//        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
//        // Called when an ad is clicked.
//        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
//        // Called when the user returned from the app after an ad click.
//        this.bannerView.OnAdClosed += this.HandleOnAdClosed;

//        // Create an empty ad request.
//        AdRequest request = new AdRequest.Builder().Build();

//        // Load the banner with the request.
//        this.bannerView.LoadAd(request);
//    }

//    private void RequestBigBanner()
//    {
//        if (PlayerPrefs.GetInt("RAD", 0) == 1)
//            return;

//        if (this.bigBannerView != null)
//        {
//            this.bigBannerView.Destroy();
//        }

//        this.bigBannerView = new BannerView(bigBannerId, AdSize.MediumRectangle, AdPosition.BottomRight);

//        // Called when an ad request has successfully loaded.
//        this.bigBannerView.OnAdLoaded += this.HandleOnAdBigBannerLoaded;
//        // Called when an ad request failed to load.
//        this.bigBannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
//        // Called when an ad is clicked.
//        this.bigBannerView.OnAdOpening += this.HandleOnAdOpened;
//        // Called when the user returned from the app after an ad click.
//        this.bigBannerView.OnAdClosed += this.HandleOnAdClosed;

//        // Create an empty ad request.
//        AdRequest request = new AdRequest.Builder().Build();

//        // Load the banner with the request.
//        this.bigBannerView.LoadAd(request);
//    }

//    private void RequestInterstitial()
//    {
//        //if (PlayerPrefs.GetInt("RAD", 0) == 1)
//        //    return;
//        // Initialize an InterstitialAd.
//        this.interstitial = new InterstitialAd(interstitialAdId);

//        // Called when an ad request has successfully loaded.
//        //this.interstitial.OnAdLoaded += HandleOnAdBannerLoaded;
//        // Called when an ad request failed to load.
//        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
//        // Called when an ad is shown.
//        this.interstitial.OnAdOpening += HandleOnAdOpening;
//        // Called when the ad is closed.
//        this.interstitial.OnAdClosed += HandleOnAdClosed;

//        // Create an empty ad request.
//        AdRequest request = new AdRequest.Builder().Build();
//        // Load the interstitial with the request.
//        this.interstitial.LoadAd(request);
//    }


//    private void RequestRewarded()
//    {

//        this.rewardedAd = new RewardedAd(rewardedAdId);

//        // Called when an ad request has successfully loaded.
//        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
//        // Called when an ad request failed to load.
//        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
//        // Called when an ad is shown.
//        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
//        // Called when an ad request failed to show.
//        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
//        // Called when the user should be rewarded for interacting with the ad.
//        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
//        // Called when the ad is closed.
//        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
//        // Create an empty ad request.
//        AdRequest request = new AdRequest.Builder().Build();
//        // Load the rewarded ad with the request.
//        this.rewardedAd.LoadAd(request);
//    }

//    public void RequestAppOpenAd()
//    {
//        // create new app open ad instance
//        AppOpenAd.LoadAd(appOpenId, ScreenOrientation.LandscapeLeft, CreateAdRequest(), (appOpenAd, error) =>
//        {
//            if (error != null)
//            {
//                MobileAdsEventExecutor.ExecuteInUpdate(() =>
//                {
//                    appOpenNumber = 1;
//                    Debug.Log("AppOpenAd load failed, error: " + error);
//                });
//                return;
//            }
//            MobileAdsEventExecutor.ExecuteInUpdate(() =>
//            {
//                appOpenNumber = 2;
//                Debug.Log("AppOpenAd loaded. Please background the app and return.");
//            });
//            this.appOpenAd = appOpenAd;
//        });
//    }

//    private AdRequest CreateAdRequest()
//    {
//        return new AdRequest.Builder().Build();
//    }

//    public void LoadUnityRewardedAd()
//    {
//        unityRewardedReady = false;
//        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
//        Advertisement.Load(Unity_RewardedId, this);
//        //   Advertisement.Load(Unity_Interstitial, this);
//    }
//    public void loadunityintertialad()
//    {
//        Advertisement.Load(Unity_Interstitial, this);
//    }
//    #endregion

//    #region Reload Rewarded Ads
//    public void CreateAndLoadRewardedAd()
//    {
//        this.rewardedAd = new RewardedAd(rewardedAdId);

//        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
//        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
//        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

//        // Create an empty ad request.
//        AdRequest request = new AdRequest.Builder().Build();
//        // Load the rewarded ad with the request.
//        this.rewardedAd.LoadAd(request);
//    }
//    #endregion

//    #region Showing Ads
//    public void ShowInterstitialAds()
//    {
//        //     Debug.Log("Intertial Called");

//        if (PlayerPrefs.GetInt("RAD", 0) == 1)
//            return;

//        if (canShow >= 0)
//            return;


//        //if (this.interstitial != null && this.interstitial.IsLoaded())
//        //{
//        //    this.interstitial.Show();

//        //   // this.RequestInterstitial();
//        //}
//        //else
//        // if (Advertisement.isInitialized)
//        // {
//        //     Advertisement.Show(Unity_Interstitial,this);
//        // }
//        //


//        if (this.interstitial != null && this.interstitial.IsLoaded())
//        {
//            //Debug.Log("Admob Intertial");

//            PlayerPrefs.SetString(SharedPrefs.CanShowAppOpen, "false");
//            this.interstitial.Show();


//            // this.RequestInterstitial();
//        }
//        else
//        {
//            if (Advertisement.isInitialized)
//            {
//                // Debug.Log("unity1");
//                //Debug.Log("Unity Intertial");

//                PlayerPrefs.SetString(SharedPrefs.CanShowAppOpen, "false");
//                Advertisement.Show(Unity_Interstitial, this);
//            }



//            //   Debug.Log("unity2");
//        }



//        Debug.Log("cans show value  : " + canShow);


//        canShow = frequency;


//    }
//    public void callInterstitialwithCounter()
//    {
//        if (PlayerPrefs.GetInt("RAD", 0) == 1)
//            return;

//        if (this.interstitial != null && this.interstitial.IsLoaded())
//        {
//            isadstruecounter = true;
//            counterPenal.SetActive(true);
//            StartCoroutine(DelayCounterForAds(adsdelaytime));
//        }
//        else
//        {
//            if (Advertisement.isInitialized)
//            {
//                isadstruecounter = true;
//                counterPenal.SetActive(true);
//                StartCoroutine(DelayCounterForAds(adsdelaytime));

//            }
//        }
//    }
//    IEnumerator DelayCounterForAds(float time)
//    {



//        while (counternum > 0)
//        {
//            yield return new WaitForSeconds(time);
//            countertext.text = "Showing Ads in " + counternum.ToString();
//            counternum -= 1;
//        }

//        if (counternum >= 0)
//        {
//            if (isadstruecounter)
//            {
//                ShowInterstitialAds();
//                isadstruecounter = false;
//                counterPenal.SetActive(false);
//                counternum = 3;
//            }

//        }
//    }
//    private void Update()
//    {

//        if (canShow > 0)
//        {
//            canShow -= Time.unscaledDeltaTime;
//        }
//    }


//    public void ShowRewardedAds()
//    {
//        if (PlayerPrefs.GetInt("RAD", 0) == 1)
//            return;

//        //  Debug.Log("Reward Called");
//        if (unityRewardedReady && Advertisement.isInitialized)
//        {
//            //  rewardAction = action;
//            Debug.Log("Unity Rewarded");

//            PlayerPrefs.SetString(SharedPrefs.CanShowAppOpen, "false");
//            Advertisement.Show(Unity_RewardedId, this);
//            return;
//        }
//        else
//        {
//            //  rewardAction = action;
//            if (this.rewardedAd != null && this.rewardedAd.IsLoaded())
//            {
//                PlayerPrefs.SetString(SharedPrefs.CanShowAppOpen, "false");
//                this.rewardedAd.Show();
//                Debug.Log("Admob Rewarded");
//                return;
//            }
//        }
//    }

//    public bool isShowingAppOpenAd;

//    public void ShowAppOpenAd()
//    {
//        HideBannerAds();
//        Debug.Log("ShowAppOpenAd " + !RemoteConfigHandler.Instance.remoteConfigs.configs[2].value);
//        if (!RemoteConfigHandler.Instance.remoteConfigs.configs[2].value)
//            return;
//            Debug.Log("App Open Ads 2" + isShowingAppOpenAd);
//        if (isShowingAppOpenAd)
//        {
//            return;
//        }
//        Debug.Log("App Open Ads 3" + appOpenAd == null);
//        if (appOpenAd == null)
//        {
//            return;
//        }
//        // Register for ad events.
//        this.appOpenAd.OnAdDidDismissFullScreenContent += (sender, args) =>
//        {
//            isShowingAppOpenAd = false;
//            MobileAdsEventExecutor.ExecuteInUpdate(() =>
//            {
//                ShowBannerAds();
//                Debug.Log("AppOpenAd dismissed.");
//                if (this.appOpenAd != null)
//                {
//                    this.appOpenAd.Destroy();
//                    this.appOpenAd = null;
//                    this.RequestAppOpenAd();
//                }
//            });
//        };
//        this.appOpenAd.OnAdFailedToPresentFullScreenContent += (sender, args) =>
//        {
//            isShowingAppOpenAd = false;
//            var msg = args.AdError.GetMessage();
//            MobileAdsEventExecutor.ExecuteInUpdate(() =>
//            {
//                Debug.Log("AppOpenAd present failed, error: " + msg);
//                if (this.appOpenAd != null)
//                {
//                    this.appOpenAd.Destroy();
//                    this.appOpenAd = null;
//                }
//            });
//        };
//        this.appOpenAd.OnAdDidPresentFullScreenContent += (sender, args) =>
//        {
//            isShowingAppOpenAd = true;
//            MobileAdsEventExecutor.ExecuteInUpdate(() =>
//            {
//                Debug.Log("AppOpenAd presented.");
//            });
//        };
//        this.appOpenAd.OnAdDidRecordImpression += (sender, args) =>
//        {
//            MobileAdsEventExecutor.ExecuteInUpdate(() =>
//            {
//                Debug.Log("AppOpenAd recorded an impression.");
//            });
//        };
//        this.appOpenAd.OnPaidEvent += (sender, args) =>
//        {
//            string currencyCode = args.AdValue.CurrencyCode;
//            long adValue = args.AdValue.Value;
//            string suffix = "AppOpenAd received a paid event.";
//            MobileAdsEventExecutor.ExecuteInUpdate(() =>
//            {
//                string msg = string.Format("{0} (currency: {1}, value: {2}", suffix, currencyCode, adValue);
//                Debug.Log(msg);
//            });
//        };
//        appOpenAd.Show();
//    }

//    public void ShowBannerAds()
//    {
//        if (!RemoteConfigHandler.Instance.remoteConfigs.configs[0].value)
//            return;
//            if (PlayerPrefs.GetInt("RAD", 0) == 1)
//            return;
//        if (this.bannerView == null)
//            this.RequestBanner();
//        else
//            this.bannerView.Show();
//    }


//    public void ShowBigBannerAds()
//    {
//        if (!RemoteConfigHandler.Instance.remoteConfigs.configs[1].value)
//            return;
//            if (PlayerPrefs.GetInt("RAD", 0) == 1)
//            return;
//        if (this.bigBannerView == null)
//            this.RequestBigBanner();
//        else
//            this.bigBannerView.Show();
//    }

//    public void HideBannerAds()
//    {
//        if (this.bannerView != null)
//            this.bannerView.Hide();
//        //if (this.bannerView != null)
//        //{
//        //    this.bannerView.Destroy();
//        //}
//    }

//    public void HideBigBannerAds()
//    {
//        if (this.bigBannerView != null)
//            this.bigBannerView.Hide();
//        //if (this.bannerView != null)
//        //{
//        //    this.bannerView.Destroy();
//        //}
//    }
//    #endregion

//    #region Handlers
//    public void HandleOnAdBannerLoaded(object sender, EventArgs args)
//    {
//        this.bannerView.Hide();
//        bannerAvailable = true;
//        //bannerView.Show();
//    }

//    public void HandleOnAdBigBannerLoaded(object sender, EventArgs args)
//    {
//        this.bigBannerView.Hide();
//        bigBannerAvailable = true;
//        //bannerView.Show();
//    }

//    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) { }

//    public void HandleOnAdOpened(object sender, EventArgs args)
//    {

//    }

//    public void HandleOnAdClosed(object sender, EventArgs args)
//    {
//        this.RequestInterstitial();
//    }

//    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
//    {

//    }
//    public void HandleOnAdOpening(object sender, EventArgs args)
//    {

//    }
//    public void HandleRewardedAdLoaded(object sender, EventArgs args)
//    {

//    }

//    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {

//    }

//    public void HandleRewardedAdOpening(object sender, EventArgs args)
//    {

//    }

//    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
//    {

//    }

//    public void HandleRewardedAdClosed(object sender, EventArgs args)
//    {
//        CreateAndLoadRewardedAd();
//    }

//    public void HandleUserEarnedReward(object sender, Reward args)
//    {
//        string type = args.Type;
//        double amount = args.Amount;
//        Debug.Log(
//            "HandleRewardedAdRewarded event received for "
//                        + amount.ToString() + " " + type);

//        if (rewardtype == 1)
//        {
//            Debug.Log("Giving Reward type = " + rewardedAd);
//            rewardtype = 0;
//            PlayerPrefs.SetInt(SharedPrefs.Coins, PlayerPrefs.GetInt(SharedPrefs.Coins) + 500);
//        }
//        this.RequestRewarded();

//        //if (rewardAction != null)
//        //{
//        //    rewardAction.Invoke();

//        //}
//    }

//    public void OnInitializationComplete()
//    {
//        Debug.Log("Unity Ads initialization complete.");

//        LoadUnityRewardedAd();
//    }

//    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
//    {
//        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
//    }

//    void HandleShowResult(ShowResult result)
//    {
//        Debug.Log(result.ToString());
//        if (result == ShowResult.Finished)
//        {
//            if (rewardtype == 1)
//            {
//                Debug.Log("Giving Reward type = " + rewardedAd);
//                rewardtype = 0;
//                PlayerPrefs.SetInt(SharedPrefs.Coins, PlayerPrefs.GetInt(SharedPrefs.Coins) + 500);
//            }
//        }
//        else if (result == ShowResult.Skipped)
//        {
//            Debug.Log("The player skipped the video - DO NOT REWARD!");
//        }
//        else if (result == ShowResult.Failed)
//        {
//            Debug.Log("Video failed to show");
//        }
//    }

//    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message) { }
//    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message) { }
//    public void OnUnityAdsShowStart(string adUnitId) { }
//    public void OnUnityAdsShowClick(string adUnitId) { }
//    #endregion

//    #region HELPER METHODS

//    public bool unityRewardedReady = false;
//    public void OnUnityAdsAdLoaded(string placementId)
//    {
//        if (placementId == Unity_RewardedId)
//        {
//            unityRewardedReady = true;
//        }
//    }

//    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
//    {
//        if (placementId == Unity_RewardedId)
//        {
//            HandleShowResult(ShowResult.Finished);
//            LoadUnityRewardedAd();
//        }
//        else
//        {
//            loadunityintertialad();
//        }
//    }


//    public void OnApplicationPause(bool paused)
//    {
//        // Display the app open ad when the app is foregrounded.
//        Debug.Log("App Open Ads 0" + paused);
//        if (!paused)
//        {
//            Debug.Log("App Open Ads 1 = " + PlayerPrefs.GetString(SharedPrefs.CanShowAppOpen));

//            if (PlayerPrefs.GetString(SharedPrefs.CanShowAppOpen) == "true")
//            {
//                ShowAppOpenAd();
//            }
//            else
//            {
//                PlayerPrefs.SetString(SharedPrefs.CanShowAppOpen, "true");
//            }
//        }
//    }
//    #endregion
//}
