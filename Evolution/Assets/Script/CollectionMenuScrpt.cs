using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectionMenuScrpt : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnIconClick(){
		MenuScript.current.ShowDetail (gameObject.GetComponent<SampleButton>().number);
	}



}
