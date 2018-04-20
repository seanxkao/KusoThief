using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetTitle : MonoBehaviour {

    private string[] title = { "很坦的盜賊", "大車輪盜賊", "神聖屬性盜賊", "沒任務接的盜賊", "裝成盜賊的盜賊", "佛系盜賊", "逾期的盜賊" };
    private Text text;

    private void Start () {
        text = this.GetComponent<Text>();
        text.text = "獲得稱號：" + title[(int)Random.Range(0, title.Length)];
    }

}
