using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharityPasser : MonoBehaviour {

	protected float mCharity;
	public float charity{
		get{ 
			return mCharity;
		}
		set{ 
			mCharity = value;
		}
	}
	

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
