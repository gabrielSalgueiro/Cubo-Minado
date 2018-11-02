using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayCast : MonoBehaviour {

	public LayerMask layer;

	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if(Physics.Raycast(ray, out hit, 10000, layer)) {
			Debug.DrawLine(transform.position, hit.point);

			hit.transform.gameObject.GetComponent<Caixa>().RequestRealce();

			if (Input.GetMouseButtonUp(0)) {
	            if(hit.transform.gameObject.GetComponent<Caixa>().GetMarcada() == 0)
					hit.transform.gameObject.GetComponent<Caixa>().AbrirCaixa();
			}
		    else if (Input.GetMouseButtonUp(1)) {
		    	hit.transform.gameObject.GetComponent<Caixa>().MarcarCaixa();
		    }
		}
	}
}
