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
    public static Cubo cubo;
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
    public GameObject Realce;

    void Awake () {
        bombasAdjacentes = 0;
        isBomb = false;
        if(manager == null){
            manager = GameObject.Find("Manager").GetComponent<Manager> ();
            Debug.Log("FINDO manager");
        }
        if(cubo == null){
            cubo = GameObject.Find("Cubo").GetComponent<Cubo> ();
            Debug.Log("FINDO cubo");
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

    private void LateUpdate() {
        Realce.SetActive(realce);
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
            cubo.nCaixasRestantes--;

            aberta = true;
            gameObject.layer = 9;

            manager.Vitoria();
        }
    }

    public void MarcarCaixa() {
        if(marcada == 0) {
            marcada = 1;
            SMR.material = Marcada;
            cubo.nCaixasMarcadas++;

            manager.Vitoria();
        }
        else if(marcada == 1) {
            marcada = 0;
            SMR.material = Normal;
            cubo.nCaixasMarcadas--;
        }
        
        if(!isBomb){
            manager.MarcouCaixa();
        }
    }

    public void RequestRealce() {
        cubo.RealceAdjascentes(posicao);
    }

    public void SetRealce(bool real) {
        realce = real;
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
