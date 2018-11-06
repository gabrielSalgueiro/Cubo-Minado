using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour {
    public Text text;
    float theTime;
    public float speed = 1.0f;
    public Manager manager;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (manager.jogando == true) {
            theTime += Time.deltaTime * speed;
            string minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00");
            string seconds = (theTime % 60).ToString("00");
            text.text = minutes + ":" + seconds;
        }
    }

    public void ClickPlay() {
        manager.jogando = true;
    }

    public void ClickStop() {
        manager.jogando = false;
    }
}

