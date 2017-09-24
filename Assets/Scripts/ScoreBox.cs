using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBox : MonoBehaviour {

    public Text scoreText;

    private int score = 0;

	void OnTriggerEnter (Collider other) {
        Debug.Log("Collided with " + other.name);
        score += 1;
        scoreText.text = "Score: " + score;
    }
}
