using UnityEngine;
using System.Collections;

public class RoleAnimationList : MonoBehaviour {
	public Sprite[] idleSprite;
	public Sprite[] attackSprite;
	public Sprite[] eatingSprite;
	public Sprite[] evolutionSprite;

	
	public static RoleAnimationList _mySpriteList;
	void Awake(){

		_mySpriteList = this;

	}
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void LoadSprites(){
		int s = GameManager._myGameManager.stage;

		idleSprite = Resources.LoadAll<Sprite>("Sprites/roleIdle"+s);
		attackSprite = Resources.LoadAll<Sprite> ("Sprites/roleAttack"+s);
		Animation._myAnimation.getSprites ();

	}




}
