using UnityEngine;
using System.Collections;

public class TestPath : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//iTween.MoveTo (gameObject, iTween.Hash("path",iTweenPath.GetPath("path1"),"time",5,"delay",2,"speed",2,"lookahead",1));

	}
	
	// Update is called once per frame
	void Update () {
		/*iTween.LookTo(gameObject,iTween.Hash("looktarget",GameObject.Find("Sphere").transform,
		                                     "time",1));*/
		transform.LookAt (GameObject.Find ("Sphere").transform.position);
	}
}
