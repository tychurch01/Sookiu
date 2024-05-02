using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using TMPro; // If using TextMeshPro

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;
    private int[] highScores = new int[10];
    public TextMeshProUGUI highScoresText; // Reference to your Text or TextMeshProUGUI

    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep it persistent across scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        LoadHighScores();
    }
    
    void Start()
    {
        LoadHighScores();
        FindHighScoreTextComponent();
        UpdateHighScoreUI();
    }

    private void FindHighScoreTextComponent()
    {
        // Assuming the high score text has a unique tag, like "HighScoreText"
        GameObject highScoreTextObj = GameObject.FindGameObjectWithTag("HighScoreText");
        if (highScoreTextObj != null)
        {
            highScoresText = highScoreTextObj.GetComponent<TextMeshProUGUI>();
        }
    }
    
    public void UpdateHighScoreUI()
    {
        if (highScoresText != null)
        {
            highScoresText.text = "High Scores:\n";
            for (int i = 0; i < highScores.Length; i++)
            {
                highScoresText.text += (i + 1) + ". " + highScores[i] + "\n";
            }
        }
        else
        {
            Debug.LogError("HighScoresText is not set in the Inspector.");
        }
    }

    public void SaveHighScore(int newScore)
    {
        // Add new score and sort the array
        highScores[highScores.Length - 1] = newScore;
        highScores = highScores.OrderByDescending(x => x).ToArray();

        // Save the updated scores
        for (int i = 0; i < highScores.Length; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, highScores[i]);
        }

        PlayerPrefs.Save();
    }


    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



    public void LoadHighScores()
    {
        // Load the high scores from PlayerPrefs
        for (int i = 0; i < highScores.Length; i++)
        {
            highScores[i] = PlayerPrefs.GetInt("HighScore" + i, 0);
        }
        // Call update UI method after loading scores
        UpdateHighScoreUI();
    }
}