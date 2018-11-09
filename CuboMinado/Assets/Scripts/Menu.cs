using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	public GameObject menu;
	public GameObject telaCreditos;
	public GameObject telaInstrucoes;
	public GameObject telaDificuldade;
	public SaveSystem ss;

	void Start() {
		ss.Load();
	}

	public void Play() {
		//SceneManager.LoadScene ("Dificuldade");
		telaDificuldade.SetActive(true);
		menu.SetActive(false);
	}

	public void instrucoes() {
		telaInstrucoes.SetActive (true);
		menu.SetActive (false);
	}

	public void VoltarI() {
		telaInstrucoes.SetActive (false);
		menu.SetActive (true);
	}

	public void Creditos() {
		telaCreditos.SetActive (true);
		menu.SetActive (false);
	}

	public void VoltarC() {
		telaCreditos.SetActive (false);
		menu.SetActive (true);
	}

	public void Sair() {
		Application.Quit ();
	}

	
}
