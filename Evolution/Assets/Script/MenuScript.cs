using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System;


public class MenuScript : MonoBehaviour {
	public Text expText;
	public Text itemText;
	public Text stageText;
	public Button refillButton;

	public Button showAdButton;
	public Button subMenuButton;
	public GameObject subMenu;
	private bool subMenuisOn;
	private bool adAvailable;

	public GameObject collectionMenu;
	public GameObject descriptionPanel;
	private Text name;
	private Text description;
	private Image roleAnimation;
	public static MenuScript current;
	DateTime now ;
	void Awake(){
		current = this;
	}
	// Use this for initialization
	void Start () {

		init ();
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager._myGameManager.item<=0)
			refillButton.enabled = false;
		else
			refillButton.enabled = true;

		/*if (adAvailable) {
			if (Advertisement.isShowing) {
				adAvailable = false;
				showAdButton.enabled = adAvailable;
			} 
		}*/
		expText.text = "Exp: "+GameManager._myGameManager.exp+ " / " + GameManager._myGameManager.maxExp;
		itemText.text = "Item: "+GameManager._myGameManager.item;
		stageText.text = "Stage: " + GameManager._myGameManager.stage;
	}
	void init(){
		expText.text = "Exp: "+GameManager._myGameManager.exp+ " / " + GameManager._myGameManager.maxExp;
		itemText.text = "Item: "+GameManager._myGameManager.item;
		stageText.text = "Stage: " + GameManager._myGameManager.stage;
		now = DateTime.Now;
		collectionMenu.transform.localScale = new Vector3 (0,0,1);
		descriptionPanel.transform.localScale = new Vector3 (0,0,1);
		name = descriptionPanel.gameObject.transform.FindChild ("name").GetComponent<Text>();
		description = descriptionPanel.gameObject.transform.FindChild ("description").GetComponent<Text> ();
		roleAnimation = descriptionPanel.gameObject.transform.FindChild ("roleAnimation").GetComponent<Image> ();

	}
	public void subMenuButtonPress(){
	
		if (subMenuisOn) {
			//close sub menu
			iTween.MoveTo(subMenu,iTween.Hash("x",400,"time",1.5f,"islocal",true));
			subMenuisOn = false;

			//subMenu.SetActive (false);
		} else {
			//open sub menu
			iTween.MoveTo(subMenu,iTween.Hash("x",170,"time",1.5f,"islocal",true));
			subMenuisOn = true;
			DateTime old = GameManager._myGameManager.adPressedTime;
			if(old.Second!= now.Hour||old.Hour != now.Hour || old.Day != now.Day || old.Month != now.Month){
				adAvailable = true;
			}
			else{
				adAvailable = false;
			}
			showAdButton.enabled = adAvailable;
		}

		//iTween.ScaleTo (subMenu,iTween.Hash("x",subMenuisOn,"time",1.5f));
	}
	public void AdButtonPress(){
		GameManager._myGameManager.adPressedTime = DateTime.Now;
	}
	public void showCollectionMenu(){
		iTween.ScaleTo (collectionMenu, iTween.Hash("x",1,"y",1,"time",1f));
	}
	public void CloseCollectionMenu(){
		iTween.ScaleTo (collectionMenu, iTween.Hash("x",0,"y",0,"time",1f));
	}
	public void CloseDetail(){
		iTween.ScaleTo (descriptionPanel, iTween.Hash("x",0,"y",0,"time",1f));
	}
	public void ShowDetail(int n){
		iTween.ScaleTo (descriptionPanel, iTween.Hash("x",1,"y",1,"time",1f));
		if (name != null && description != null&& roleAnimation!=null) {
			name.text = JSONScript._current.GetJsonObject (n, "name");
			description.text = JSONScript._current.GetJsonObject (n, "description");
			roleAnimation.sprite = Resources.Load<Sprite>("Sprites/roleIdle"+(n+1));
			roleAnimation.GetComponent<CollectionRoleAnimation>().SetSprites(n+1);
		}
	}
}
