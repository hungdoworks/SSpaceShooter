using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private AudioSource audioPlayer;
    public AudioClip startSound;

    public GameObject startGameText;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || TouchAnyWhere())
        {
            InvokeRepeating("PlayStartGameAnim", 0, 0.3f);

            audioPlayer.PlayOneShot(startSound);

            StartCoroutine("GameStart");
        }
    }

    bool TouchAnyWhere()
    {
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
    }

    public IEnumerator GameStart()
    {
        yield return new WaitForSeconds(startSound.length);

        SceneManager.LoadScene("MainScene");
    }

    public void PlayStartGameAnim()
    {
        startGameText.SetActive(!startGameText.activeSelf);
    }
}
