using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour {
	[SerializeField]	protected GameObject panel;
	[SerializeField]	protected Text showTextP;
	[SerializeField]	protected HpBar hpBar;
	[SerializeField]	protected TimeBar timeBar;
	[SerializeField]	protected GameObject amaSpawners;
	[SerializeField]	protected GameObject carSpawners;
	[SerializeField]	protected KillerSpawner killerSpawner;
	[SerializeField]	protected CharityPasser charityPasser;

	protected float time;

	int counter;
	// Use this for initialization
	void Start () {
		panel.SetActive (true);
		
	}

	void stop(){
		charityPasser.charity = Player.instance.getCharity ();
		panel.SetActive (true);
		amaSpawners.SetActive (false);
		carSpawners.SetActive (false);
		Car[] cars = GameObject.FindObjectsOfType<Car> ();
		foreach (Car car in cars) {
			car.stop ();
		}
	}

	// Update is called once per frame
	void Update () {
		if (time > 1f && counter == 0) {
			Text showText = Instantiate (showTextP, panel.transform);
			showText.text = "3";
			Destroy (showText.gameObject, 2.5f);
			counter++;
			time = 0;
		}
		if (time > 1f && counter == 1) {
			Text showText = Instantiate (showTextP, panel.transform);
			showText.text = "2";
			Destroy (showText.gameObject, 2.5f);
			counter++;
			time = 0;
		}
		if (time > 1f && counter == 2) {
			Text showText = Instantiate (showTextP, panel.transform);
			showText.text = "1";
			Destroy (showText.gameObject, 2.5f);
			counter++;
			time = 0;
		}
		if (time > 1f && counter == 3) {
			Text showText = Instantiate (showTextP, panel.transform);
			showText.text = "Start!";
			Destroy (showText.gameObject, 2.5f);
			counter++;
			time = 0;
		}
		if (time > 1f && counter == 4) {
			panel.SetActive (false);
			amaSpawners.SetActive (true);
			carSpawners.SetActive (true);
			timeBar.countDown ();
			counter++;
			time = 0;
		}
		if (hpBar.isKillerActivate () && !killerSpawner.isActiveAndEnabled) {
			killerSpawner.gameObject.SetActive (true);
		}
		//events
		if(hpBar.isFull() && counter==5){
			stop ();
			Text showText = Instantiate (showTextP, panel.transform);
			showText.text = "Full!";
			Destroy (showText.gameObject, 2.5f);
			counter++;
			time = 0;
		}
		//events
		if(timeBar.isEmpty() && counter==5){
			stop ();
			Text showText = Instantiate (showTextP, panel.transform);
			showText.text = "Time up!";
			Destroy (showText.gameObject, 2.5f);
			counter++;
			time = 0;
		}
		if (time > 1f && counter == 6) {
			SceneManager.LoadScene ("Request");
		}

		time += Time.deltaTime;
	}
}
