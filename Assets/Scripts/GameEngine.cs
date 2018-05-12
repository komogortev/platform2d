using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameEngine : MonoBehaviour {
	 
	//Game Stats
	private int lifes;
	public Text lifeCount;
	public LevelManager levelManager;

	private int totalScore;
	public Text totalScoreCount;

	private int score;
	public Text scoreCount;

	public Text exitLevelText;

	void Start () {
		Time.timeScale = 0.5f; 
		lifes = 3;
		score = 0;
		totalScore = PlayerPrefs.GetInt ("TotalScore");
		//exitLevelText.text = "";

		SetLifeCountText ();
		SetScoreCountText ();
		  
	}
	 
	void FixedUpdate () { 
		if (lifes == 0) {
			SetExitLevelText ("You lost");
			totalScore += score;
			SetTotalScoreCountText ();
			StartCoroutine ("ExitLevel"); 
		}
	}
	 
	void OnTriggerEnter2D (Collider2D other) {
		//Execute collision scenarios
		if (other.gameObject.CompareTag("Enemy")) {
			lifes = --lifes;
			SetLifeCountText ();
		}  

		if (other.gameObject.CompareTag("Object")) {
			score += 10;
			SetScoreCountText ();
		}  

		if (other.gameObject.CompareTag("ExitGate")) {
			score += 100;
			totalScore += score;

			SetScoreCountText ();
			SetTotalScoreCountText ();

			SetExitLevelText ("You won");
			StartCoroutine ("ExitLevel"); 
		}  
	}

	void SetScoreCountText () {
		scoreCount.text = "Scores: " + score.ToString();
	}

	void SetTotalScoreCountText () {
		PlayerPrefs.SetInt ("TotalScore", totalScore);
		totalScoreCount.text = "Scores: " + totalScore.ToString();
		Debug.Log ("total " + PlayerPrefs.GetInt ("TotalScore", totalScore).ToString());
	}

	void SetLifeCountText () {
		lifeCount.text = "Count: " + lifes.ToString();
	}

	void SetExitLevelText (string msg) {
		exitLevelText.text = msg;
	}

	private IEnumerator ExitLevel () {
		Time.timeScale = 0; 
		yield return new WaitForSecondsRealtime (2);
		Time.timeScale = 1;
		levelManager.LoadScene ("_Main");
	}
}

