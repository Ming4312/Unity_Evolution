using UnityEngine;
using System.Collections;

public class CreateFoodScript : MonoBehaviour {
	public float interval=10f;
	public static CreateFoodScript _myCreator;
	void Awake(){
		_myCreator = this;
	}
	// Use this for initialization
	void Start () {
		LastTimeFoods ();
		DiffTimeFoods ();


		InvokeRepeating ("createFood",2f,interval);
	}
	public void LastTimeFoods(){
		int last=GameManager._myGameManager.lastTimeFoods;
		if( last !=0){
			for(int i =0;i<last;i++){
				createFood();
			}
			Debug.Log("Last Created Foods: " + last);
		}
	}
	public void DiffTimeFoods(){
		if(GameManager._myGameManager.timeGap!=null){
			
			float totalSeconds = GameManager._myGameManager.getTimeGapTotalSeconds();
			for(int i=0; i<ObjectPool.current._pool.Count; i++){
				totalSeconds -= interval;
				if(totalSeconds >0 ){
					createFood();
				}
			}
			
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
	void createFood(){
		GameObject obj = ObjectPool.current.GetPoolObject ();
		if (obj == null)
			return;
		obj.transform.position = new Vector2(Random.Range(0,GameManager._myGameManager.limit.x),
		                                     Random.Range(0,GameManager._myGameManager.limit.y));

		obj.SetActive (true);
	
	}
	public void Refill(){
		for (int i=0; i<ObjectPool.current._pool.Count; i++) {
			createFood();
		}
		GameManager._myGameManager.item--;
	}

}
