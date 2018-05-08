using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Converstion1 : MonoBehaviour {

	public GameObject text;
	public float delay = .1f;
	public string fullText;

	string currentText = "";
	GameObject TextBgGo;
	Color ColorValue;

	void OnEnable(){
		TextBgGo = gameObject;
		ColorValue = gameObject.GetComponent<Renderer> ().material.color;

		FadeIn ();
	}

	void StartText () {
		StartCoroutine (ShowText(delay));
	}

	void FadeIn(){
		iTween.ColorTo (TextBgGo, iTween.Hash ("r", ColorValue.r, "g", ColorValue.g, "b", ColorValue.b, "a", .5f, "time", 2, "oncomplete", "StartText", "oncompletetarget", gameObject));
	}

	void FadeOut(){
		iTween.ColorTo (TextBgGo, iTween.Hash ("r", ColorValue.r, "g", ColorValue.g, "b", ColorValue.b, "a", 0, "time", 2, "oncomplete", "End", "oncompletetarget", gameObject));
		iTween.ColorTo (TextBgGo.transform.GetChild(0).gameObject, iTween.Hash ("r", 1, "g", 1, "b", 1, "a", 0, "time", 2));
	}

	void End(){
		gameObject.SetActive (false);
	}

	IEnumerator ShowText(float delay){

		for(int i=0; i<=fullText.Length; i++){
			currentText = fullText.Substring (0, i);
			text.GetComponent<Renderer>().material.color = new Color (1, 1, 1, 1);
			text.GetComponent<TextMesh>().text = currentText;

			if(i==fullText.Length){
				Invoke("FadeOut", 5);
			}

			yield return new WaitForSeconds (delay);
		}

	}


}
