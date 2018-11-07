using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour {
    public Text text;
    public float theTime;
    public float speed = 1.0f;
    public Manager manager;

    void Update() {
        if (manager.jogando) {
            theTime += Time.deltaTime * speed;
            string minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00");
            string seconds = (theTime % 60).ToString("00");
            text.text = minutes + ":" + seconds;
        }
    }
}

