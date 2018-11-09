using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerTelaRank : MonoBehaviour {
	public Armazena armazena;
	public SaveSystem ss;
	public Text titulo, tamanho, mina, seuTempo, melhorTempo;
	public Menu menu;

	void Start () {
		if(armazena.venceu)
			titulo.text = "Você Venceu";
		else
			titulo.text = "Você Perdeu";

		tamanho.text = armazena.dimensoes.x.ToString() + "x" + armazena.dimensoes.y.ToString() + "x" + armazena.dimensoes.z.ToString();

		mina.text = armazena.nMinas.ToString();

		var dificuldade = armazena.dificuldade;
		var tempo = armazena.tempo;

		string minutes = Mathf.Floor((tempo % 3600) / 60).ToString("00");
		string seconds = (tempo % 60).ToString("00");
		seuTempo.text = minutes + ":" + seconds;


		if(dificuldade == 1){
			if(armazena.venceu && tempo < armazena.facil){
				armazena.facil = tempo;
				ss.Save();
			}

			if(armazena.facil > 0f){
				minutes = Mathf.Floor((armazena.facil % 3600) / 60).ToString("00");
				seconds = (armazena.facil % 60).ToString("00");
				melhorTempo.text = minutes + ":" + seconds;
			}
		}
		else if(dificuldade == 2){
			if(armazena.venceu && tempo < armazena.medio){
				armazena.medio = tempo;
				ss.Save();
			}

			if(armazena.facil > 0f){
				minutes = Mathf.Floor((armazena.medio % 3600) / 60).ToString("00");
				seconds = (armazena.medio % 60).ToString("00");
				melhorTempo.text = minutes + ":" + seconds;
			}
		}
		else if(dificuldade == 3){
			if(armazena.venceu && tempo < armazena.dificil){
				armazena.dificil = tempo;
				ss.Save();
			}

			if(armazena.facil > 0f){
				minutes = Mathf.Floor((armazena.dificil % 3600) / 60).ToString("00");
				seconds = (armazena.dificil % 60).ToString("00");
				melhorTempo.text = minutes + ":" + seconds;
			}
		}
	}

	public void ClickMenu() {
		SceneManager.LoadScene ("Menu");
	}

	public void ClickJogarNovamente() {
		SceneManager.LoadScene ("Jogo");
	}
}
