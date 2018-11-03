using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubo : MonoBehaviour {

    public Vector3Int dimensoes;
    public Caixa[][][] matrizCaixas;
    public int nMinas { get; set; }
    public int nCaixasR { get; set; }
    public int nCaixasMarcadas { get; set; }
    public float rotSpeed;
    public float distanceBetweenTiles = 1.1f;
    public GameObject tilePrefab;
    
   

	void Start () {
	}
	
	void Update () {
		Rotaciona();
	}

    public void CriaCaixas() {
        Vector3 init = -(dimensoes - new Vector3(1,1,1))*distanceBetweenTiles/2;
        Vector3 offset = init;
        
        matrizCaixas = new Caixa[dimensoes.x][][];

        for (int i = 0; i < dimensoes.x; i++) {
            matrizCaixas[i] = new Caixa[dimensoes.y][];
            
            for(int j = 0; j < dimensoes.y; j++) {
            matrizCaixas[i][j] = new Caixa[dimensoes.z];    

                for(int k = 0; k < dimensoes.z; k++) {
                    GameObject box = (GameObject) Instantiate(tilePrefab, transform.position + offset, transform.rotation, transform);
                    matrizCaixas[i][j][k] = box.GetComponentInChildren<Caixa>();
                    matrizCaixas[i][j][k].posicao = new Vector3Int (i, j, k);
                    //matrizCaixas[i][j][k].gameObject.transform.SetParent(transform);
                    offset.z += distanceBetweenTiles;
                }
                offset.z = init.z;
                offset.y += distanceBetweenTiles;
            }
            offset.z = init.z;
            offset.y = init.y;
            offset.x += distanceBetweenTiles;
        }
    }

    public void ConfereMatriz() {
		for (int i = 0; i < dimensoes.x; ++i) {
			for (int j = 0; j < dimensoes.y; ++j) {
				string line = "";
				for (int k = 0; k < dimensoes.z; ++k) {
					line = string.Concat(line, matrizCaixas[i][j][k].bombasAdjacentes.ToString());
					line = string.Concat(line, " ");
				}
				Debug.Log(line);
			}
			Debug.Log("-------");
		}
	}

    public void AtualizaAdjacentes(Vector3Int posicaoReferente,int n) {
		Vector3Int pos;
		for (int i = -1; i < 2; ++i) {			// vai de -1 a 1
			for (int j = -1; j < 2; ++j) {
				for (int k = -1; k < 2; ++k) {
					// pos é uma possível posição adjacente àquela referente
					pos = new Vector3Int (posicaoReferente.x + i, posicaoReferente.y + j, posicaoReferente.z + k);

					/*// Caso todos sejam 0, a posição é igual à referente, então continua (deseja atualizar apenas posições adjacentes)
					if (i == j && j == k && k == 0)
						continue;
                    */
					// Confere se a nova posição é válida, checando se está dentro das dimensões do cubo
					if (pos.x >= 0 && pos.y >= 0 && pos.z >= 0 && pos.x < dimensoes.x && pos.y < dimensoes.y && pos.z < dimensoes.z) {
						// Confere se a caixa não possui bomba, pois não faria sentido atualizar o valor de uma bomba, mas sim dos números
						if (!matrizCaixas [pos.x] [pos.y] [pos.z].isBomb) {
							/*// Caso esteja diminuindo as bombas e o número de bombas desta caixa já seja zero, não diminui mais
							if (n < 0 && matrizCaixas [pos.x] [pos.y] [pos.z].bombasAdjacentes <= 0)
								continue;
							*/
                            matrizCaixas [pos.x] [pos.y] [pos.z].bombasAdjacentes += n;	// Finalmente atualiza as bombas adjacentes :D
						}
					}
				}
			}
		}
    }

    public void FirstClick() {

    }

    public void Rotaciona() {
        if (Input.GetMouseButton(1)){
			Vector2 rot = new Vector2(
				Input.GetAxis("Mouse X") * rotSpeed,
				Input.GetAxis("Mouse Y") * rotSpeed
			);

			transform.RotateAround(Vector3.down, rot.x);
			transform.RotateAround(Vector3.right, rot.y);
		}
    }

    public void RealceAdjascentes() {
        
    }

    public void AbreAdjascentes() {

    }

    public void ReduzCaixas() {

    }

}
