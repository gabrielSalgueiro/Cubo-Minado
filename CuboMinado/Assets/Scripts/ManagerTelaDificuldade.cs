using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerTelaDificuldade : MonoBehaviour {
	public Armazena armazena;
	public Slider eixoX, eixoY, eixoZ, nMinas;
	public Text textEixoX, textEixoY, textEixoZ, textNMinas;
	
	void Start() {
		eixoX.minValue = 4;
		eixoX.maxValue = 15;
		eixoY.minValue = 4;
		eixoY.maxValue = 15;
		eixoZ.minValue = 4;
		eixoZ.maxValue = 15;
		nMinas.minValue = 4;
	}
	
	void Update(){
		nMinas.maxValue = (eixoX.value*eixoY.value*eixoZ.value) - 28;

		textEixoX.text = eixoX.value.ToString();
		textEixoY.text = eixoY.value.ToString();
		textEixoZ.text = eixoZ.value.ToString();
		textNMinas.text = nMinas.value.ToString();
	}

	public void Facil() {
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

		SceneManager.LoadScene ("Jogo");
	}

	public void Voltar() {
		SceneManager.LoadScene ("Menu");
	}
}
