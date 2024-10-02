using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundOnAwake : MonoBehaviour
{

    public List<AudioClip> audioClips;

    private AudioSource thisAudioSource;

    void Awake()
    {
        //Tocar um som aleatorio
        thisAudioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        AudioClip audioClip = audioClips[Random.Range(0, audioClips.Count)];
        thisAudioSource.PlayOneShot(audioClip);

    }
}
