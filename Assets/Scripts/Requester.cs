using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Requester : MonoBehaviour {

    private Object[] items;
    private Item requestItem;
    public GameObject requestImage;
    public int requestID;

    void Awake() {
        items = Resources.LoadAll ("Prefab/Items");
        requestItem = ((GameObject)items[Random.Range (0, items.Length)]).GetComponent<Item>();
        requestImage.GetComponent<Image> ().sprite = requestItem.getImage ();
        requestID = requestItem.GetComponent<Item> ().ID;
        DontDestroyOnLoad (gameObject);
    }
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Sprite getTargetImage() {
        return requestItem.getImage();
    }
}
