using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

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
     * 0 - Epic Music
     * 1 - Light Music INTRO
     * 2 - Light Music LOOP
     */

    private AudioSource source;

    private AudioClip nextTrack;
    
    private void Awake()
    {
        Debug.Log("Sound Manager Awake.");  
        DontDestroyOnLoad(transform.gameObject);
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        Debug.Log("Sound Manager Start.");
        PlayMusic(0);
        nextTrack = music[0];
    }

    void Update()
    {
        if (source.isPlaying) return;
        source.clip = nextTrack;
        source.Play();
    }

    public void PlaySound(int index)
    {
        source.PlayOneShot(sounds[index]);
    }

    public void PlayMusic(int index)
    {
        source.PlayOneShot(music[index]);
    }

    public void MusicTransition(int TransitionIndex, int TrackIndex)
    {
        if (source.isPlaying)
        {
            Debug.Log(source.isPlaying);
            StartCoroutine(Transition(source, 2, music[TransitionIndex], music[TrackIndex]));
        } else
        {
            PlayMusic(TrackIndex);
        }
    }
    
    private IEnumerator Transition(AudioSource audioSource, float fadeTime, AudioClip through, AudioClip too)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
        audioSource.clip = through;
        audioSource.Play();
        nextTrack = too;
    }   
}
