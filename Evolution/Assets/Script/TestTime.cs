using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;


public class TestTime : MonoBehaviour {
	[SerializeField]
	private Text t;

	private TimeSpan difference;
	// Use this for initialization
	void Start () {
		init ();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Home) || Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}
	//if game has closed
	void OnApplicationQuit()
	{

		// do stuff here!
		timeManager.saveQuitTime ();
	}
	void OnApplicationPause(){
		timeManager.saveQuitTime ();
	}
	void init(){
		t = t.GetComponent<Text> ();
		/*if (PlayerPrefs.HasKey ("oldTime")) {
			difference = timeManager.calTimeSpan();
			t.text = "total seconds: "+difference.TotalSeconds;
			Debug.Log("total s: " + difference.TotalSeconds);

		}*/
		if (File.Exists (Application.persistentDataPath + "/gameTimeInfo.dat")) {
			difference = timeManager.calTimeSpan (timeManager.loadOldTime ());
			t.text = "total seconds: " + difference.TotalSeconds;
			Debug.Log ("total s: " + difference.TotalSeconds);
		}
	}
}
