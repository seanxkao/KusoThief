using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmaQueue : MonoBehaviour {
	[SerializeField]	protected Vector2[] queuePoints;
	protected List<Ama> amas;
	protected int queueMax = 5;

	public static AmaQueue instance = null;

	// Use this for initialization
	void Awake() {
		if (instance != null) {
			Destroy (gameObject);
			return;
		}
		instance = this;
		amas = new List<Ama>();
	}
	
	// Update is called once per frame
	void Update () {
		refreshQueue ();
	}

	public bool isFull(){
		return amas.Count >= queueMax;
	}

	public void refreshQueue(){
		amas.RemoveAll (ama => ama==null || !ama.isQueueing());
		int size = amas.Count;
		for (int i = 0; i < size; i++) {
			amas [i].walkTo (queuePoints [i]);
		}
	}
	public void addAma(Ama ama){
		if (isFull ()) {
			return;
		}
		int count = amas.Count;
		amas.Add (ama);
		ama.walkTo (queuePoints [count]);

	}


}
