using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectionRoleAnimation : MonoBehaviour {
	public  Sprite[] idleSprites;
	public  Sprite[] attackSprites;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void SetSprites(int p){
		idleSprites = Resources.LoadAll<Sprite>("Sprites/roleIdle"+p);
		attackSprites = Resources.LoadAll<Sprite> ("Sprites/roleAttack"+p);
		if (idleSprites.Length != 0 && attackSprites.Length != 0) {
			PlayIdleAnimation();
		}
	}
	private void PlayIdleAnimation(){
		iTween.ValueTo (gameObject, iTween.Hash ("name", "colIdle",
		                                         "from", 0,
		                                         "to", idleSprites.Length,
		                                         "time", 1,
		                                         "looptype", iTween.LoopType.loop,
		                                         "onupdate", "OnUpdateITween",
		                                         "onupdatetarget", this.gameObject
		                                         ));
	}
	public void PlayAttackAnimation(){
		StopItween ();
		iTween.ValueTo (gameObject, iTween.Hash ("name", "colAttack",
		                                         "from", 0,
		                                         "to", attackSprites.Length,
		                                         "time", 1,
		                                         "looptype", "none",
		                                         "onupdate", "OnUpdateAttack",
		                                         "onupdatetarget", this.gameObject,
		                                         "oncomplete","OnCompleteAttack",
		                                         "oncompletetarget",this.gameObject));
	}
	void OnUpdateITween(int value){
		if(value<idleSprites.Length)
			gameObject.GetComponent<Image>().sprite = idleSprites [value];
	}
	void OnUpdateAttack(int value){
		if (value < attackSprites.Length)
			gameObject.GetComponent<Image> ().sprite = attackSprites [value];
	}
	void OnCompleteAttack(){
		PlayIdleAnimation ();
	}
	public void StopItween(){
		iTween.StopByName ("colAttack");
		iTween.StopByName ("colIdle");

	}

}
