﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
public class ShopManager : MonoBehaviour {

	public Text removeAdsPriceText;
	public Text smallPackPriceText;
	public Text mediumPackPriceText;
	public Text bigPackPriceText;

	// Main scene buttons
	public GameObject mainScreenCoinsHolder;
	public GameObject mainScreenStarsHolder;
	public GameObject levelSelectButtonsHolder;
	public GameObject worldSelectButtonsHolder;
	public GameObject mainSceneShopBackButtonHolder;
	public GameObject mainSceneShopCoinsHolder;

	// Level scene buttons
	public GameObject levelSceneCoinsHolder;
	public GameObject pauseButtonHolder;
	public GameObject levelSceneShopBackButtonHolder;
	public GameObject levelSceneShopCoinsHolder;

	// Animacija za dodavanje coina
	public GameObject addCoinsAnimationHolder;

	// Video nije dostupan popup
	public GameObject videoNotAvailablePopup;
	public string rewardedVideoZone;
    public string nonRewardedVideoZone;
//	void Awake()
//	{
//		// Setujemo cene
//		if (GlobalVariables.removeAdsOwned)
//		{
//			// Setujemo text za cenu da bude owned i stavljamo kvacicu da je remove ads kupljen
//			removeAdsPriceText.text = "Owned";
//			removeAdsPriceText.transform.parent.Find("BuyButton/BoughtImage").gameObject.SetActive(true);
//			removeAdsPriceText.transform.parent.Find("BuyButton/Text").gameObject.SetActive(false);
//		}
//		else
//		{
//			removeAdsPriceText.text = GlobalVariables.removeAdsPrice;
//		}
//
//		smallPackPriceText.text = GlobalVariables.smallCoinsPackPrice;
//		mediumPackPriceText.text = GlobalVariables.mediumCoinsPackPrice;
//		bigPackPriceText.text = GlobalVariables.bigCoinsPackPrice;
//
//	}


	public void AddCoinsAnimation()
	{
		StartCoroutine("AddCoinsCoroutine");
	}

	IEnumerator AddCoinsCoroutine()
	{
		addCoinsAnimationHolder.SetActive(true);
//		SoundManager.Instance.Play_Sound(SoundManager.Instance.claimExtraCoins);

		yield return new WaitForSeconds(2f);

		if (SceneManager.GetActiveScene().name == "MainScene")
			LevelSelectManager.levelSelectManager.RefreshStarsAndCoins();
		else if (SceneManager.GetActiveScene().name == "Level")
		{
			GameplayManager.gameplayManager.coinsText.text = GlobalVariables.coins.ToString();
			GameplayManager.gameplayManager.coinsTextShop.text = GlobalVariables.coins.ToString();
		}

		addCoinsAnimationHolder.SetActive(false);
	}

	public void WatchVideoForCoins()
	{
		//ShowRewardedAds();
		AdsManager.Instance.IsVideoRewardAvailable();
#if UNITY_EDITOR
		AddCoinsAfterVideoWatched();
#endif
	}

	public void AddCoinsAfterVideoWatched()
	{
		// Dodajemo coine i pustamo animaciju

		GlobalVariables.globalVariables.AddCoins(100);
		addCoinsAnimationHolder.transform.Find("AnimationHolder/CoinsHolder/CoinsNumberTextShop").GetComponent<Text>().text = "+100";
		AddCoinsAnimation();

	}

public bool GetRewardedUnityAdsReady() {
	#if UNITY_ADS

			rewardedVideoZone = "rewardedVideo";
			if (Advertisement.IsReady(rewardedVideoZone)) {
				return true;
			}
			else {
				rewardedVideoZone = "rewardedVideoZone";
				if (Advertisement.IsReady(rewardedVideoZone)) {
					return true;
				}
			}
	#endif

        return false;
    }	
	public void ShowRewardedAds() {
	#if UNITY_ADS

			if (GetRewardedUnityAdsReady()) {
				Advertisement.Show(rewardedVideoZone, new ShowOptions {
					resultCallback = result => {
						if (result == ShowResult.Finished) {
							GlobalVariables.globalVariables.AddCoins(30);
		addCoinsAnimationHolder.transform.Find("AnimationHolder/CoinsHolder/CoinsNumberTextShop").GetComponent<Text>().text = "+30";
		AddCoinsAnimation();
						}
					}
				});
			}
	#endif
    }
}
