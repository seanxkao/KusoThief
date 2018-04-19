using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToCharity () {
		SceneManager.LoadScene ("Charity");	
	}

	public void ToSteal () {
		SceneManager.LoadScene ("Steal");
	}

	public void ToMenu () {
		SceneManager.LoadScene ("Menu");
	}

	public void Quit () {
		Application.Quit ();
	}

}
