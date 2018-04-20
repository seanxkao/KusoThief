using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextTimer : MonoBehaviour {

    public int timer;

    private Text text;

    private void Start () {
        text = this.GetComponent<Text>();
        StartCoroutine(CountDown());
    }

    private void Update () {
        text.text = "剩下" + timer + "秒";
    }

    private IEnumerator CountDown () {
        while (timer >= 0) {
            yield return new WaitForSeconds(1f);
            timer--;
        }
        SceneManager.LoadScene("Lose");
    }

}
