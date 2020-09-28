using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    private AudioSource audioPlayer;
    public AudioClip background;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        audioPlayer.PlayOneShot(background);
        audioPlayer.loop = true;
    }

    public void Pause()
    {
        audioPlayer.Pause();
    }

    public void UnPause()
    {
        audioPlayer.UnPause();
    }
}
