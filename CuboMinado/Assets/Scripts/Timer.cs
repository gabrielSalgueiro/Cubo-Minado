using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	float minutos = 0f;
	float segundos = 0f;

	void Start () {
		InvokeRepeating("Tempo", 0.0f, 1.0f);
	}

	public void Tempo() {
		segundos++;
		if(segundos >= 60){
			segundos -= 60;
			minutos++;
		}
	}
}

