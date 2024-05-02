using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
    
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private int score; // Private backing field

    public int Score
    {
        get { return score; }
        // If you need a setter, it should be implemented here.
    }


    public TextMeshProUGUI scoreText;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    public void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + Score.ToString();
        }
        else
        {
            Debug.LogError("Score TextMeshProUGUI not assigned in Inspector");
        }
    }

}
