using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerTelaFinal : MonoBehaviour {

	public Armazena armazena;
	public Text tempo;


	void Start () {
		string minutes = Mathf.Floor((armazena.tempo % 3600) / 60).ToString("00");
		string seconds = (armazena.tempo % 60).ToString("00");
		tempo.text = minutes + ":" + seconds;
	}

	public void ClickContinuar(){
		SceneManager.LoadScene ("Rank");
	}
}
