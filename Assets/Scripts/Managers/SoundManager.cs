using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 0.2f)]
    private float pitchVariation;

    //Singleton class
    public static SoundManager Instance { get; private set; }
    private AudioSource source;
    public void PlayAudio(AudioClip clip, bool randomizePitch = false)
    {
        if (randomizePitch) source.pitch = RandomizePitch();
        else source.pitch = 1f;

        source.PlayOneShot(clip);
    }
    float RandomizePitch()
    {
        float randomValue = Random.Range(-pitchVariation, pitchVariation);
        return 1f + randomValue;
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

        //Get audio source or add
        if (gameObject.GetComponent<AudioSource>() != null)
            source = gameObject.GetComponent<AudioSource>();
        else
        {
            source = gameObject.AddComponent<AudioSource>();
        }
    }

}
