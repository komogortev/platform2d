using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class LevelsManager : MonoBehaviour {



	//	public GameObject loadingImage;
	//public Text totalScoreCount;

	public float autoLoadNextLevelAfter;

	private bool isMenu = true;
	private bool isSettingsVisible = false;

	void Start () {
		if (SceneManager.GetActiveScene().name == "00 Splash") {
			Invoke ("LoadMainMenu", autoLoadNextLevelAfter); 	

			//totalScoreCount.text = "Scores: " + PlayerPrefs.GetInt("TotalScore").ToString();
			//Debug.Log ("AA" + totalScoreCount.text);
		}


	}
	   
	public void LoadMainMenu(){
		SceneManager.LoadScene ("_Main");
	}

	public void LoadScene(string level){
		//loadingImage.SetActive (true);
		SceneManager.LoadScene (level);
	}
		

	public void ToggleSettings () {
		isSettingsVisible = !isSettingsVisible;

		//find objects that move
		GameObject systemMenu = GameObject.Find ("SystemMenu");
		Vector3 menuMarker = new Vector3 (250, 120, 0);
		Vector3 sysMenuMarker = new Vector3 (250, 400, 0);

		StartCoroutine(SlideMenu(systemMenu, isSettingsVisible == true ? menuMarker : sysMenuMarker, .2f));
	}

	//Toggle to Character room
	public void ToggleRoom () {
		isMenu = !isMenu;

		//find objects that move
		GameObject menuScene = GameObject.Find ("MenuScene");
		GameObject scenesNav = GameObject.Find ("ScenesNav");
		//set menu positions
		Vector3 menuMarker = new Vector3 (0, 0, 0);
		Vector3 roomMarker = new Vector3 (0, 10, 0);
		//@Todo init canvas values for nav
		Vector3 navMenu = new Vector3 (309.5f, 174f, 0f);
		Vector3 navRoom = new Vector3 (309.5f, 664f, 0f);

		//slide menu/room to camera view position
		StartCoroutine(SlideMenu(menuScene, isMenu == true ? menuMarker : roomMarker, .2f));
		StartCoroutine(SlideMenu(scenesNav, isMenu == true ? navMenu : navRoom, .2f));
	}


	private IEnumerator SlideMenu(GameObject scene, Vector3 location, float delta) {
		float closeEnough = .2f;
		float distance = (scene.transform.position - location).magnitude;
		  
		while (distance >= closeEnough) {
			scene.transform.position = Vector3.Slerp (scene.transform.position, location, delta);
			yield return new WaitForEndOfFrame ();
			//Update distance to final location;	
			distance = (scene.transform.position -	 location).magnitude;
		}

		//final moving touch from closeEnough to final location
		scene.transform.position = location;

	}




	public void Quit(){
		Application.Quit ();
	}

}

 