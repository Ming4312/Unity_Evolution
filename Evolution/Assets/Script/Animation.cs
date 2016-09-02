using UnityEngine;
using System.Collections;

public class Animation : MonoBehaviour {
	public Sprite[] idleSprites;
	public Sprite[] attackSprites;
	public Sprite[] eatingSprites;
	public Sprite[] evolutionSprite;
	private Sprite[] currentSprites;
	private STATE currentState;

	private iTween.LoopType loopType;
	public float defaultTotalTime;
	public Vector3 limit;
	private float totalTime;
	public static Animation _myAnimation;
	public enum STATE{
		IDLE,
		ATTACK,
		EAT,
		EVOLUTION
	}
	void Awake(){
		_myAnimation = this;
	}
	// Use this for initialization
	void Start () {
		//getSprites ();
		totalTime = defaultTotalTime;
		limit = GameManager._myGameManager.limit;



	}
	
	// Update is called once per frame
	void Update () {
		//for test animation 
		if (Input.GetKeyDown (KeyCode.A)) {
			iTween.StopByName (currentState.ToString ());
			PlayAnimation (STATE.ATTACK);
		}
		/*transform.position = new Vector3 (Mathf.Clamp (transform.position.x,0,limit.x),
		                                  Mathf.Clamp (transform.position.y,0,(int)limit.y),
		                                  0);*/
	
	}
	void OnUpdateITween(int value){
		if(value<currentSprites.Length)
			gameObject.GetComponent<SpriteRenderer>().sprite = currentSprites [value];
	}
	void OnCompleteITween(){
		if (currentState == STATE.ATTACK || currentState == STATE.EAT || currentState == STATE.EVOLUTION)
			PlayAnimation (STATE.IDLE);
	}
	public void PlayAnimation(STATE s){
		//switch playing animation
		iTween.StopByName (currentState.ToString ());
		currentState = s;
		if (s == STATE.IDLE) {
			currentSprites = idleSprites;
			loopType = iTween.LoopType.loop;
			totalTime = defaultTotalTime;
		} else if (s == STATE.ATTACK) {
			currentSprites = attackSprites;
			loopType = iTween.LoopType.none;
			totalTime = defaultTotalTime ;
		} else if (s == STATE.EAT) {
			currentSprites = eatingSprites;
		}else if(s == STATE.EVOLUTION){
			currentSprites = evolutionSprite;
		}
		iTween.ValueTo (gameObject, iTween.Hash ("name", currentState.ToString(),
		                                      	 "from", 0,
		                                      	 "to", currentSprites.Length,
		                                     	 "time", totalTime,
		                                      	 "looptype", loopType,
		                                         "onupdate", "OnUpdateITween",
		                                         "onupdatetarget", this.gameObject,
		                                         "oncomplete","OnCompleteITween",
		                                         "oncompletetarget",this.gameObject));
		
	}
	public void Evolution(){
		Debug.Log ("Evolution");
		RoleAnimationList._mySpriteList.LoadSprites ();
		getSprites ();
	}
	public void getSprites(){
		idleSprites = RoleAnimationList._mySpriteList.idleSprite;
		attackSprites = RoleAnimationList._mySpriteList.attackSprite;
		PlayAnimation (STATE.IDLE);
	}


}
