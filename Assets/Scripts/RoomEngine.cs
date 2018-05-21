using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEngine : MonoBehaviour {

	public GameObject wardrobe;
 	RectTransform wardrobeSlider;
	private bool isOpen;

	// Use this for initialization
	void Start () {
		isOpen = false;
 
		//set starting state for outfit slider
		wardrobeSlider = GameObject.Find ("WardrobeSlider").GetComponent<RectTransform>();
		wardrobeSlider.sizeDelta = new Vector2 (isOpen ? 300f : 0, 300f);

	}
	 

	public void ToggleWardrobe () {
		isOpen = !isOpen;
 		//wardrobe.GetComponent<Animation> ().Play ();

		wardrobe.GetComponent<Animation>()["wardrobe_open"].speed = isOpen ? 1 : -1;

		if (isOpen)
			wardrobe.GetComponent<Animation>()["wardrobe_open"].time = wardrobe.GetComponent<Animation>()["wardrobe_open"].length;

		wardrobe.GetComponent<Animation>().Play("wardrobe_open");


		//hide/reveal outfit slider
		wardrobeSlider.sizeDelta = new Vector2 (isOpen ? 300f : 0, 300f);
 
	}

	void FetchWardrobeInventory () {

	}

	void ScrollWardrobeInventory () {
	
	}

	void ApplySelectedItem () {
	
	}



}
