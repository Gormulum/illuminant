using UnityEngine;
using Yarn.Unity;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource music;
    [SerializeField] AudioSource soundEvent;
    [SerializeField] AudioSource UI;
    [Header("Event")]
    [SerializeField] AudioClip stinger;
    [SerializeField] AudioClip boatCrash;
    [SerializeField] AudioClip corruptedFisherman;
    [SerializeField] AudioClip goodEnding;
    [SerializeField] AudioClip badEnding;


    [Header("Choices")]
    [SerializeField] AudioClip goodDecision;
    [SerializeField] AudioClip badDecision;


    [Header("Music")]
    [SerializeField] AudioClip goodMusic;
    [SerializeField] AudioClip badMusic;

    void Start()
    {
        music.clip = goodMusic;
        music.Play();
    }

    // Update is called once per frame

    [YarnCommand("play_oneshot")]
    public void PlayOneShot(string name)
    {
        switch(name)
        {
            case ("stinger"):
                soundEvent.clip = stinger;
                soundEvent.PlayOneShot(stinger);
                break;
            case ("boatCrash"):
                soundEvent.clip = boatCrash;
                soundEvent.PlayOneShot(boatCrash);
            break;
        }
    }

    
}
