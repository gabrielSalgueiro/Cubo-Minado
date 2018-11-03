using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mina : MonoBehaviour {

	private Animator anim;
	private ParticleSystem PS;
	private SkinnedMeshRenderer SMR;

	void Start () {
		anim = GetComponent<Animator>();
		PS = GetComponent<ParticleSystem>();
		SMR = GetComponent<SkinnedMeshRenderer>();

		Explode();
	}
	
	public void Explode(){
		anim.SetTrigger("Explode");
		Invoke("ExplodeParticles", 2f);
	}

	private void ExplodeParticles(){
		SMR.enabled = false;
		PS.Play();
	}

}
