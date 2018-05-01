using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	private static bool isMute;
 
	public void ToggleMute () {
		print (isMute);
		isMute = !isMute;
		AudioListener.volume = isMute ? 0 : 1;
	}
	
	void Start () {
 
	}
}
