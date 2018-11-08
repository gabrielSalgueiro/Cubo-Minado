using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cubo : MonoBehaviour {

    public Vector3Int dimensoes;
    public Caixa[][][] matrizCaixas;
    public int nMinas { get; set; }
    public int nCaixasRestantes { get; set; }
    public int nCaixasMarcadas { get; set; }
    public float distanceBetweenTiles;
    public GameObject tilePrefab;
    private Vector3Int lastRealce;
    public Text nMinasRestantesText;
	public Text nCaixasMarcadasText;
    public PlanosDeCorte PDC;

	void Start () {
        nCaixasMarcadas = 0;
        nCaixasRestantes -= nMinas;
        lastRealce = Vector3Int.zero;
	}

    void Update() {
        TextUI();
    }
	
    public void CriaCaixas() {
        GetComponent<RotZoom>().SetPos(dimensoes, distanceBetweenTiles);
        
        Vector3 init = -(dimensoes - new Vector3(1,1,1))*distanceBetweenTiles/2;
        Vector3 offset = init;
        
        nCaixasRestantes = dimensoes.x * dimensoes.y * dimensoes.z;

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
        PDC.SetMatrizCaixas(ref matrizCaixas, dimensoes);
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
						if (!matrizCaixas [pos.x] [pos.y] [pos.z].IsBomb()) {
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

    public void FirstClick(Vector3Int posicaoReferente) {
        // Se a caixa for bomba
        if (matrizCaixas [posicaoReferente.x] [posicaoReferente.y] [posicaoReferente.z].IsBomb()) {
            Vector3Int pos;
            int bombasAdj = 0;

            // Conta quantas bombas estão adjacentes a esta caixa
            for (int i = -1; i < 2; ++i) {			// vai de -1 a 1, percorrendo todos os adjacentes
                for (int j = -1; j < 2; ++j) {
                    for (int k = -1; k < 2; ++k) {
                        pos = new Vector3Int (posicaoReferente.x + i, posicaoReferente.y + j, posicaoReferente.z + k);

                        if (pos.x >= 0 && pos.y >= 0 && pos.z >= 0 && pos.x < dimensoes.x && pos.y < dimensoes.y && pos.z < dimensoes.z) {
                            if (matrizCaixas [pos.x] [pos.y] [pos.z].IsBomb())
                                bombasAdj++;
                        }
                    }
                }
            }

            // Marca a quantidade exata de bombas adjacentes a esta posição, já que ela não é mais uma bomba.
            matrizCaixas [posicaoReferente.x] [posicaoReferente.y] [posicaoReferente.z].bombasAdjacentes = bombasAdj;
            AtualizaAdjacentes(posicaoReferente, -1);   // Retira uma bomba do contador de todos as caixas adjacentes.

            // Agora procura uma nova caixa sem bomba para mover essa antiga bomba.
            for (int i = 0; i < dimensoes.x; i++) {
                for(int j = 0; j < dimensoes.y; j++) {
                    for(int k = 0; k < dimensoes.z; k++) {
                        // Caso a nova posição escolhida não seja uma bomba
                        if (!matrizCaixas[i][j][k].IsBomb()) {
                            // E caso a nova posição não seja a mesma de antes
                            if (i != posicaoReferente.x || j != posicaoReferente.y || k != posicaoReferente.z) {
                                // Coloca uma bomba nesta posição...
                                matrizCaixas[i][j][k].bombasAdjacentes = -1;
                                // Atualiza o contador de bombas dos adjacentes...
                                AtualizaAdjacentes(posicaoReferente, 1);
                                RayCast.OnAbrir -= FirstClick;
                                return; // E encerra a função =)
                            }
                        }
                    }
                }
            }
        }

        RayCast.OnAbrir -= FirstClick;
    }

    public void OtherClicks(Vector3Int posicaoReferente) {
        matrizCaixas[posicaoReferente.x][posicaoReferente.y][posicaoReferente.z].AbrirCaixa();
    }

    public void RealceAdjascentes(Vector3Int posicaoReferente) {
        Vector3Int pos;
		for (int i = -1; i < 2; ++i) {			// vai de -1 a 1
			for (int j = -1; j < 2; ++j) {
				for (int k = -1; k < 2; ++k) {
                    pos = new Vector3Int (lastRealce.x + i, lastRealce.y + j, lastRealce.z + k);

                    if (pos.x >= 0 && pos.y >= 0 && pos.z >= 0 && pos.x < dimensoes.x && pos.y < dimensoes.y && pos.z < dimensoes.z) {
                        matrizCaixas [pos.x] [pos.y] [pos.z].SetRealce(false);
                    }
                }
            }
        }
        
		for (int i = -1; i < 2; ++i) {			// vai de -1 a 1
			for (int j = -1; j < 2; ++j) {
				for (int k = -1; k < 2; ++k) {
                    if (!(i == 0 && j == 0 && k == 0)){                    
                        pos = new Vector3Int (posicaoReferente.x + i, posicaoReferente.y + j, posicaoReferente.z + k);

                        if (pos.x >= 0 && pos.y >= 0 && pos.z >= 0 && pos.x < dimensoes.x && pos.y < dimensoes.y && pos.z < dimensoes.z) {
                            matrizCaixas [pos.x] [pos.y] [pos.z].SetRealce(true);
                        }
                    }
                }
            }
        }

        lastRealce = posicaoReferente;
    }

    public void AbreAdjascentes(Vector3Int posicaoReferente) {
        for (int i = -1; i < 2; ++i) {			// vai de -1 a 1
			for (int j = -1; j < 2; ++j) {
				for (int k = -1; k < 2; ++k) {
                    Vector3Int pos = new Vector3Int (posicaoReferente.x + i, posicaoReferente.y + j, posicaoReferente.z + k);

                    if (pos.x >= 0 && pos.y >= 0 && pos.z >= 0 && pos.x < dimensoes.x && pos.y < dimensoes.y && pos.z < dimensoes.z) {
                        if (!matrizCaixas [pos.x] [pos.y] [pos.z].aberta)
                            matrizCaixas [pos.x] [pos.y] [pos.z].AbrirCaixa();
                    }
                }
            }
        }
    }

    public void ReduzCaixas() {

    }

    public void TextUI() {
        nMinasRestantesText.text = (nMinas - nCaixasMarcadas).ToString("00");
    }
}
