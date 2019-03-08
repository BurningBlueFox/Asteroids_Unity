using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField]
    private MenuSounds menuSounds;

    public void PlayAny()
    {
        AudioClip clip = menuSounds.GetAny();
        SoundManager.Instance.PlayAudio(clip, true);
    }
}
