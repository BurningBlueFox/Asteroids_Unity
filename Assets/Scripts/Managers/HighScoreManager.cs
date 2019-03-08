using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField]
    private string key = "HighScore";
    private int highScore;
    //Singleton class
    public static HighScoreManager Instance { get; private set; }

    public void DeleteHighScore()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        highScore = 0;
    }
    public void SubmitScore(int score)
    {
        if(score > highScore)
        {
            highScore = score;
            SaveScore(score);
        }
    }
    public void SaveScore(int score)
    {
        PlayerPrefs.SetInt(key, score);
        PlayerPrefs.Save();
    }
    public int GetHighScore()
    {
        return highScore;
    }
    void Awake()
    {
        //Initiate Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);

        //Get high score if available
        //otherwise create new key
        if (PlayerPrefs.HasKey(key))
        {
            highScore = PlayerPrefs.GetInt(key);
        }
        else
        {
            highScore = 0;
            SaveScore(highScore);
        }
    }

    
}
