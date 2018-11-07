using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Caixa : MonoBehaviour {

    public Vector3Int posicao;
	public int bombasAdjacentes;
    public static Cubo cubo;
    public static Manager manager;
    public int marcada;
	public bool aberta;
	public bool realce;

    public Material Normal, Marcada, Sumida;
    private Animator anim;
    public SkinnedMeshRenderer SMR;
    public List<Sprite> Numbers;
    public Image img;
    public Mina mina;
    public GameObject Realce;

    void Awake () {
        bombasAdjacentes = 0;
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

    public bool IsBomb(){
        return bombasAdjacentes < 0;
    }

    public void AbrirCaixa() {
        Debug.Log("abrindo caixa "+ posicao + IsBomb());
        anim.SetTrigger("AbreCaixa");
        Invoke("SUMIU", .65f);

        if(IsBomb()){
            Invoke("IXPRUDIU", .65f);
            manager.AbriuBomba();
        }
        else{
            cubo.nCaixasRestantes--;

            aberta = true;
            gameObject.layer = 9;

            manager.Vitoria();
        }

        if (bombasAdjacentes == 0)
            cubo.AbreAdjascentes(posicao);
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
        
        if(!IsBomb()){
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
        if (bombasAdjacentes > 0) {
            img.sprite = Numbers[bombasAdjacentes-1];
            img.enabled = SMR.enabled;
        }
        else {
            SMR.enabled = false;
        }
    }

    private void IXPRUDIU(){
        mina.gameObject.SetActive(true);
    }
}
