using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundmanager : MonoBehaviour
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
        DontDestroyOnLoad(transform.gameObject);
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        //Debug.Log("This did appen");
        StartCoroutine(DoIt(2));
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
        }
    }

    public IEnumerator Transition(AudioSource audioSource, float fadeTime, AudioClip through, AudioClip too)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

            Debug.Log("eternal loop");

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;

        Debug.Log("This happens");

        audioSource.clip = through;
        audioSource.Play();
        nextTrack = too;
    }   

    public IEnumerator DoIt(int pauseTime)
    {
        PlayMusic(0);
        Debug.Log("waiting... " + Time.time);
        yield return new WaitForSeconds(pauseTime);
        Debug.Log("play!" + Time.time);
        MusicTransition(1, 2);
    }

}
