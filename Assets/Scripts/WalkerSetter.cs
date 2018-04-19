using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerSetter : MonoBehaviour {

    [SerializeField] private Target[] walkers;
    public int minItemLen;
    public int maxItemLen;
    [SerializeField] private GameObject[] items;
    private bool[] used;

	// Use this for initialization
	void Start () {
        if (minItemLen < 0 || maxItemLen < minItemLen) {
            Debug.LogError ("Wrong item len bounds.");
        }
        Object[] objs = Resources.LoadAll ("Prefab/Items");
        items = new GameObject[objs.Length];
        used = new bool[objs.Length];
        for (int i = 0; i < objs.Length; i++) {
            items [i] = (GameObject)objs [i];
            used [i] = false;
        }

        // TODO
        int targetIdx = getTargetItemIdx ();
        used [targetIdx] = true;
        // GameObject[] tmpItemList = spawnItemList (Random.Range (minItemLen, maxItemLen + 1));

        for (int i = 1; i < walkers.Length; i++) {
            Target walker = walkers [i];
            walker.setItems (spawnItemList (Random.Range (minItemLen, maxItemLen + 1)));
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private int getTargetItemIdx() {
        return 0;
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
