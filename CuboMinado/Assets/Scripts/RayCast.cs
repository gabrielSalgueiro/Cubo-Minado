﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayCast : MonoBehaviour {

	public LayerMask layer;
	RaycastHit hit1, hit2;
	public Manager manager;
	private Cubo cubo;

	private void Start() {
		cubo = GameObject.Find("Cubo").GetComponent<Cubo> ();
	}

	void Update () {
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		//Camera.main.ViewportPointToRay(Input.mousePosition);

		if(manager.jogando){
			if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
				if(Physics.Raycast(ray, out hit1, 10000, layer)) {
					Debug.DrawLine(transform.position, hit1.point);
					Debug.Log(hit1.point);
				}

			if(Physics.Raycast(ray, out hit2, 10000, layer)) {
				hit2.transform.gameObject.GetComponent<Caixa>().RequestRealce();
				
				Vector3 distancia = hit1.point - hit2.point;

				if(distancia.magnitude < .5f){
					if (Input.GetMouseButtonUp(0)) {
						if(hit2.transform.gameObject.GetComponent<Caixa>().marcada == 0)
							hit2.transform.gameObject.GetComponent<Caixa>().AbrirCaixa(new Vector3Int(0, 0, 0));
					}
					else if (Input.GetMouseButtonUp(1))
						hit2.transform.gameObject.GetComponent<Caixa>().MarcarCaixa();
				}
			}
			else {
				cubo.RealceAdjascentes(new Vector3Int(100,100,100));
			}
		}
		
		RaycastHit hitZoom;
		Ray rayZoom = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(Vector3.zero));

		if (Physics.Raycast(rayZoom, out hitZoom, 100, layer)){
			Debug.DrawLine(transform.position, hitZoom.point, Color.red);
			hitZoom.transform.gameObject.GetComponentInParent<RotZoom>().MinZoom(hitZoom.point.z - 2);
		}
	}
}
