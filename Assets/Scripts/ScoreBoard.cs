using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

   
    [SerializeField] int TimeBonusPerFrame = 1;

    int score;
    Text scoreText;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
        UpdateScore();
	}
	
	public void ScoreHit(int scoreIncrease)
    {
        score += scoreIncrease;
        UpdateScore();
    }

    public void AddTimeBonus()
    {
        score += TimeBonusPerFrame;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    // Change A
    // Change B
}
