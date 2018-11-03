using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    //[SerializeField]
    public static Manager manager;
    private int powerUp;
    public int marcada { get; set; }
	public bool aberta { get; set; }
	public bool realce { get; set; }
	public bool isBomb { get; private set; }

    public Material Normal, Marcada, Sumida;
    private Animator anim;
    private SkinnedMeshRenderer SMR;
    public List<Sprite> Numbers;
    private Image img;
    public Mina mina;

    void Awake () {
        bombasAdjacentes = 0;
        isBomb = false;
        if(manager == null){
            manager = GameObject.Find("Manager").GetComponent<Manager> ();
            Debug.Log("FINDO");
        }
    }
    
    
    void Start () {
        marcada = 0;
        aberta = false;
        realce = false;
        anim = GetComponent<Animator>();
        SMR = GetComponent<SkinnedMeshRenderer>();
        SMR.material = Normal;
        img = GetComponentInChildren<Image>();
    }

	void Update () {
        
	}

    public void AbrirCaixa() {
        Debug.Log(_adjBomb + " " + isBomb);
        anim.SetTrigger("AbreCaixa");
        Invoke("SUMIU", .65f);
        if(isBomb){
            Invoke("IXPRUDIU", .65f);
            manager.AbriuBomba();
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
        
        if(!isBomb){
            manager.MarcouCaixa();
        }
    }

    public void RequestRealce() {

    }

    public void SetRealce() {

    }

    private void SUMIU(){
        SMR.material = Sumida;
        if (!isBomb && _adjBomb > 0) {
            img.sprite = Numbers[_adjBomb-1];
            img.enabled = true;
        }
    }

    private void IXPRUDIU(){
        mina.gameObject.SetActive(true);
    }
}
