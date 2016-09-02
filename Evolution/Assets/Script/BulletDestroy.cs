using UnityEngine;
using System.Collections;

public class BulletDestroy : MonoBehaviour {

	void OnEnable(){
		Invoke ("Destroy",2f);
	}
	void Destroy(){
		gameObject.SetActive (false);
		if(!gameObject.activeInHierarchy)
		Debug.Log ("d");
	}
	void OnDisable(){
		CancelInvoke ();
	}
}
