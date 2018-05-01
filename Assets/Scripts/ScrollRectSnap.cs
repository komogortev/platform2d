using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectSnap : MonoBehaviour {
	// ScrollPanel wrapper
	public RectTransform panel;

	// List of menu buttons
	public Button[] btn;

	// Center to compare the distance for each button
	public RectTransform center;

	// All buttons' distance to the center
	private float[] distance;

	// 
	private float[] distReposition;

	// Is true while dragging to prevent button snapping
	private bool dragging = false;

	// The distance between the buttons
	private int btnDistance;

	// Min amount of btns close to the center
	private int minBtnNum;
	private int btnLength;
	 
	// Use this for initialization
	void Start () {
		// Get the total amount of buttons
		btnLength = btn.Length;
		distance = new float[btnLength];
		distReposition = new float[btnLength];

		// Get the distance between the buttons
		float posBtn_0 = btn [0].GetComponent<RectTransform>().anchoredPosition.x;
		float posBtn_1 = btn [1].GetComponent<RectTransform>().anchoredPosition.x;
		btnDistance = (int)Mathf.Abs(posBtn_1 - posBtn_0);
	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < btn.Length; i++) {
			float centerPos = center.GetComponent<RectTransform>().position.x;
			float btnPos = btn [i].GetComponent<RectTransform> ().position.x;
			distReposition[i] =  centerPos - btnPos;
			distance [i] = Mathf.Abs (distReposition[i]);

			if (distReposition [i] > 1000) {
				float curX = btn [i].GetComponent<RectTransform> ().anchoredPosition.x;
				float curY = btn [i].GetComponent<RectTransform> ().anchoredPosition.y;

				Vector2 newAnchoredPos = new Vector2 (curX + (btnLength * btnDistance), curY);
				btn [i].GetComponent<RectTransform> ().anchoredPosition = newAnchoredPos;
			}

			if (distReposition [i] < -1000) {
				float curX = btn [i].GetComponent<RectTransform> ().anchoredPosition.x;
				float curY = btn [i].GetComponent<RectTransform> ().anchoredPosition.y;

				Vector2 newAnchoredPos = new Vector2 (curX - (btnLength * btnDistance), curY);
				btn [i].GetComponent<RectTransform> ().anchoredPosition = newAnchoredPos;
			}
		}

		float minDistance = Mathf.Min (distance);

		for (int a = 0; a < btn.Length; a++) {
			if (minDistance == distance [a]) {
				minBtnNum = a;
			}

			if (!dragging) {
				//LerpToBtn (minBtnNum * -btnDistance);
				float tmp = -btn[minBtnNum].GetComponent<RectTransform>().anchoredPosition.x;
				LerpToBtn (tmp);
			}
		}
		
	}


	void LerpToBtn (float position) {
		float newX = Mathf.Lerp (panel.anchoredPosition.x, position, Time.deltaTime * 10f);
		Vector2 newPosition = new Vector2 (newX, panel.anchoredPosition.y);

		panel.anchoredPosition = newPosition;
	}

	public void StartDrag () {
		dragging = true;
	}

	public void EndDrag () {
		dragging = false;
	}




}
