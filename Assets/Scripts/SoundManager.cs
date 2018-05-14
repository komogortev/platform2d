using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {
	private static bool isMute;
	public Sprite volumeOn, volumeOff;

	private SpriteRenderer spriteRenderer;

	void Start () {
	}

	//method linked directly to audio setting button
	public void ToggleMute () {
		isMute = !isMute;
		AudioListener.volume = isMute ? 0 : 1;

		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		spriteRenderer.sprite = isMute ? volumeOn : volumeOff;
			

	}
	
}
