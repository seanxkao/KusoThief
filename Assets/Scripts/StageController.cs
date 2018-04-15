using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageController : MonoBehaviour {

	[SerializeField]	protected GameObject panel;
	[SerializeField]	protected Text showTextP;
	[SerializeField]	protected GameObject amaSpawners;
	[SerializeField]	protected GameObject carSpawners;
	[SerializeField]	protected HpBar hpBar;
	[SerializeField]	protected TimeBar timeBar;

	int counter;
	// Use this for initialization
	void Start () {
		panel.SetActive (true);
	}

	void stop(){
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
		if (Time.time > 1f && counter == 0) {
			Text showText = Instantiate (showTextP, panel.transform);
			showText.text = "3";
			Destroy (showText.gameObject, 2.5f);
			counter++;
		}
		if (Time.time > 2f && counter == 1) {
			Text showText = Instantiate (showTextP, panel.transform);
			showText.text = "2";
			Destroy (showText.gameObject, 2.5f);
			counter++;
		}
		if (Time.time > 3f && counter == 2) {
			Text showText = Instantiate (showTextP, panel.transform);
			showText.text = "1";
			Destroy (showText.gameObject, 2.5f);
			counter++;
		}
		if (Time.time > 4f && counter == 3) {
			Text showText = Instantiate (showTextP, panel.transform);
			showText.text = "Start!";
			Destroy (showText.gameObject, 2.5f);
			counter++;
		}
		if (Time.time > 5f && counter == 4) {
			panel.SetActive (false);
			amaSpawners.SetActive (true);
			carSpawners.SetActive (true);
			timeBar.countDown ();
			counter++;
		}

		//events
		if(hpBar.isFull() && counter==5){
			stop ();
			Text showText = Instantiate (showTextP, panel.transform);
			showText.text = "Full!";
			Destroy (showText.gameObject, 2.5f);
			counter++;
		}
		//events
		if(timeBar.isEmpty() && counter==5){
			stop ();
			Text showText = Instantiate (showTextP, panel.transform);
			showText.text = "Time up!";
			Destroy (showText.gameObject, 2.5f);
			counter++;
		}

	}
}
