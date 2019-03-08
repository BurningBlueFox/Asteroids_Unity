using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreShow : MonoBehaviour
{

    void Awake()
    {
        this.GetComponent<Text>().text = "HIGHSCORE: " + HighScoreManager.Instance.GetHighScore();
    }

}
