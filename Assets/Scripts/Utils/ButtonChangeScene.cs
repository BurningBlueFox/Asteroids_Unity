using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChangeScene : MonoBehaviour
{
    [SerializeField]
    private int sceneIndexOnArray;
    public void ChangeScene()
    {
        SceneChangeManager.Instance.LoadLevel(sceneIndexOnArray);
    }
}
