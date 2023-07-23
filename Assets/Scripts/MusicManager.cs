using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MusicTrack
{
    public string trackName;
    public AudioClip audioClip;
}

public class MusicManager : MonoBehaviour
{
    public List<MusicTrack> musicTracks;
    private AudioSource audioSource;
    private int currentTrackIndex;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentTrackIndex = 0;
    }

    private void Start()
    {
        audioSource.clip = musicTracks[currentTrackIndex].audioClip;
        audioSource.Play();
    }

    public void PlayTrack(int trackIndex)
    {
        currentTrackIndex = trackIndex;
        audioSource.Stop();
        audioSource.clip = musicTracks[currentTrackIndex].audioClip;
        audioSource.Play();
    }

    public void PlayerDeathMusic()
    {
        PlayTrack(1);
    }

    public void GameCompleteMusic()
    {
        PlayTrack(2);
    }

    public void UndergroundMusic()
    {
        PlayTrack(3);
    }
}
