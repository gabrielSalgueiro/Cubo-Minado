using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Armazem")]
public class Armazena : ScriptableObject {
	public Vector3Int dimensoes;
	public int nMinas;
	public float tempo;
}
