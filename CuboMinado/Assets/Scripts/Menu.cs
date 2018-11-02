using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	public GameObject menu;
	public GameObject telaCreditos;

	public void Creditos(){
		telaCreditos.SetActive (true);
		menu.SetActive (false);
	}

	public void Voltar(){
		telaCreditos.SetActive (false);
		menu.SetActive (true);
	}
	
	public void Play(){
		SceneManager.LoadScene ("Jogo");

	}

	public void Sair(){
		Application.Quit ();
	}
}
