using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class playerDoll : MonoBehaviour {

	List<string> hats = new List<string>();
	List<string> gear = new List<string>();


	int currentHat;
	int currentGear;

	// Use this for initialization
	void Start () {
		currentHat = PlayerPrefs.GetInt ("hat");
		currentGear = PlayerPrefs.GetInt ("gear");
	}

	void FixedUpdate () {
		if (PlayerPrefs.GetInt ("hat") != currentHat) {
			currentHat = PlayerPrefs.GetInt("hat");

		}

		if (PlayerPrefs.GetInt ("gear") != currentHat) {
			Debug.Log (PlayerPrefs.GetInt ("gear") != currentGear);
			Debug.Log (PlayerPrefs.GetInt ("gear"));
			Debug.Log (currentGear);
			currentGear = PlayerPrefs.GetInt("gear");

		}


		 
		
	}
	



	public void putOnGear () {

	}
	public void putOnHat () {

	}

}
