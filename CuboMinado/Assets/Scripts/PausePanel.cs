using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour {

	public Text facil,medio,dificil;
	public Armazena armazena;

	void Start () {
		facil.text = ToMinute(armazena.facil);
		medio.text = ToMinute(armazena.medio);
		dificil.text = ToMinute(armazena.dificil);
	}

	public string ToMinute(float time) {
        string minutes = Mathf.Floor((time % 3600) / 60).ToString("00");
        string seconds = (time % 60).ToString("00");

		return minutes + ":" + seconds;
	}
}
