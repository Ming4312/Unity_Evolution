using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ObjectPool : MonoBehaviour {
	public GameObject prefab; // target created object
	public int initailSize = 20; // pool size

	public List<GameObject> _pool;
	public List<GameObject> _myPool;

	public static ObjectPool current;

	public GameObject parent;

	public bool willGrow = false;
	void Awake(){
		current = this;
		_pool = new List<GameObject>();
		for (int i = 0; i < initailSize; i++) {
			GameObject obj = (GameObject)Instantiate(prefab);
			obj.SetActive(false);
			obj.GetComponent<FoodScript>().number = i;
			obj.transform.parent = parent.transform;
			_pool.Add(obj);
			
		}

	}
	// Use this for initialization
	void Start () {

		//_myPool = _pool;
	}

	public GameObject GetPoolObject(){
		for (int i =0; i<_pool.Count ;i++) {
			if(!_pool[i].activeInHierarchy){
				return _pool[i];
			}
		}
		if (willGrow) {
			GameObject obj = (GameObject)Instantiate(prefab);
			//obj.SetActive(false);
			_pool.Add(obj);
			return obj;

		}
		return null;
	}
	public int GetNumberOfInActiveObject(){
		int count=0;
		for (int i =0; i< _pool.Count; i++) {
			if(_pool[i].activeInHierarchy){
				count++;
			}
		}
		return count;
	}
	public int CountNotInActiveObject(){
		int count = 0;
		for (int i=0; i < _pool.Count; i++) {
			if(!_pool[i].activeInHierarchy)
				count++;
		}
		return count;
	}
}
