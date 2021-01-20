using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
    // FEILDS ==============================================================================================

    public AudioClip[] sounds;
    /*
     * SOUND DIRECTORY
     * 0 - oof
     * 
     * 
     */

    public AudioClip[] music;
    /*
     * MUSIC DIRECTORY
     * 0 - Light Music INTRO
     * 1 - Light Music LOOP
     * 2 - Heavy Music INTRO
     * 3 - Heavy Music LOOP
     * 4 - Epic Music
     */

    private AudioSource source;

    private AudioClip nextTrack;

    // SETUP ==============================================================================================

    //All the Game Management Objects live in DontDestroyOnLoad!
    private void Awake()
    {
        Debug.Log("Sound Manager Awake.");  
        DontDestroyOnLoad(transform.gameObject);
        source = GetComponent<AudioSource>();
    }

    //Title Screen Music
    void Start()
    {
        Debug.Log("Sound Manager Start.");
        PlayMusic(1, 2);
    }

    //This keeps Loop Tracks playing
    void Update()
    {
        if (source.isPlaying) return;
        source.clip = nextTrack;
        source.Play();
    }

    // PUBLIC METHODS ==========================================================================================

    //SFX here
    public void PlaySound(int index)
    {
        source.PlayOneShot(sounds[index]);
    }

    //ALL EXTRIOR MUSIC SHOULD GO THROUGH HERE, Just give the index of the INTRO and the LOOP and we'll do the rest
    public void MusicTransition(int TransitionIndex, int TrackIndex)
    {
        if (source.isPlaying)
        {
            Debug.Log("Music playing: " + source.isPlaying);
            StartCoroutine(Transition(source, 2, TransitionIndex, TrackIndex));
        } else
        {
            PlayMusic(TransitionIndex, TrackIndex);
        }
    }

    // PRIVATE METHODS ==========================================================================================

    //This simply plays an intro then queues up a Loop that will be played infinately until this method is called again.
    private void PlayMusic(int INTRO, int LOOP)
    {
        source.clip = music[INTRO];
        source.Play();
        nextTrack = music[LOOP];
    }

    //This Fades Out whatevers playing, queues up your LOOP, and plays your INTRO
    private IEnumerator Transition(AudioSource audioSource, float fadeTime, int INTRO, int LOOP)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = startVolume;

        PlayMusic(INTRO, LOOP);
    }   
}