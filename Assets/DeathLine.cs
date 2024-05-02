using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Fruit fruit = other.GetComponent<Fruit>();

        if (fruit != null && fruit.currentState == Fruit.State.Resting)
        {
            EndGame(); // Call EndGame without passing fruit's points
        }
    }



    public void EndGame()
    {
        int totalScore = ScoreManager.instance.Score;
        HighScoreManager.instance.SaveHighScore(totalScore);
        HighScoreManager.instance.ReloadScene();
    }
}
