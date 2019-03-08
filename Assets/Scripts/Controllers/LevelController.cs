using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private PlayerShipController player;
    [SerializeField]
    private ShipLevel level;
    [SerializeField]
    private PlayerStatus status;
    private int playerScore = 0;
    [SerializeField]
    private Text scoreText;
    //Singleton class
    public static LevelController Instance { get; private set; }

    public int GetPlayerShotDamage()
    {
        return level.GetShotDamage();
    }
    public Vector3 GetPlayerPosition()
    {
        return player.gameObject.transform.position;
    }
    public void AddPlayerExperience(int amount)
    {
        status.IncrementXP(amount);
        playerScore += amount;
        scoreText.text = "SCORE: " + playerScore;
        HighScoreManager.Instance.SubmitScore(playerScore);
    }
    public Color GetShipColor()
    {
        return level.GetColor();
    }
    public void ChangeShipLevel(ShipLevel lvl)
    {
        level = lvl;
    }
    void Awake()
    {
        //Initiate Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this.gameObject);
        scoreText.text = "SCORE: " + playerScore;
    }

    void Update()
    {
        if (PlayerInputManager.Instance.GetEscapePrompt())
            SceneChangeManager.Instance.LoadMainScene();
    }
}
