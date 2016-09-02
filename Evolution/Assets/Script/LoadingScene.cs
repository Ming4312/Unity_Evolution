using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LoadingScene : MonoBehaviour {
	public string levelToLoad;
	int progress = 0;
	public Text progressText;
	// Use this for initialization
	void Start () {
		StartCoroutine (DisplayLoadingScreen(levelToLoad));
	}
	
	// Update is called once per frame
	void Update () {


	}
	IEnumerator DisplayLoadingScreen(string level){
		AsyncOperation async = Application.LoadLevelAsync (level);
		while(!async.isDone){
			progress =  (int)(async.progress *100);
			progressText.text = "Progress " + progress + "%";
			yield return null;
		}

	}
}
