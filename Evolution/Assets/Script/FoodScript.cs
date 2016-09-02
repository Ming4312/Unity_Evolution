using UnityEngine;
using System.Collections;

public class FoodScript : MonoBehaviour {
	private float maxX;
	private float maxY;

	public float moveSpeed = .1f;
	private bool isTarget = false;
	public int maxHp=2;
	private int currentHp;
	public int number;
	public int exp=2;
	public static FoodScript _thisFood;

	void Awake(){
		_thisFood = this;

	}
	// Use this for initialization
	void Start () {
		maxX = GameManager._myGameManager.getScreenWidth ();
		maxY = GameManager._myGameManager.getScreenHeigth ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnEnable(){
		init ();
	}
	void OnDisable(){
		CancelInvoke ();
	}
	void init(){
		isTarget = false;
		currentHp = maxHp;
		startInvoke ();

	}
	void randomMove(){
		float xMove = randomPositionX();
		float yMove = randomPositionY();
		/*if (xMove > transform.position.x) {
			Vector3 theScsle = transform.localScale;
			theScsle *= -1;
			transform.localScale = theScsle;
		} else {

		}*/
		iTween.MoveTo (this.gameObject,iTween.Hash("name",
		                                      "movement"+number,
		                                      "position",
		                                      new Vector3(xMove,yMove,0),
		                                      "speed",
		                                      moveSpeed));
	}
	public void DecreaseHP(){
		currentHp--;
		if (currentHp==1) {
			CancelInvoke();
			iTween.StopByName("movement"+number);
			StartCoroutine(waitForGetUp());
		}
		if (currentHp==0) {
			StopCoroutine("waitForGetUp");
			gameObject.SetActive(false);
		}
	}
	void startInvoke(){
		InvokeRepeating ("randomMove",Random.Range(2f,4f),Random.Range(9f,15f));
	}
	public int getCurrentHP(){
		return currentHp;
	}

	IEnumerator waitForGetUp(){
		yield return new WaitForSeconds (10f);
		startInvoke ();
	}

	float randomPositionX(){
		return Random.Range (0,maxX);
	}
	float randomPositionY(){
		return Random.Range (0,maxY);
	}
}
