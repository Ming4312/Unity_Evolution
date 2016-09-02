using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class HandleStatusScript : MonoBehaviour {
	private string saveingPath = "/rolestatus.dat";
	public static HandleStatusScript _myHandler ;
	//public static HandleStatusScript _myStatusHandler;
	void Awake(){

		_myHandler = this;
	}
	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SaveStatus(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + saveingPath);

		RoleStatus data = new RoleStatus ();

		data.stage = GameManager._myGameManager.stage;
		data.exp = GameManager._myGameManager.exp;
		data.item = GameManager._myGameManager.item;
		data.maxExp = GameManager._myGameManager.maxExp;
		data.lastTimeFoods = ObjectPool.current.GetNumberOfInActiveObject();
		data.oldTime = timeManager.getCurrentTimeFromLocal ();
		data.adPressedTime = GameManager._myGameManager.adPressedTime;
		Debug.Log ("save time: " + data.oldTime);
		Debug.Log ("Current Foods: " + data.lastTimeFoods);

		bf.Serialize (file,data);
		file.Close ();
	
	}
	public RoleStatus LoadStatus(){
		if (File.Exists (Application.persistentDataPath + saveingPath)) {
	
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + saveingPath, FileMode.Open);
			RoleStatus data = (RoleStatus)bf.Deserialize(file);
			file.Close();

			return data;
		}
		return null;
	}




}
