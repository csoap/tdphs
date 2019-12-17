using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager _instance;
    private Dictionary<string, AudioClip> audioDict = new Dictionary<string, AudioClip>();
    public AudioClip[] audioClipArray;
    public AudioSource audioSource;
    public bool isQuiet = false;

    void Awake()
    {
        _instance = this;
    }
	void Start () {
        foreach (AudioClip ac in audioClipArray)
        {
            audioDict.Add(ac.name, ac);
        }

    }

    public void Play(string audioName)
    {
        if (isQuiet) return;
        AudioClip ac;
        if (audioDict.TryGetValue(audioName, out ac))
        {
            //AudioSource.PlayClipAtPoint(ac, Vector3.zero);
            this.audioSource.PlayOneShot(ac);
        }

    }

    public void Play(string audioName, AudioSource mAudioSource)
    {
        if (isQuiet) return;
        AudioClip ac;
        if (audioDict.TryGetValue(audioName, out ac))
        {
            mAudioSource.PlayOneShot(ac);
        }

    }
}
