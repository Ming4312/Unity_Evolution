using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
public class TestUnityAds : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Advertisement.Initialize("1029369",true);

		StartCoroutine (ShowAdWhenReady());
	}
	IEnumerator ShowAdWhenReady(){
		while (!Advertisement.IsReady())
			yield return null;
		Advertisement.Show();
	}
}
