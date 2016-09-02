using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {
	public int stage = 1;
	public int exp = 0;
	public int item = 0;
	public int maxExp = 10;
	public int lastTimeFoods;
	public Vector3 limit;
	public DateTime oldTime;
	public static GameManager _myGameManager;
	public TimeSpan timeGap;
	public DateTime adPressedTime;

	void Awake(){
		_myGameManager = this;
		limit = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));	
		limit = new Vector3 (limit.x,limit.y-1f,0);
	}
	// Use this for initialization
	void Start () {
		LoadStatus ();
		CreateScrollList._current.PopulateList ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Home) || Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}
	void OnApplicationQuit(){
		SaveStatus ();
	}
	void OnApplicationPause(bool pauseStatus){


		#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
		if(pauseStatus)
			SaveStatus ();
		else if(!pauseStatus) {
			LoadStatus();
			CreateFoodScript._myCreator.DiffTimeFoods();
		}
		#endif


	}

	void LoadStatus(){
		RoleStatus r = HandleStatusScript._myHandler.LoadStatus ();
		if (r != null) {
			stage = r.stage;
			exp = r.exp;
			item = r.item;
			maxExp = r.maxExp;
			lastTimeFoods = r.lastTimeFoods;
			oldTime = r.oldTime;
			adPressedTime = r.adPressedTime;
			timeGap = timeManager.calTimeSpan(oldTime);
		}
		RoleAnimationList._mySpriteList.LoadSprites ();
		PlayerController._current.InitValue ();

		//Animation._myAnimation.getSprites ();


	}
	void SaveStatus(){
		HandleStatusScript._myHandler.SaveStatus ();
	}
	public float getScreenWidth(){
		return limit.x;
	}
	public float getScreenHeigth(){
		return limit.y;
	}
	public float getTimeGapTotalSeconds(){
		Debug.Log ("time Gap: "+timeGap.TotalSeconds);
		return (float)timeGap.TotalSeconds;
	}
	public void Reset(){
		stage = 1;
		exp = 0;
		maxExp = 10;
		item = 10;
		PlayerController._current.InitValue ();
		RoleAnimationList._mySpriteList.LoadSprites ();
	}

}
