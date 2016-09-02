using UnityEngine;
using System.Collections;

public class Creator : MonoBehaviour {
	public float fireTime = .5f;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("Fire",fireTime,fireTime);
	}
	void Fire(){
		GameObject obj = ObjectPool.current.GetPoolObject ();
		if (obj == null)
			return;
		obj.transform.position = transform.position;
		obj.transform.rotation = transform.rotation;
		obj.SetActive (true);
	}
}
