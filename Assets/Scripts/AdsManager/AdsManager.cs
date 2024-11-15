using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System.Collections.Generic;

public class AdsManager : MonoBehaviour {

    //#region AdMob
    //[Header("Admob")]
    //public string adMobAppID = "";
    //// this is test ID
    //public string interstitalAdMobId = "";
    //// this is test ID
    //public string videoAdMobId = "";
    //// this is test ID
    //public string bannerAdMobId = "";
    //private BannerView bannerView;
    //InterstitialAd interstitialAdMob;
    //private RewardBasedVideoAd rewardBasedAdMobVideo; 
    //AdRequest requestAdMobInterstitial, AdMobVideoRequest;
    //#endregion
    //[Space(15)]
    //#region
    //[Header("UnityAds")]
    //public string unityAdsGameId;
    //public string unityAdsVideoPlacementId = "rewardedVideo";
    //#endregion

    int npaValue = -1;
    //"npa"=Non Personalized Ads

    static AdsManager instance;
    public static AdsManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType(typeof(AdsManager)) as AdsManager;

            return instance;
        }
    }

    void Awake()
    {
        npaValue = PlayerPrefs.GetInt("npa", 0);

        gameObject.name = this.GetType().Name;
        DontDestroyOnLoad(gameObject);
       // InitializeAds();
        //if (Advertisement.isSupported) {
        //	print("Init UnityAd");
        //	#if UNITY_ANDROID
        //		Advertisement.Initialize ("1635511");
        //	#elif UNITY_IOS
        //		Advertisement.Initialize ("1635510");
        //	#else
        //		Advertisement.Initialize ("1635511");
        //	#endif
        //}
        //else
        //	print("Not support UnityAd");		
    }

    //public void ShowInterstitial()
    //{
    //	ShowAdMob();
    //}

    public void IsVideoRewardAvailable()
    {
        if (isVideoAvaiable())
        {
            ShowRewardedAd();
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "MainScene")
                LevelSelectManager.levelSelectManager.menuManager.ShowPopUpMenu(Camera.main.GetComponent<ShopManager>().videoNotAvailablePopup);
            else
                GameplayManager.gameplayManager.menuManager.ShowPopUpMenu(Camera.main.GetComponent<ShopManager>().videoNotAvailablePopup);
        }
    }

    //public void ShowVideoReward()
    //{
    //	/*if(Advertisement.IsReady(unityAdsVideoPlacementId))
    //	{
    //		UnityAdsShowVideo();
    //	}
    //	else */if(rewardBasedAdMobVideo.IsLoaded())
    //	{
    //		AdMobShowVideo();
    //	}
    //}

    //private void RequestInterstitial()
    //{
    //	// Initialize an InterstitialAd.
    //	interstitialAdMob = new InterstitialAd(interstitalAdMobId);

    //	// Called when an ad request has successfully loaded.
    //	interstitialAdMob.OnAdLoaded += HandleOnAdLoaded;
    //	// Called when an ad request failed to load.
    //	interstitialAdMob.OnAdFailedToLoad += HandleOnAdFailedToLoad;
    //	// Called when an ad is shown.
    //	interstitialAdMob.OnAdOpening += HandleOnAdOpened;
    //	// Called when the ad is closed.
    //	interstitialAdMob.OnAdClosed += HandleOnAdClosed;
    //	// Called when the ad click caused the user to leave the application.
    //	interstitialAdMob.OnAdLeavingApplication += HandleOnAdLeavingApplication;

    //	// Create an empty ad request.
    //	requestAdMobInterstitial = new AdRequest.Builder()
    //		.AddTestDevice(AdRequest.TestDeviceSimulator)
    //			.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
    //			.AddKeyword("game")
    //               .AddExtra("npa",npaValue.ToString())
    //			.TagForChildDirectedTreatment(false)
    //			.AddExtra("color_bg", "9B30FF")
    //		.Build();
    //	// Load the interstitial with the request.
    //	interstitialAdMob.LoadAd(requestAdMobInterstitial);
    //}

    //public void ShowAdMob()
    //{
    //	if(interstitialAdMob.IsLoaded())
    //	{
    //		interstitialAdMob.Show();
    //	}
    //	else
    //	{
    //		interstitialAdMob.LoadAd(requestAdMobInterstitial);
    //	}
    //}

    //public void HandleOnAdLoaded(object sender, EventArgs args)
    //{
    //	MonoBehaviour.print("HandleAdLoaded event received");
    //}

    //public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    //{
    //	MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
    //}

    //public void HandleOnAdOpened(object sender, EventArgs args)
    //{
    //	MonoBehaviour.print("HandleAdOpened event received");
    //}

    //public void HandleOnAdClosed(object sender, EventArgs args)
    //{
    //	MonoBehaviour.print("HandleAdClosed event received");
    //	interstitialAdMob.LoadAd(requestAdMobInterstitial);
    //}

    //public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    //{
    //	MonoBehaviour.print("HandleAdLeftApplication event received");
    //}

    //private void RequestRewardedVideo()
    //{
    //	// Called when an ad request has successfully loaded.
    //	rewardBasedAdMobVideo.OnAdLoaded += HandleRewardBasedVideoLoadedAdMob;
    //	// Called when an ad request failed to load.
    //	rewardBasedAdMobVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoadAdMob;
    //	// Called when an ad is shown.
    //	rewardBasedAdMobVideo.OnAdOpening += HandleRewardBasedVideoOpenedAdMob;
    //	// Called when the ad starts to play.
    //	rewardBasedAdMobVideo.OnAdStarted += HandleRewardBasedVideoStartedAdMob;
    //	// Called when the user should be rewarded for watching a video.
    //	rewardBasedAdMobVideo.OnAdRewarded += HandleRewardBasedVideoRewardedAdMob;
    //	// Called when the ad is closed.
    //	rewardBasedAdMobVideo.OnAdClosed += HandleRewardBasedVideoClosedAdMob;
    //	// Called when the ad click caused the user to leave the application.
    //	rewardBasedAdMobVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplicationAdMob;
    //	// Create an empty ad request.
    //	AdMobVideoRequest = new AdRequest.Builder()
    //		.AddTestDevice(AdRequest.TestDeviceSimulator)
    //			.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
    //			.AddKeyword("game")
    //			.AddExtra("npa", npaValue.ToString())
    //			.TagForChildDirectedTreatment(false)
    //			.AddExtra("color_bg", "9B30FF")
    //		.Build();
    //	// Load the rewarded video ad with the request.
    //	this.rewardBasedAdMobVideo.LoadAd(AdMobVideoRequest, videoAdMobId);
    //}

    //public void HandleRewardBasedVideoLoadedAdMob(object sender, EventArgs args)
    //{
    //	MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");

    //}

    //public void HandleRewardBasedVideoFailedToLoadAdMob(object sender, AdFailedToLoadEventArgs args)
    //{
    //	MonoBehaviour.print("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);

    //}

    //public void HandleRewardBasedVideoOpenedAdMob(object sender, EventArgs args)
    //{
    //	MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    //}

    //public void HandleRewardBasedVideoStartedAdMob(object sender, EventArgs args)
    //{
    //	MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    //}

    //public void HandleRewardBasedVideoClosedAdMob(object sender, EventArgs args)
    //{
    //	MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    //	this.rewardBasedAdMobVideo.LoadAd(AdMobVideoRequest, videoAdMobId);
    //}

    //public void HandleRewardBasedVideoRewardedAdMob(object sender, Reward args)
    //{
    //    Camera.main.GetComponent<ShopManager>().AddCoinsAfterVideoWatched();
    //    string type = args.Type;
    //    double amount = args.Amount;
    //    MonoBehaviour.print("HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);

    //}

    //public void HandleRewardBasedVideoLeftApplicationAdMob(object sender, EventArgs args)
    //{
    //	MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    //}

    //void InitializeAds()
    //{
    //	MobileAds.Initialize(adMobAppID);
    //	this.RequestBanner();
    //	this.rewardBasedAdMobVideo = RewardBasedVideoAd.Instance;
    //	this.RequestRewardedVideo();
    //	//Advertisement.Initialize(unityAdsGameId);
    //	RequestInterstitial();
    //}


    //void AdMobShowVideo()
    //{
    //	rewardBasedAdMobVideo.Show();	
    //}



    bool isVideoAvaiable()
    {
        //#if !UNITY_EDITOR
        //if(Advertisement.IsReady(unityAdsVideoPlacementId))
        //{
        //	return true;
        //}
        if (rewardedAd.IsLoaded())
        {
            return true;
        }
        //#endif
        return false;
    }

    //private void RequestBanner()
    //{
    //	// Create a 320x50 banner at the top of the screen.
    //	bannerView = new BannerView(bannerAdMobId, AdSize.Banner, AdPosition.Bottom);

    //	// Called when an ad request has successfully loaded.
    //	bannerView.OnAdLoaded += HandleBannerOnAdLoaded;
    //	// Called when an ad request failed to load.
    //	bannerView.OnAdFailedToLoad += HandleBannerOnAdFailedToLoad;
    //	// Called when an ad is clicked.
    //	bannerView.OnAdOpening += HandleBannerOnAdOpened;
    //	// Called when the user returned from the app after an ad click.
    //	bannerView.OnAdClosed += HandleBannerOnAdClosed;
    //	// Called when the ad click caused the user to leave the application.
    //	bannerView.OnAdLeavingApplication += HandleBannerOnAdLeavingApplication;

    //	// Create an empty ad request.
    //	AdRequest request = new AdRequest.Builder()
    //		.AddTestDevice(AdRequest.TestDeviceSimulator)
    //			.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
    //			.AddKeyword("game")
    //			.AddExtra("npa", npaValue.ToString())
    //			.TagForChildDirectedTreatment(false)
    //			.AddExtra("color_bg", "9B30FF")
    //		.Build();

    //	// Load the banner with the request.
    //	bannerView.LoadAd(request);
    //}

    //public void HandleBannerOnAdLoaded(object sender, EventArgs args)
    //{
    //	MonoBehaviour.print("HandleAdLoaded event received");
    //	bannerView.Show();
    //}

    //public void HandleBannerOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    //{
    //	MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
    //		+ args.Message);
    //}

    //public void HandleBannerOnAdOpened(object sender, EventArgs args)
    //{
    //	MonoBehaviour.print("HandleAdOpened event received");
    //}

    //public void HandleBannerOnAdClosed(object sender, EventArgs args)
    //{
    //	MonoBehaviour.print("HandleAdClosed event received");
    //}

    //public void HandleBannerOnAdLeavingApplication(object sender, EventArgs args)
    //{
    //	MonoBehaviour.print("HandleAdLeftApplication event received");
    //}




    private readonly TimeSpan APPOPEN_TIMEOUT = TimeSpan.FromHours(4);
    private DateTime appOpenExpireTime;
    private AppOpenAd appOpenAd;
    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;
    private RewardedInterstitialAd rewardedInterstitialAd;
    private float deltaTime;
    private bool isShowingAppOpenAd;

    [SerializeField]
    private string Admob_Banner_ANDROID_ID = "Your adMob Banner ID";
    [SerializeField]
    private string Admob_Interstitial_ANDROID_ID = "Your adMob Interstitial ID";
    [SerializeField]
    private string Admob_Reward_ANDROID_ID = "Your adMob Reward ID";

    [SerializeField]
    private string Admob_Banner_IOS_ID = "Your adMob Banner ID";
    [SerializeField]
    private string Admob_Interstitial_IOS_ID = "Your adMob Interstitial ID";
    [SerializeField]
    private string Admob_Reward_IOS_ID = "Your adMob Reward ID";

    public UnityEvent OnAdLoadedEvent;
    public UnityEvent OnAdFailedToLoadEvent;
    public UnityEvent OnAdOpeningEvent;
    public UnityEvent OnAdFailedToShowEvent;
    public UnityEvent OnUserEarnedRewardEvent;
    public UnityEvent OnAdClosedEvent;
    //public bool showFpsMeter = true;
    //public Text fpsMeter;
    //public Text statusText;



    #region UNITY MONOBEHAVIOR METHODS



    // public static GoogleMobileAdsScript instance;

    public void Start()
    {
        MobileAds.SetiOSAppPauseOnBackground(true);

        List<String> deviceIds = new List<String>() { AdRequest.TestDeviceSimulator };

        // Add some test device IDs (replace with your own device IDs).
#if UNITY_IPHONE
        deviceIds.Add("96e23e80653bb28980d3f40beb58915c");
#elif UNITY_ANDROID
        deviceIds.Add("75EF8D155528C04DACBBA6F36F433035");
#endif

        // Configure TagForChildDirectedTreatment and test device IDs.
        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.Unspecified)
            .SetTestDeviceIds(deviceIds).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(HandleInitCompleteAction);


        //        instance = this;
        // Listen to application foreground / background events.
        //AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
    }

    private void HandleInitCompleteAction(InitializationStatus initstatus)
    {
        Debug.Log("Initialization complete.");

        // Callbacks from GoogleMobileAds are not guaranteed to be called on
        // the main thread.
        // In this example we use MobileAdsEventExecutor to schedule these calls on
        // the next Update() loop.

        RequestAndLoadInterstitialAd();
        RequestAndLoadRewardedAd();
        //RequestBannerAd();
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            // statusText.text = "Initialization complete.";
            RequestBannerAd();
        });
    }

    //private void Update()
    //{
    //    //if (showFpsMeter)
    //    //{
    //    //    fpsMeter.gameObject.SetActive(true);
    //    //    deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    //    //    float fps = 1.0f / deltaTime;
    //    //    fpsMeter.text = string.Format("{0:0.} fps", fps);
    //    //}
    //    //else
    //    //{
    //    //    fpsMeter.gameObject.SetActive(false);
    //    //}
    //}

    #endregion

    #region HELPER METHODS

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();
    }

    #endregion

    #region BANNER ADS

    public void RequestBannerAd()
    {
        PrintStatus("Requesting Banner ad.");

        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = Admob_Banner_ANDROID_ID;
#elif UNITY_IPHONE
        string adUnitId = Admob_Banner_IOS_ID;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up banner before reusing
        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        // Create a 320x50 banner at top of the screen
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Add Event Handlers
        bannerView.OnAdLoaded += (sender, args) =>
        {
            PrintStatus("Banner ad loaded.");
            OnAdLoadedEvent.Invoke();
        };
        bannerView.OnAdFailedToLoad += (sender, args) =>
        {
            PrintStatus("Banner ad failed to load with error: " + args.LoadAdError.GetMessage());
            OnAdFailedToLoadEvent.Invoke();
        };
        bannerView.OnAdOpening += (sender, args) =>
        {
            PrintStatus("Banner ad opening.");
            OnAdOpeningEvent.Invoke();
        };
        bannerView.OnAdClosed += (sender, args) =>
        {
            PrintStatus("Banner ad closed.");
            OnAdClosedEvent.Invoke();
        };
        bannerView.OnPaidEvent += (sender, args) =>
        {
            string msg = string.Format("{0} (currency: {1}, value: {2}",
                                        "Banner ad received a paid event.",
                                        args.AdValue.CurrencyCode,
                                        args.AdValue.Value);
            PrintStatus(msg);
        };

        // Load a banner ad
        bannerView.LoadAd(CreateAdRequest());
    }

    public void DestroyBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }


    public void ShowBanner()
    {
        if (bannerView != null)
        {
            bannerView.Show();
        }
    }

    #endregion

    #region INTERSTITIAL ADS

    public void RequestAndLoadInterstitialAd()
    {
        PrintStatus("Requesting Interstitial ad.");

#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = Admob_Interstitial_ANDROID_ID;
#elif UNITY_IPHONE
        string adUnitId = Admob_Interstitial_IOS_ID;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up interstitial before using it
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }

        interstitialAd = new InterstitialAd(adUnitId);

        // Add Event Handlers
        interstitialAd.OnAdLoaded += (sender, args) =>
        {
            PrintStatus("Interstitial ad loaded.");
            OnAdLoadedEvent.Invoke();

        };
        interstitialAd.OnAdFailedToLoad += (sender, args) =>
        {
            PrintStatus("Interstitial ad failed to load with error: " + args.LoadAdError.GetMessage());
            OnAdFailedToLoadEvent.Invoke();
        };
        interstitialAd.OnAdOpening += (sender, args) =>
        {
            PrintStatus("Interstitial ad opening.");
            OnAdOpeningEvent.Invoke();
        };
        interstitialAd.OnAdClosed += (sender, args) =>
        {
            PrintStatus("Interstitial ad closed.");
            OnAdClosedEvent.Invoke();
            RequestAndLoadInterstitialAd();
        };
        interstitialAd.OnAdDidRecordImpression += (sender, args) =>
        {
            PrintStatus("Interstitial ad recorded an impression.");
        };
        interstitialAd.OnAdFailedToShow += (sender, args) =>
        {
            PrintStatus("Interstitial ad failed to show.");
        };
        interstitialAd.OnPaidEvent += (sender, args) =>
        {
            string msg = string.Format("{0} (currency: {1}, value: {2}",
                                        "Interstitial ad received a paid event.",
                                        args.AdValue.CurrencyCode,
                                        args.AdValue.Value);
            PrintStatus(msg);
        };

        // Load an interstitial ad
        interstitialAd.LoadAd(CreateAdRequest());
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
            // RequestAndLoadInterstitialAd();
        }
        else
        {
            PrintStatus("Interstitial ad is not ready yet.");
        }
    }

    public void DestroyInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }
    }

    #endregion

    #region REWARDED ADS

    public void RequestAndLoadRewardedAd()
    {
        PrintStatus("Requesting Rewarded ad.");
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = Admob_Reward_ANDROID_ID;
#elif UNITY_IPHONE
        string adUnitId = Admob_Reward_IOS_ID;
#else
        string adUnitId = "unexpected_platform";
#endif

        // create new rewarded ad instance
        rewardedAd = new RewardedAd(adUnitId);

        // Add Event Handlers
        rewardedAd.OnAdLoaded += (sender, args) =>
        {
            PrintStatus("Reward ad loaded.");
            OnAdLoadedEvent.Invoke();
        };
        rewardedAd.OnAdFailedToLoad += (sender, args) =>
        {
            PrintStatus("Reward ad failed to load.");
            OnAdFailedToLoadEvent.Invoke();
        };
        rewardedAd.OnAdOpening += (sender, args) =>
        {
            PrintStatus("Reward ad opening.");
            OnAdOpeningEvent.Invoke();
        };
        rewardedAd.OnAdFailedToShow += (sender, args) =>
        {
            PrintStatus("Reward ad failed to show with error: " + args.AdError.GetMessage());
            OnAdFailedToShowEvent.Invoke();
        };
        rewardedAd.OnAdClosed += (sender, args) =>
        {
            PrintStatus("Reward ad closed.");
            OnAdClosedEvent.Invoke();
            RequestAndLoadRewardedAd();
        };
        rewardedAd.OnUserEarnedReward += (sender, args) =>
        {
            Camera.main.GetComponent<ShopManager>().AddCoinsAfterVideoWatched();
            PrintStatus("User earned Reward ad reward: " + args.Amount);
            OnUserEarnedRewardEvent.Invoke();
            isUpdate = true;
            RequestAndLoadRewardedAd();
        };
        rewardedAd.OnAdDidRecordImpression += (sender, args) =>
        {
            PrintStatus("Reward ad recorded an impression.");
        };
        rewardedAd.OnPaidEvent += (sender, args) =>
        {
            string msg = string.Format("{0} (currency: {1}, value: {2}",
                                        "Rewarded ad received a paid event.",
                                        args.AdValue.CurrencyCode,
                                        args.AdValue.Value);
            PrintStatus(msg);
        };

        // Create empty ad request
        rewardedAd.LoadAd(CreateAdRequest());
    }

    private bool isUpdate = false;
    private UnityAction rewardVideoAction;
    //public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    //{

    //    Debug.Log("Reload Reward Video");
    //    this.RequestRewardBasedVideo();

    //}


    private void Update()
    {
        if (!isUpdate) return;
        if (rewardVideoAction != null)
        {
            rewardVideoAction();

        }
        rewardVideoAction = null;
        isUpdate = false;
    }

    //public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    //{
    //    Debug.Log("HandleRewardBasedVideoRewarded");
    //    isUpdate = true;
    //    this.RequestRewardBasedVideo();

    //}


    public bool CheckRewardBasedVideo()
    {


        return rewardedAd.IsLoaded();
    }

    public void ShowRewardVideo(UnityAction action = null)
    {
        if (CheckRewardBasedVideo())
        {
            rewardVideoAction = action;
            this.rewardedAd.Show();
        }




    }


    public void ShowRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Show();

        }
        else
        {
            PrintStatus("Rewarded ad is not ready yet.");
        }
    }

    public void RequestAndLoadRewardedInterstitialAd()
    {
        PrintStatus("Requesting Rewarded Interstitial ad.");

        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/5354046379";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/6978759866";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create an interstitial.
        RewardedInterstitialAd.LoadAd(adUnitId, CreateAdRequest(), (rewardedInterstitialAd, error) =>
        {
            if (error != null)
            {
                PrintStatus("Rewarded Interstitial ad load failed with error: " + error);
                return;
            }

            this.rewardedInterstitialAd = rewardedInterstitialAd;
            PrintStatus("Rewarded Interstitial ad loaded.");

            // Register for ad events.
            this.rewardedInterstitialAd.OnAdDidPresentFullScreenContent += (sender, args) =>
            {
                PrintStatus("Rewarded Interstitial ad presented.");
            };
            this.rewardedInterstitialAd.OnAdDidDismissFullScreenContent += (sender, args) =>
            {
                PrintStatus("Rewarded Interstitial ad dismissed.");
                this.rewardedInterstitialAd = null;
            };
            this.rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent += (sender, args) =>
            {
                PrintStatus("Rewarded Interstitial ad failed to present with error: " +
                                                                        args.AdError.GetMessage());
                this.rewardedInterstitialAd = null;
            };
            this.rewardedInterstitialAd.OnPaidEvent += (sender, args) =>
            {
                string msg = string.Format("{0} (currency: {1}, value: {2}",
                                            "Rewarded Interstitial ad received a paid event.",
                                            args.AdValue.CurrencyCode,
                                            args.AdValue.Value);
                PrintStatus(msg);
            };
            this.rewardedInterstitialAd.OnAdDidRecordImpression += (sender, args) =>
            {
                PrintStatus("Rewarded Interstitial ad recorded an impression.");
            };
        });
    }

    public void ShowRewardedInterstitialAd()
    {
        if (rewardedInterstitialAd != null)
        {
            rewardedInterstitialAd.Show((reward) =>
            {
                PrintStatus("Rewarded Interstitial ad Rewarded : " + reward.Amount);
            });
        }
        else
        {
            PrintStatus("Rewarded Interstitial ad is not ready yet.");
        }
    }

    #endregion

    #region APPOPEN ADS

    public bool IsAppOpenAdAvailable
    {
        get
        {
            return (!isShowingAppOpenAd
                    && appOpenAd != null
                    && DateTime.Now < appOpenExpireTime);
        }
    }

    //public void OnAppStateChanged(AppState state)
    //{
    //    // Display the app open ad when the app is foregrounded.
    //    UnityEngine.Debug.Log("App State is " + state);

    //    // OnAppStateChanged is not guaranteed to execute on the Unity UI thread.
    //    MobileAdsEventExecutor.ExecuteInUpdate(() =>
    //    {
    //        if (state == AppState.Foreground)
    //        {
    //            ShowAppOpenAd();
    //        }
    //    });
    //}

    public void RequestAndLoadAppOpenAd()
    {
        PrintStatus("Requesting App Open ad.");
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/3419835294";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/5662855259";
#else
        string adUnitId = "unexpected_platform";
#endif
        // create new app open ad instance
        AppOpenAd.LoadAd(adUnitId,
                         ScreenOrientation.Portrait,
                         CreateAdRequest(),
                         OnAppOpenAdLoad);
    }

    private void OnAppOpenAdLoad(AppOpenAd ad, AdFailedToLoadEventArgs error)
    {
        if (error != null)
        {
            PrintStatus("App Open ad failed to load with error: " + error);
            return;
        }

        PrintStatus("App Open ad loaded. Please background the app and return.");
        this.appOpenAd = ad;
        this.appOpenExpireTime = DateTime.Now + APPOPEN_TIMEOUT;
    }

    public void ShowAppOpenAd()
    {
        if (!IsAppOpenAdAvailable)
        {
            return;
        }

        // Register for ad events.
        this.appOpenAd.OnAdDidDismissFullScreenContent += (sender, args) =>
        {
            PrintStatus("App Open ad dismissed.");
            isShowingAppOpenAd = false;
            if (this.appOpenAd != null)
            {
                this.appOpenAd.Destroy();
                this.appOpenAd = null;
            }
        };
        this.appOpenAd.OnAdFailedToPresentFullScreenContent += (sender, args) =>
        {
            PrintStatus("App Open ad failed to present with error: " + args.AdError.GetMessage());

            isShowingAppOpenAd = false;
            if (this.appOpenAd != null)
            {
                this.appOpenAd.Destroy();
                this.appOpenAd = null;
            }
        };
        this.appOpenAd.OnAdDidPresentFullScreenContent += (sender, args) =>
        {
            PrintStatus("App Open ad opened.");
        };
        this.appOpenAd.OnAdDidRecordImpression += (sender, args) =>
        {
            PrintStatus("App Open ad recorded an impression.");
        };
        this.appOpenAd.OnPaidEvent += (sender, args) =>
        {
            string msg = string.Format("{0} (currency: {1}, value: {2}",
                                        "App Open ad received a paid event.",
                                        args.AdValue.CurrencyCode,
                                        args.AdValue.Value);
            PrintStatus(msg);
        };

        isShowingAppOpenAd = true;
        appOpenAd.Show();
    }

    #endregion


    #region AD INSPECTOR

    public void OpenAdInspector()
    {
        PrintStatus("Open ad Inspector.");

        MobileAds.OpenAdInspector((error) =>
        {
            if (error != null)
            {
                PrintStatus("ad Inspector failed to open with error: " + error);
            }
            else
            {
                PrintStatus("Ad Inspector opened successfully.");
            }
        });
    }

    #endregion

    #region Utility

    ///<summary>
    /// Log the message and update the status text on the main thread.
    ///<summary>
    private void PrintStatus(string message)
    {
        Debug.Log(message);
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            // statusText.text = message;
        });
    }

    #endregion




}
