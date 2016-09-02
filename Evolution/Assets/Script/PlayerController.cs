using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public int currentExp;
	public int maxExp;
	public int stage;
	public static PlayerController _current; 
	//public Animation _myAnimation;
	void Awake(){
		_current = this;
	}
	// Use this for initialization
	void Start () {


		StartInvoke ();
	}
	
	// Update is called once per frame
	void Update () {
		#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
		MobileInput ();
		#else
		DesktopInput ();
		#endif



	}
	void DesktopInput(){
		if (Input.GetMouseButtonDown(0)){ // if left button pressed...
			CheckClick("desktop");
		}
	}
	void MobileInput (){
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
			CheckClick("mobile");
		
		}
	}
	void rotate(Vector3 hit){
		/*// Get Angle in Radians
		float AngleRad = Mathf.Atan2(hit.transform.position.y - transform.position.y, hit.transform.position.x - transform.position.x);
		// Get Angle in Degrees
		float AngleDeg = (360 / Mathf.PI) * AngleRad;
		// Rotate Object
		this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);*/
		Vector3 hitPoint = hit;
		
		Vector3 targetDir = hitPoint - transform.position;
		
		float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle+90, Vector3.forward);

		
	}
	void CheckClick(string inputType){
		Ray ray;
		if(inputType.Equals("mobile"))
			ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
		else
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)){
			//if touch gameobject which tag is food
			if(hit.transform.tag == "Food"){
				//CancelInvoke();
				//iTween.StopByName("playerRandomMovement");

				//if food hp =2
				if(hit.transform.GetComponent<FoodScript>().getCurrentHP() == 2){
					//player object face to food
					rotate(hit.point);
					//reduce food hp
					hit.transform.GetComponent<FoodScript>().DecreaseHP();
					//move player object to touch point
					MoveToClickedFood(hit.point);

				}
				else if(hit.transform.GetComponent<FoodScript>().getCurrentHP() == 1){
					//player object face to food
					rotate(hit.point);

					//move player object to touch point
					MoveToClickedFood(hit.point);
					//reduce food hp
					hit.transform.GetComponent<FoodScript>().DecreaseHP();
					//increase player hp by food exp
					addNewExp(hit.transform.GetComponent<FoodScript>().exp);

				}

			}

		}
	}
	void MoveToClickedFood(Vector3 position){
		CancelInvoke ();
		iTween.StopByName ("playerRandomMovement");
		iTween.MoveTo (gameObject,iTween.Hash ("position", position,"time", .5));
		gameObject.GetComponent<Animation> ().PlayAnimation (Animation.STATE.ATTACK);
		
		StartInvoke ();

	}
	void addNewExp(int exp){

		currentExp += exp;
		GameManager._myGameManager.exp = currentExp;
		Debug.Log("added exp: "+ exp);
		if (currentExp >= maxExp) {
			stage ++;
			GameManager._myGameManager.stage = stage;

			maxExp += maxExp;
			GameManager._myGameManager.maxExp = maxExp;
			Animation._myAnimation.Evolution();
			CreateScrollList._current.SetEnable(stage);

		}
		//StartInvoke ();
	}
	void RandomMove(){
		iTween.StopByName ("playerRandomMovement");

		
		float xMove = RandomPositionX();
		float yMove = RandomPositionY();
		Vector3 randomPosition =  new Vector3 (xMove,yMove,0);
		rotate (randomPosition);
		iTween.MoveTo (this.gameObject,iTween.Hash("name",
		                                           "playerRandomMovement",
		                                           "position",
		                                           randomPosition,
		                                           "speed",
		                                           0.1f));
	}
	float RandomPositionX(){
		return Random.Range (0,GameManager._myGameManager.limit.x);
	}
	float RandomPositionY(){
		return Random.Range (0,GameManager._myGameManager.limit.y);
	}

	void StartInvoke(){
		InvokeRepeating ("RandomMove",4f,10f);

	}
	public void InitValue(){
		currentExp = GameManager._myGameManager.exp;
		maxExp = GameManager._myGameManager.maxExp;
		stage = GameManager._myGameManager.stage;
	}
}
