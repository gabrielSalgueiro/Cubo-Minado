using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayCast : MonoBehaviour {

	public LayerMask layer;
	RaycastHit hit1, hit2;

	void Update () {
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		//Camera.main.ViewportPointToRay(Input.mousePosition);

		if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
			if(Physics.Raycast(ray, out hit1, 10000, layer)) {
				Debug.DrawLine(transform.position, hit1.point);
				Debug.Log(hit1.point);
			}

			if(Physics.Raycast(ray, out hit2, 10000, layer)) {
				hit2.transform.gameObject.GetComponent<Caixa>().RequestRealce();
				
				Vector3 distancia = hit1.point - hit2.point;

				if(distancia.magnitude < 1.0f){
					if (Input.GetMouseButtonUp(0)) {
						if(hit2.transform.gameObject.GetComponent<Caixa>().marcada == 0)
							hit2.transform.gameObject.GetComponent<Caixa>().AbrirCaixa(new Vector3Int(0, 0, 0));
					}
					else if (Input.GetMouseButtonUp(1))
						hit2.transform.gameObject.GetComponent<Caixa>().MarcarCaixa();
				}
			}
	}
}
