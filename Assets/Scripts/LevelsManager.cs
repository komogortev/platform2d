using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class LevelsManager : MonoBehaviour {

	//Collect UI buttons to disable while action performing
	Button[] buttons;

	//Declare key markers for UI slide actions
	Vector3 sysMenuPos;
	Vector3 sysMenuHiddenPos;

	//Splash screen trigger to main menu
	public float autoLoadNextLevelAfter;

	//Current game status flags
	private bool isMenuScreenOn = true;
	private bool isSettingsDialogVisible = false;

	void Start () {
		buttons = GameObject.Find("SceneCanvas").GetComponentsInChildren<Button>(true);
		 
		sysMenuPos = new Vector3 (Screen.width * 0.5f, Screen.height * 0.5f, 0);
		sysMenuHiddenPos = new Vector3 (Screen.width * 0.5f, Screen.height * 1.5f, 0);
 
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

		isSettingsDialogVisible = !isSettingsDialogVisible;

		//find objects that move
		GameObject systemMenu = GameObject.Find ("MenuPanel");

		StartCoroutine(SlideObject(systemMenu, isSettingsDialogVisible == true ? sysMenuPos : sysMenuHiddenPos, .2f));
	}

	//Toggle to Character room
	public void ToggleRoom () {
		 
		isMenuScreenOn = !isMenuScreenOn;

		//find objects that move
		GameObject menuScene = GameObject.Find ("MenuScene");
		GameObject scenesSlider = GameObject.Find ("ScenesSlider");
 
		//Room position
		Vector3 menuScenePos = new Vector3 (0, 0, 0);
		Vector3 roomScenePos = new Vector3 (0, 10, 0);

		//@Scenes Slider
		Vector3 scenesSliderPos =  new Vector3 (Screen.width * 0.5f, Screen.height * 0.2f, 0);
		Vector3 scenesSliderHidden =  new Vector3 (Screen.width * 0.5f, Screen.height * 1.5f, 0);

		 
		//slide menu/room to camera view position
		StartCoroutine(SlideObject(menuScene, isMenuScreenOn == true ? menuScenePos : roomScenePos, .2f));
		StartCoroutine(SlideObject(scenesSlider, isMenuScreenOn == true ? scenesSliderPos : scenesSliderHidden, .2f));
 	}

	private IEnumerator SlideObject(GameObject obj, Vector3 destination, float delta) {
		//disable all the buttons
		ToggleUI ();
		Debug.Log (obj.name);
		//freeze scene if  menu is visible
		Time.timeScale = isSettingsDialogVisible ? 0f : 0.55f;

		float closeEnough = .2f;
		float distance = (obj.transform.position - destination).magnitude;

		while (distance >= closeEnough) {
			obj.transform.position = Vector3.Slerp (obj.transform.position, destination, delta);
			yield return new WaitForEndOfFrame ();
			//Update distance to final destination;	
			distance = (obj.transform.position - destination).magnitude;
		}

		//final moving touch from closeEnough to final destination
		obj.transform.position = destination;


		//enable all the bubttons
		ToggleUI ();
	}
		
	private void ToggleUI () {
		foreach(Button obj in buttons) {
			obj.GetComponent<Button>().interactable = !obj.GetComponent<Button>().interactable;
		}
	}

	public void Quit () {
		Application.Quit ();
	}

}

 