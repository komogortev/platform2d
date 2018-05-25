using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEngine : MonoBehaviour {

	public GameObject wardrobe;
 	RectTransform wardrobeSlider;
	private bool isWardrobeOpen;

	 

	// Use this for initialization
	void Start () {
		isWardrobeOpen = false;
		 
		//set starting state for gear slider
		wardrobeSlider = GameObject.Find ("WardrobeSlider").GetComponent<RectTransform>();
		wardrobeSlider.sizeDelta = new Vector2 (isWardrobeOpen ? 300f : 0, 300f);
		 
	}
	 

	public void ToggleWardrobe () {
		isWardrobeOpen = !isWardrobeOpen;
 		//wardrobe.GetComponent<Animation> ().Play ();

		wardrobe.GetComponent<Animation>()["wardrobe_open"].speed = isWardrobeOpen ? 1 : -1;

		if (isWardrobeOpen)
			wardrobe.GetComponent<Animation>()["wardrobe_open"].time = wardrobe.GetComponent<Animation>()["wardrobe_open"].length;

		wardrobe.GetComponent<Animation>().Play("wardrobe_open");

		//hide/reveal outfit slider
		wardrobeSlider.sizeDelta = new Vector2 (isWardrobeOpen ? 300f : 0, 300f);
 
	}

	void FetchWardrobeInventory () {

	}

	void FetchHatsInventory () {

	}


	public void ApplySelectedGear (int index) {
		PlayerPrefs.SetInt ("gear", index);
	}

	public void ApplySelectedHat (int index) {
		PlayerPrefs.SetInt ("hat", index);
	}



}
