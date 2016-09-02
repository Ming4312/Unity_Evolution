using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float speed = 1;

	private Transform _myTransform;
	void Awake(){
		_myTransform = transform;
	}


	
	// Update is called once per frame
	void Update () {
		if (_myTransform.position.y < 7)
			_myTransform.Translate (_myTransform.up * speed * Time.deltaTime);
		else
			gameObject.SetActive (false);
	}
}
