using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
  * Scene:Splash
  * Object:Main Camera
  * author: dotmobstudio@gmail.com
  **/
public class SplashScene : MonoBehaviour {
	
	int appStartedNumber;
	AsyncOperation progress = null;
	Image progressBar;
	float myProgress=0;
	string sceneToLoad;
	public GameObject GDPR_Popup;
	// Use this for initialization
	void Start ()
	{
		// CheckForGDPR();
			//		if(PlayerPrefs.HasKey("TutorialCompleted"))
			//		{
			sceneToLoad = "MainScene";
//		}
//		else
//			sceneToLoad = "TutorialLevel";
		
		//progressBar = GameObject.Find("ProgressBar").GetComponent<Image>();
		if(PlayerPrefs.HasKey("appStartedNumber"))
		{
			appStartedNumber = PlayerPrefs.GetInt("appStartedNumber");
		}
		else
		{
			appStartedNumber = 0;
		}
		appStartedNumber++;
		PlayerPrefs.SetInt("appStartedNumber",appStartedNumber);
		StartCoroutine(LoadScene());
	}
	
	
	IEnumerator LoadScene()
	{
		yield return new WaitForSeconds(2.0f);
		Application.LoadLevel(sceneToLoad);
		
		
	}

	//GDPR
	void CheckForGDPR()
	{
		if (PlayerPrefs.GetInt("npa", -1) == -1)
		{
			//show gdpr popup
			GDPR_Popup.SetActive(true);
			//pause the game
			Time.timeScale = 0;
		}
	}


	//Popup events
	public void OnUserClickAccept()
	{
		PlayerPrefs.SetInt("npa", 0);
		//hide gdpr popup
		GDPR_Popup.SetActive(false);
		//play the game
		Time.timeScale = 1;
	}

	public void OnUserClickCancel()
	{
		PlayerPrefs.SetInt("npa", 1);
		//hide gdpr popup
		GDPR_Popup.SetActive(false);
		//play the game
		Time.timeScale = 1;
	}

	public void OnUserClickPrivacyPolicy()
	{
		Application.OpenURL("https://privacyURL.com"); //your privacy url
	}


	void Update()
	{

		
	}
}
