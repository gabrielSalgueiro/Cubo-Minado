using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caixa : MonoBehaviour {

    public Vector3Int posicao { get; set; }
    private int _adjBomb;
	public int bombasAdjacentes { 
		get {
			return _adjBomb;
		}
		set {
			this._adjBomb = value;
			isBomb = (value < 0) ? true: false;
		}
	}
    private static Cubo cubo;
    private int powerUp;
    public int marcada { get; set; }
	public bool aberta { get; set; }
	public bool realce { get; set; }
	public bool isBomb { get; private set; }

    public Material Normal, Marcada;
    private Animator anim;
    private SkinnedMeshRenderer SMR;

    // Use this for initialization
    void Start () {
        bombasAdjacentes = 0;
        marcada = 0;
        aberta = false;
        realce = false;
		isBomb = false;
        anim = GetComponent<Animator>();
        SMR = GetComponent<SkinnedMeshRenderer>();
        SMR.material = Normal;
    }

	void Update () {
        
	}

    public void AbrirCaixa() {
        Debug.Log(_adjBomb);
        anim.SetTrigger("AbreCaixa");
        if(isBomb){
            Debug.Log("KABUUUUM - Perdeu Vacilao");
        }
        else{
            aberta = true;
            gameObject.layer = 9;
        }
    }

    public void MarcarCaixa() {
        if(marcada == 0) {
            marcada = 1;
            SMR.material = Marcada;
        }
        else if(marcada == 1) {
            marcada = 0;
            SMR.material = Normal;
        }

    }

    public int GetMarcada(){
        return marcada;
    }

    public void RequestRealce() {
    }

    public void SetRealce() {

    }
}
