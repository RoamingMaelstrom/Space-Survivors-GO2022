using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOEvents;

public class MainMusicLogic : MonoBehaviour
{
    [SerializeField] SOEvent enterPauseEvent;
    [SerializeField] SOEvent exitPauseEvent;
    [SerializeField] AudioSource musicPlayer1;
    [SerializeField] AudioSource musicPlayer2;
    [SerializeField] TrackContainer trackContainer;

    int currentTrackIndex = 0;

    public bool running = true;

    public float transitionLength = 1f;
    [SerializeField] [Range(0.0f, 1.0f)] public float normalVolume = 0.6f;
    [SerializeField] [Range(0.0f, 1.0f)] public float pausedVolume = 0.2f;

    AudioSource playingSource;

    Coroutine currentVolumeTransitionRoutine;

    private void Awake() {
        enterPauseEvent.AddListener(EnterMutedStateOnEvent);
        exitPauseEvent.AddListener(ExitMutedStateOnEvent);
    }

    private void Start() {
        SetupMusicSystem();
        StartCoroutine(RunMusicSystem());
    }

    void SetupMusicSystem()
    {
        trackContainer.GenerateTrackPath();
        SetupPlayersVolume();
        playingSource = musicPlayer1;
        playingSource.clip = trackContainer.tracks[trackContainer.trackPath[currentTrackIndex]];
    }

    IEnumerator RunMusicSystem()
    {
        while (running)
        {
            playingSource.Play();

            while (playingSource.clip.length - playingSource.time > 5) yield return new WaitForSecondsRealtime(1);

            SwapPlayingSource();
            playingSource.clip = GetNextTrack();
        }
        yield return null;
    }

    void SwapPlayingSource()
    {
        if (playingSource.GetInstanceID() == musicPlayer1.GetInstanceID()) playingSource = musicPlayer2;
        else playingSource = musicPlayer1;
    }

    AudioClip GetNextTrack()
    {
        currentTrackIndex ++;
        if (currentTrackIndex >= trackContainer.trackPath.Count - 1) 
        {
            trackContainer.GenerateTrackPath();
            currentTrackIndex = 0;
        }
        return trackContainer.tracks[trackContainer.trackPath[currentTrackIndex]];
    }

    // Todo: Implement playerprefs loading/saving.
    void SetupPlayersVolume()
    {
        musicPlayer1.volume = normalVolume;
        musicPlayer2.volume = normalVolume;
    }


    void EnterMutedStateOnEvent()
    {
        if (currentVolumeTransitionRoutine != null) StopCoroutine(currentVolumeTransitionRoutine);
        currentVolumeTransitionRoutine = StartCoroutine(EnterMutedState());
    }

    IEnumerator EnterMutedState()
    {
        float rateOfChange =  (normalVolume - pausedVolume) * Time.fixedDeltaTime / transitionLength;

        while (playingSource.volume > pausedVolume)
        {
            playingSource.volume -= rateOfChange;
            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime); 
        }
        yield return null;
    }


    void ExitMutedStateOnEvent()
    {
        if (currentVolumeTransitionRoutine != null) StopCoroutine(currentVolumeTransitionRoutine);
        currentVolumeTransitionRoutine = StartCoroutine(ExitMutedState());
    }

    IEnumerator ExitMutedState()
    {
        float rateOfChange =  (pausedVolume - normalVolume) * Time.fixedDeltaTime / transitionLength * 1.01f;

        while (playingSource.volume < normalVolume)
        {
            playingSource.volume -= rateOfChange;
            yield return new WaitForFixedUpdate(); 
        }
        yield return null;
    }
}
