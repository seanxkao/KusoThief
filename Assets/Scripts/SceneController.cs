using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    private void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ToMenu();
        }
    }

    public void ToTutorial () {
        SceneManager.LoadScene("Tutorial");
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
