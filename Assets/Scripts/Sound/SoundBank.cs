using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBank : MonoBehaviour
{
    [SerializeField]
    AudioClip[] audioClips;

    public AudioClip GetAny()
    {
        int length = audioClips.Length;
        int index = Random.Range(0, length);
        return audioClips[index];
    }
}
