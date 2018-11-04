using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayCast : MonoBehaviour {

	public LayerMask layer;

	void Update () {
		RaycastHit hit1, hit2;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		//Camera.main.ViewportPointToRay(Input.mousePosition);

		//if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
			if(Physics.Raycast(ray, out hit1, 10000, layer)) {
				Debug.DrawLine(transform.positaion, hit1.point);
				Debug.Log(hit1.point);
			}
				//hit1.transform.gameObject.GetComponent<Caixa>().RequestRealce();

				if (Input.GetMouseButtonUp(0)) {
					if(hit1.transform.gameObject.GetComponent<Caixa>().marcada == 0)
						hit1.transform.gameObject.GetComponent<Caixa>().AbrirCaixa();
				}
				else if (Input.GetMouseButtonUp(1))
					hit1.transform.gameObject.GetComponent<Caixa>().MarcarCaixa();
			//}
	}
}
