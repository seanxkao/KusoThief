using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerSetter : MonoBehaviour {

	[SerializeField] GameObject targetPositions;
	private Transform[] tPositions;
	private Target[] walkers;
    public int minItemLen;
    public int maxItemLen;
    private GameObject[] items;
	private int[] IDs;
    private bool[] used;

	void Awake() {
		// check
		if (minItemLen < 0 || maxItemLen < minItemLen) {
			Debug.LogError ("Wrong item len bounds.");
		}

		// init
		tPositions = new Transform[targetPositions.transform.childCount];
		for (int i = 0; i < tPositions.Length; i++) {
			tPositions [i] = targetPositions.transform.GetChild (i);
		}
		shuffleTransform ();
		walkers = FindObjectsOfType<Target>();
		Object[] objs = Resources.LoadAll ("Prefab/Items");
		items = new GameObject[objs.Length];
		IDs = new int[objs.Length];
		used = new bool[objs.Length];
		for (int i = 0; i < objs.Length; i++) {
			items [i] = (GameObject)objs [i];
			IDs [i] = items [i].GetComponent<Item> ().ID;
			used [i] = false;
		}

		// set positions
		for(int i = 0; i < walkers.Length; i++) {
			walkers[i].transform.position = tPositions [i].position;
		}

		// set items
		Requester requester = FindObjectOfType<Requester> ();
		int targetIdx;
		if (requester != null) {
			targetIdx = getIdxByID (requester.requestID);
		}
		else {
			// for test
			targetIdx = 1;
		}
		used [targetIdx] = true;
		GameObject[] tmpItemList = spawnItemList (Random.Range (minItemLen, maxItemLen + 1));
		used [getIdxByID (tmpItemList [0].GetComponent<Item> ().ID)] = false;
		tmpItemList [0] = items [targetIdx];
		walkers [0].setItems (tmpItemList);

		for (int i = 1; i < walkers.Length; i++) {
			Target walker = walkers [i];
			walker.setItems (spawnItemList (Random.Range (minItemLen, maxItemLen + 1)));
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void shuffleTransform() {
		int len = tPositions.Length;
		for (int i = 0; i < len; i++) {
			Transform tmp = tPositions [i];
			int idx = Random.Range (i, len);
			tPositions [i] = tPositions [idx];
			tPositions [idx] = tmp;
		}
	}
	private int getIdxByID(int id) {
		for (int i = 0; i < IDs.Length; i++) {
			if(id == IDs[i]) {
				return i;
			}
		}
		return -1;
    }
    private GameObject[] spawnItemList(int len) {
        GameObject[] newItems = new GameObject[len];
        for (int i = 0; i < newItems.Length; i++) {
            int idx = getRandomItemIndex ();
            newItems [i] = items[idx];
            used [idx] = true;
        }
        return newItems;
    }
    private int getRandomItemIndex() {
        while(true) {
            int idx = Random.Range (0, items.Length);
            if (!used [idx])
                return idx;
        }
    }
}
