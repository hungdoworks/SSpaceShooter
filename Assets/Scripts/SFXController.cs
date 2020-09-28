using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    private AudioSource audioPlayer;

    public AudioClip sfxPlayerDeath;
    public AudioClip sfxPauseGame;
    public AudioClip sfxEnemyKilled;
    
    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPlayerDeath()
    {
        audioPlayer.PlayOneShot(sfxPlayerDeath);
    }

    public void PlayPauseGame()
    {
        audioPlayer.PlayOneShot(sfxPauseGame);
    }

    public void PlayEnemyKilled()
    {
        audioPlayer.PlayOneShot(sfxEnemyKilled);
    }
}
