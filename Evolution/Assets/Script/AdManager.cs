using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {
	[SerializeField]
	string gameId= "1029369";
	[SerializeField]
	bool isTest = true;
	void Awake(){
		Advertisement.Initialize(gameId,isTest);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ShowAd(string zone = ""){
		//#if UNITY_EDITOR
		StartCoroutine(WaitForAd());
		//#endif

		if (string.Equals (zone, ""))
			zone = null;

		ShowOptions options = new ShowOptions ();
		options.resultCallback = AdCallbackhandler;

		if (Advertisement.IsReady (zone)) {
			Advertisement.Show(zone,options);
		}
	}
	void AdCallbackhandler(ShowResult result){
		switch (result) {
		case ShowResult.Finished:
			Debug.Log("Ad Finished.");
			GameManager._myGameManager.item++;
			break;
		case ShowResult.Skipped:
			Debug.Log("Ad Skipped.");
			break;
		case ShowResult.Failed:
			Debug.Log("Ad Failed");
			break;
		}
	}

	IEnumerator WaitForAd(){
		float currentTimeScale = Time.timeScale;
		Time.timeScale = 0f;
		yield return null;
		while(Advertisement.isShowing){
			yield return null;
		}
		Time.timeScale = currentTimeScale;
	}

}
