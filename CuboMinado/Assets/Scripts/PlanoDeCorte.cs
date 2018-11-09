using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanoDeCorte : MonoBehaviour {

	public Slider Esq, Dir;

	private PlanosDeCorte PDC;

	private PlanosDeCorte.Eixos eixo;

	private int EV, DV;
	public KeyCode minm, minM, maxm, maxM;

	private void Update() {
		Inputs();

		Esq.onValueChanged.AddListener(delegate { MudouEsq(); });
		Dir.onValueChanged.AddListener(delegate { MudouDir(); });
	}

	private void Inputs(){
		if (Input.GetKeyDown(minm)){
			Esq.value = Mathf.Max(0, Esq.value-1);
		}
		else if (Input.GetKeyDown(minM)){
			Esq.value++;
		}
		else if (Input.GetKeyDown(maxm)){
			Dir.value--;
		}
		else if (Input.GetKeyDown(maxM)){
			Dir.value = Mathf.Min(Dir.maxValue, Dir.value+1);
		}
	}

	private void MudouEsq(){
		if (Esq.value >= Dir.value){
			Esq.value = Dir.value-1;
		}
		for (; EV < Esq.value; EV++)
			PDC.EscondeCaixas(eixo, EV);
		for (; EV > Esq.value; EV--)
			PDC.MostraCaixas(eixo, EV-1);
	}
	
	private void MudouDir(){
		if (Dir.value <= Esq.value){
			Dir.value = Esq.value+1;
		}
		for (; DV > Dir.value; DV--)
			PDC.EscondeCaixas(eixo, DV-1);
		for (; DV < Dir.value; DV++)
			PDC.MostraCaixas(eixo, DV);
	}

	public void SetPlano(int tamanho, PlanosDeCorte.Eixos eixo, PlanosDeCorte PDC){
		Esq.maxValue = tamanho;
		Dir.maxValue = tamanho;
		this.eixo = eixo;
		this.PDC = PDC;
		Esq.value = 0;
		EV = 0;
		Dir.value = tamanho;
		DV = tamanho;
	}

}
