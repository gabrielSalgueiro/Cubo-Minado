using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
	public static Vector3Int[] dimensaoDificuldade = {
		new Vector3Int (10, 4, 4), // teste  (27)
		new Vector3Int (5, 5, 5), // easy 	(125)
		new Vector3Int (7, 7, 7), // medium (343)
		new Vector3Int (10, 10, 10)	// hard	(1000)
	};
	public static int[] bombasDificuldade = { 0, 7, 17, 50 };
	public Armazena armazena;
	private Cubo cubo;
	public TimeCount time;
	public int dificuldade;
	public bool jogando;
	public Button pause, play;
	


	void Awake () {
		cubo = GameObject.Find("Cubo").GetComponent<Cubo> ();
        cubo.dimensoes = armazena.dimensoes;
		cubo.CriaCaixas ();
		cubo.nMinas = armazena.nMinas;
		GerarMatriz (armazena.nMinas);
		jogando = true;
	}
	
	void Update () {
		
	}

    public void GerarMatriz(int bombas) {
		Vector3Int dim = cubo.dimensoes;
		Vector3Int randPos;
		for (int i = 0; i < bombas; i++) {	// Coloca as n bombas na matriz
			// Gera a posição aleatória onde a bomba será posicionada
			randPos = new Vector3Int (Random.Range (0, dim.x), Random.Range (0, dim.y), Random.Range (0, dim.z));

			if (cubo.matrizCaixas [randPos.x] [randPos.y] [randPos.z].bombasAdjacentes < 0)	// Caso já tenha uma bomba na posição aleatória
				i--;	// Decrementa a variável e tenta colocar dinovo em outra posição
			
			else {	// Caso a posição não tenha uma bomba, adiciona a bomba à posição
				cubo.matrizCaixas [randPos.x] [randPos.y] [randPos.z].bombasAdjacentes = -1;
				cubo.AtualizaAdjacentes (randPos, 1);
			}
		}
	}

    public void AbriuBomba() {
		jogando = false;
		for (int i = 0; i < cubo.dimensoes.x; i++) {
                for(int j = 0; j < cubo.dimensoes.y; j++) {
                    for(int k = 0; k < cubo.dimensoes.z; k++) {
						var caixa = cubo.matrizCaixas[i][j][k];
						if (caixa.IsBomb())
							caixa.MostraBomba();
					}
				}
		}
		Invoke("Derrota", 2.5f);
	}

	public void Derrota(){
		armazena.tempo = time.theTime;
		armazena.venceu = false;
		SceneManager.LoadScene ("TelaDerrota");
    }
   
    public void MarcouCaixa() {

    }

	public void Vitoria() {
		int caixasR = cubo.nCaixasRestantes;
		if(caixasR == 0){
			jogando = false;
			Invoke("TelaVitoria", 1f);
		}
	}

	private void TelaVitoria(){
		armazena.tempo = time.theTime;
		armazena.venceu = true;
		SceneManager.LoadScene ("TelaVitoria");
	}

	public void ClickPlay() {
		pause.gameObject.SetActive(true);
		play.gameObject.SetActive(false);
        jogando = true;
    }

    public void ClickPause() {
		play.gameObject.SetActive(true);
		pause.gameObject.SetActive(false);
        jogando = false;
    }

	public void ClickDesistir(){
		Derrota();
	}
}
