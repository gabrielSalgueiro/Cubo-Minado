using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayCast : MonoBehaviour {

    public delegate void Abrir (Vector3Int pos);
    public static event Abrir OnAbrir;

	public LayerMask layer;
	RaycastHit hit1, hit2;
	public Manager manager;
	public Cubo cubo;

	private void Start() {
		OnAbrir = cubo.FirstClick;
		OnAbrir += cubo.OtherClicks;
	}

	void Update () {
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if(manager.jogando){
			if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
				if(Physics.Raycast(ray, out hit1, 10000, layer)) {
					Debug.DrawLine(transform.position, hit1.point);
					Debug.Log(hit1.point);
				}

			if(Physics.Raycast(ray, out hit2, 10000, layer)) {
				Caixa caixa = hit2.transform.gameObject.GetComponent<Caixa>();
				caixa.RequestRealce();
				Debug.DrawLine(transform.position, hit2.point);
				
				Vector3 distancia = hit1.point - hit2.point;

				if(distancia.magnitude < .5f){
					if (Input.GetMouseButtonUp(0)) {
						if(!hit2.transform.gameObject.GetComponent<Caixa>().marcada)
							OnAbrir(caixa.posicao);
					}
					else if (Input.GetMouseButtonUp(1))
						caixa.MarcarCaixa();
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
