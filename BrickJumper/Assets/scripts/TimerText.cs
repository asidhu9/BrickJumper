using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour {

	public delegate void TimerFinished();
	public static event TimerFinished OnTimerFinished;

	Text timer;

	void OnEnable() {
        timer = GetComponent<Text>();
        timer.text = "3";
		StartCoroutine("Timer");
	}

	IEnumerator Timer() {
		int count = 3;
		for (int i = 0; i < count; i++) {
            timer.text = (count - i).ToString();
			yield return new WaitForSeconds(1);
		}

		OnTimerFinished();
	}
}
