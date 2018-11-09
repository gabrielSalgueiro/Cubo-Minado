using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanosDeCorte : MonoBehaviour {

	private Caixa[][][] matrizCaixas;

	private Vector3Int min, max;

	public enum Eixos{
		X, Y, Z
	}

	public PlanoDeCorte PlanoX, PlanoY, PlanoZ;

	public void SetMatrizCaixas(ref Caixa[][][] matrizCaixas, Vector3Int dimensoes){
		this.matrizCaixas = matrizCaixas;
		min = Vector3Int.zero;
		max = dimensoes;
		PlanoX.SetPlano(dimensoes.x, Eixos.X, this);
		PlanoY.SetPlano(dimensoes.y, Eixos.Y, this);
		PlanoZ.SetPlano(dimensoes.z, Eixos.Z, this);
	}

	public void EscondeCaixas(Eixos eixo, int n){
		
		if (eixo == Eixos.X){
			for (int y = min.y; y < max.y; y++){
				for (int z = min.z; z < max.z; z++){
					//Debug.Log(new Vector3Int(n, y, z));
					EscondeCaixa(matrizCaixas[n][y][z]);
				}
			}

			if (n == min.x) min.x++;
			else max.x--; 
		}
		else if (eixo == Eixos.Y){
			for (int x = min.x; x < max.x; x++){
				for (int z = min.z; z < max.z; z++){
					//Debug.Log(new Vector3Int(x, n, z));
					EscondeCaixa(matrizCaixas[x][n][z]);
				}
			}

			if (n == min.y) min.y++;
			else max.y--;
		}
		else {
			for (int x = min.x; x < max.x; x++){
				for (int y = min.y; y < max.y; y++){
					//Debug.Log(new Vector3Int(x, y, n));
					EscondeCaixa(matrizCaixas[x][y][n]);
				}
			}

			if (n == min.z) min.z++;
			else max.z--;
		}
	}

	public void MostraCaixas(Eixos eixo, int n){
		
		if (eixo == Eixos.X){
			for (int y = min.y; y < max.y; y++){
				for (int z = min.z; z < max.z; z++){
					//Debug.Log(new Vector3Int(n, y, z));
					MostraCaixa(matrizCaixas[n][y][z]);
				}
			}

			if (n == min.x-1) min.x--;
			else max.x++; 

		}
		else if (eixo == Eixos.Y){
			for (int x = min.x; x < max.x; x++){
				for (int z = min.z; z < max.z; z++){
					//Debug.Log(new Vector3Int(x, n, z));
					MostraCaixa(matrizCaixas[x][n][z]);
					
				}
			}

			if (n == min.y-1) min.y--;
			else max.y++;
		}
		else {
			for (int x = min.x; x < max.x; x++){
				for (int y = min.y; y < max.y; y++){
					//Debug.Log(new Vector3Int(x, y, n));
					MostraCaixa(matrizCaixas[x][y][n]);
				}
			}

			if (n == min.z-1) min.z--;
			else max.z++;
		}
	}

	private void EscondeCaixa(Caixa caixa){
		if (!caixa.marcada){
			caixa.SMR.enabled = false;
			caixa.GetComponent<BoxCollider>().enabled = false;
			caixa.Realce.GetComponent<MeshRenderer>().enabled = false;
			caixa.img.enabled = false;
		}
	}

	private void MostraCaixa(Caixa caixa){
		if (caixa.bombasAdjacentes != 0 || !caixa.aberta){	
			caixa.SMR.enabled = true;
			caixa.GetComponent<BoxCollider>().enabled = true;
			caixa.Realce.GetComponent<MeshRenderer>().enabled = true;
		}
		if (caixa.bombasAdjacentes > 0 && caixa.aberta){
			caixa.img.enabled = true;
		}
	}

	public void EscondeTudo(){
		for (int x = min.x; x < max.x; x++){
			for (int y = min.y; y < max.y; y++){
				for (int z = min.z; z < max.z; z++){
					EscondeCaixa(matrizCaixas[x][y][z]);
				}
			}
		}
	}

	public void Volta(){
		for (int x = min.x; x < max.x; x++){
			for (int y = min.y; y < max.y; y++){
				for (int z = min.z; z < max.z; z++){
					MostraCaixa(matrizCaixas[x][y][z]);
				}
			}
		}
	}

}
