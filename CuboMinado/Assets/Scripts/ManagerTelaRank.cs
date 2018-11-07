using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerTelaRank : MonoBehaviour {
	private float tempo;
	public Armazena armazena;

	void Start () {
		tempo = armazena.tempo;
		armazena.tempo = 0f;
	}
	
	void Update () {
		
	}

	public void ClickMenu() {
		SceneManager.LoadScene ("Menu");
	}

	public void ClickEscolherDificuldade() {
		SceneManager.LoadScene ("Dificuldade");
	}

	public void ClickJogarNovamente() {
		SceneManager.LoadScene ("Jogo");
	}
}
