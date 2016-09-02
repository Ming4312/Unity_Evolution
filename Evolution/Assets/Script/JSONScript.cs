using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
public class JSONScript : MonoBehaviour {
	private string jsonString;
	private JsonData jsonData;
	private string path;
	private TextAsset textAsset;
	public static JSONScript _current;
	void Awake(){
		_current = this;
		textAsset = Resources.Load("Json/roles_description") as TextAsset;
		jsonData = JsonMapper.ToObject (textAsset.text);
	}
	// Use this for initialization
	void Start () {


		/*jsonString = File.ReadAllText (Application.dataPath+"/roles_description.json");
		jsonData = JsonMapper.ToObject (jsonString);
		Debug.Log (jsonData["roles"][0]["name"]);
		Debug.Log (jsonData["roles"][0]["description"]);*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	IEnumerator LoadJson(){
		WWW www = new WWW (path);
		yield return www;

		jsonData = JsonMapper.ToObject<JsonData>(www.text);
	}
	public string GetJsonObject(int pos, string name){

		return jsonData["roles"][pos][name].ToString();

	}
}
