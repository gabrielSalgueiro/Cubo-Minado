using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerTelaDificuldade : MonoBehaviour {
	public Armazena armazena;
	public Slider eixoX, eixoY, eixoZ, nMinas;
	public Text textEixoX, textEixoY, textEixoZ, textNMinas;
	public int dificuldade;
	public GameObject telaMenu, telaDificuldade;
	
	void Start() {
		if(armazena.dificuldade == 0){
			Customizar();
			eixoX.value = armazena.dimensoes.x;
			eixoY.value = armazena.dimensoes.y;
			eixoZ.value = armazena.dimensoes.z;
			nMinas.value = armazena.nMinas;
		}
		else if(armazena.dificuldade == 1)
			Facil();
		else if(armazena.dificuldade == 2)
			Medio();
		else
			Dificil();
	}
	
	void Update(){
		nMinas.maxValue = (eixoX.value*eixoY.value*eixoZ.value) - 1;

		textEixoX.text = eixoX.value.ToString();
		textEixoY.text = eixoY.value.ToString();
		textEixoZ.text = eixoZ.value.ToString();
		textNMinas.text = nMinas.value.ToString();
	}

	public void Facil() {
		dificuldade = 1;

		eixoX.interactable = false;
		eixoY.interactable = false;
		eixoZ.interactable = false;
		nMinas.interactable = false;

		eixoX.value = 5;
		eixoY.value = 5;
		eixoZ.value = 5;
		nMinas.value = 6;
	}

	public void Medio() {
		dificuldade = 2;

		eixoX.interactable = false;
		eixoY.interactable = false;
		eixoZ.interactable = false;
		nMinas.interactable = false;

		eixoX.value = 7;
		eixoY.value = 7;
		eixoZ.value = 7;
		nMinas.value = 17;
	}

	public void Dificil() {
		dificuldade = 3;

		eixoX.interactable = false;
		eixoY.interactable = false;
		eixoZ.interactable = false;
		nMinas.interactable = false;

		eixoX.value = 10;
		eixoY.value = 10;
		eixoZ.value = 10;
		nMinas.value = 50;
	}

	public void Customizar() {
		dificuldade = 0;

		eixoX.interactable = true;
		eixoY.interactable = true;
		eixoZ.interactable = true;
		nMinas.interactable = true;
	}

	public void Iniciar() {
		int X = (int)eixoX.value;
		int Y = (int)eixoY.value;
		int Z = (int)eixoZ.value;
		int Minas = (int)nMinas.value;

		armazena.dimensoes = new Vector3Int(X, Y, Z);
		armazena.nMinas = Minas;
		armazena.dificuldade = dificuldade;

		SceneManager.LoadScene ("Jogo");
	}

	public void Voltar() {
		telaMenu.SetActive(true);
		telaDificuldade.SetActive(false);
	}
}
