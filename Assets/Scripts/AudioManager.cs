using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioSource audioSource;

    [SerializeField]
    private List<AudioClip> musicClips;
    [SerializeField]
    private List<AudioClip> congratulationClips;
    [SerializeField]
    private List<AudioClip> restartClips;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = 0.5f;
        PlayNewMusic();
    }

    public void PlayNewMusic()
    {
        if(musicClips.Count != 0)
        {
            var clipToPlay = musicClips[Random.Range(0, musicClips.Count - 1)];

            audioSource.Stop();
            audioSource.clip = clipToPlay;
            audioSource.Play();
        }
    }

    public void PlayCongratsSound()
    {
        if (congratulationClips.Count != 0)
        {
            var clipToPlay = congratulationClips[Random.Range(0, congratulationClips.Count - 1)];
            audioSource.PlayOneShot(clipToPlay, 1.0f);
        }
    }

    public void PlayRestartSound()
    {
        if (restartClips.Count != 0)
        {
            var clipToPlay = restartClips[Random.Range(0, restartClips.Count - 1)];
            audioSource.PlayOneShot(clipToPlay, 1.0f);
        }
    }
}
