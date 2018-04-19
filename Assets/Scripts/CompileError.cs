using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompileError : MonoBehaviour {
	public Text t;
	public GameObject obj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			obj.SetActive (true);
			t.text = "幹";
		}
	}
}
