using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

/*[System.Serializable]
public class Item {
	//public string name;
	//public Sprite icon;
	//public string type;
	//public string rarity;
	//public bool isChampion;
	public Button.ButtonClickedEvent thingToDo;
}*/

public class CreateScrollList : MonoBehaviour {
	public GameObject sampleButton;
	//public List<Item> itemList;
	public int listSize;
	public Transform contentPanel;
	public static CreateScrollList _current;
	public List<GameObject> itemList;
	int count;
	void Awake(){
		_current = this;
	}
	// Use this for initialization
	void Start () {
		//PopulateList ();
	}

	// Update is called once per frame
	void Update () {
	
	}
	public void PopulateList () {
		for (int i=1; i<=listSize; i++) {
			GameObject newButton = Instantiate (sampleButton) as GameObject;
			SampleButton button = newButton.GetComponent <SampleButton> ();
			button.nameLabel.text = JSONScript._current.GetJsonObject(i-1,"name");
			button.icon.sprite = Resources.Load<Sprite>("Sprites/roleIdle"+i);
			button.number = i-1;
			//button.icon.sprite = item.icon;
			if(GameManager._myGameManager.stage >= i)
				button.icon.enabled = true;
			else
				button.icon.enabled = false;
			
			//button.button.onClick = item.thingToDo;
			newButton.transform.SetParent (contentPanel);

			newButton.transform.localScale = new Vector3(1,1,1);
			newButton.transform.localPosition = new Vector3(transform.position.x,transform.position.y,0);
			itemList.Add(newButton);
		}
		/*foreach (var item in itemList) {
			count ++;
			GameObject newButton = Instantiate (sampleButton) as GameObject;
			SampleButton button = newButton.GetComponent <SampleButton> ();
			button.nameLabel.text = "role"+count;
			button.icon.sprite = Resources.Load<Sprite>("Sprites/roleIdle"+count);
			//button.icon.sprite = item.icon;
			if(GameManager._myGameManager.stage >= count)
				button.icon.enabled = true;
			else
				button.icon.enabled = false;

			//button.button.onClick = item.thingToDo;
			newButton.transform.SetParent (contentPanel);
			newButton.transform.localScale = new Vector3(1,1,1);
			newButton.transform.localPosition = new Vector3(transform.position.x,transform.position.y,0);
		}*/
	}
	public void SetEnable(int s){
		s--;
		if(s<itemList.Count)
			itemList [s].GetComponent<SampleButton> ().icon.enabled = true;
	}

}
