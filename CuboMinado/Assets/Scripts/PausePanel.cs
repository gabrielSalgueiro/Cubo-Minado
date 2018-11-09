using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour {

	public Text facil,medio,dificil;
	public Armazena armazena;

	void Start () {
		facil.text = armazena.facil.ToString();
		medio.text = armazena.medio.ToString();
		dificil.text = armazena.dificil.ToString();
	}
}
