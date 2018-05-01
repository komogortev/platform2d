using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	//	public GameObject loadingImage;

	public void LoadScene(string level){
		//loadingImage.SetActive (true);
		SceneManager.LoadScene (level);
	}

	public void Quit(){
		Application.Quit ();
	}



}

 