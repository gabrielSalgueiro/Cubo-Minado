using System;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour {
	//public GameObject player;
	public Armazena armazena;
	public string filename;

	[Serializable]
	private class SaveData {
		public float facil, medio, dificil;
	}

	void Awake() {
		Debug.Log(Application.persistentDataPath);
	}

	public void Save() {
		var data = new SaveData();
		
		data.facil = armazena.facil;
		data.medio = armazena.medio;
		data.dificil = armazena.dificil;

		var serializedData = JsonUtility.ToJson(data);
		File.WriteAllText(
			Application.persistentDataPath + '/' + this.filename,
			serializedData);
	}

	public void Load() {
		try {
			var serializedData = File.ReadAllText(
				Application.persistentDataPath + '/' + this.filename);
			var data = JsonUtility.FromJson<SaveData>(serializedData);

			armazena.facil = data.facil;
			armazena.medio = data.medio;
			armazena.dificil = data.dificil;
		}
		catch (Exception e){
			armazena.facil = float.MaxValue;
			armazena.medio = float.MaxValue;
			armazena.dificil = float.MaxValue;

			Save();
		}
	}
}
