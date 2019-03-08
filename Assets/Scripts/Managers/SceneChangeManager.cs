using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    [SerializeField]
    private int MainSceneIndex;
    [SerializeField]
    private int[] LevelsIndex;
    //Singleton class
    public static SceneChangeManager Instance { get; private set; }

    public void LoadLevel(int levelIndex)
    {
        LoadScene(LevelsIndex[levelIndex]);
    }
    public void LoadMainScene()
    {
        LoadScene(MainSceneIndex);
    }
    private void LoadScene(int toLoad)
    {
        SceneManager.LoadScene(toLoad);

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

        //Go to the Main Scene
        LoadMainScene();
    }

    
}
