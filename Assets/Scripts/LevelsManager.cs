using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class LevelsManager : MonoBehaviour {

	//	public GameObject loadingImage;
	public Text totalScoreCount;

	void Start () {
		if (SceneManager.GetActiveScene().name == "_Main") {
			totalScoreCount.text = "Scores: " + PlayerPrefs.GetInt("TotalScore").ToString();
			Debug.Log ("AA" + totalScoreCount.text);
		}
	}

	public void LoadScene(string level){
		//loadingImage.SetActive (true);
		SceneManager.LoadScene (level);
	}
		
	public void Quit(){
		Application.Quit ();
	}

}

 