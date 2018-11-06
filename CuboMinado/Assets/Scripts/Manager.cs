using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {
	public static Vector3Int[] dimensaoDificuldade = {
		new Vector3Int (3, 3, 3), // teste  (27)
		new Vector3Int (5, 5, 5), // easy 	(125)
		new Vector3Int (7, 7, 7), // medium (343)
		new Vector3Int (10, 10, 10)	// hard	(1000)
	};
	public static int[] bombasDificuldade = { 2, 7, 17, 50 };
	//private Vector3Int dimensaoCustom;

	private Cubo cubo;
	public int dificuldade;
	public bool jogando { get; set; }


	void Awake () {
		cubo = GameObject.Find("Cubo").GetComponent<Cubo> ();
        cubo.dimensoes = dimensaoDificuldade [dificuldade];
		cubo.CriaCaixas ();
		cubo.nMinas = bombasDificuldade [dificuldade];
		GerarMatriz (bombasDificuldade [dificuldade]);
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
		Invoke("Derrota", 3f);
	}

	public void Derrota(){
		SceneManager.LoadScene ("TelaDerrota");
    }
   
    public void MarcouCaixa() {

    }

	public void Vitoria() {
		int caixasR = cubo.nCaixasRestantes;
		if(caixasR == 0){
			jogando = false;
			SceneManager.LoadScene ("TelaVitoria");
		}
	}
}
