using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public GameObject menuInGame;
    public GameObject menuGameOver;
    public GameObject menuPauseGame;

    private PlayerController playerController;
    private SpawnManager spawnManager;
    private BGMController bgmController;
    private SFXController sfxController;

    private int score = 0;
    private bool isGameOver = false;
    private bool isGameStarted = false;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        bgmController = GameObject.Find("BGM").GetComponent<BGMController>();
        sfxController = GameObject.Find("SFX").GetComponent<SFXController>();

        UpdateScore(0);

        if (isGameStarted == false)
        {
            isGameStarted = true;

            StartGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInputDebug();

        UpdateInputAndroid();

        if (isGameOver)
        {
            GameOver();
        }
    }

    private void UpdateInputDebug()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGameOver)
            {
                RestartGame();
            }
            else
            {
                PauseResumeGame();
            }
        }
    }

    private void UpdateInputAndroid()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (isGameOver)
            {
                if (TouchAnyWhere())
                    RestartGame();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                    PauseResumeGame();
            }
        }
    }

    public void UpdateScore(int score)
    {
        this.score += score;
        scoreText.text = "SCORE " + this.score;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public bool IsGameStarted()
    {
        return isGameStarted;
    }

    public void SetGameOver(bool isOver)
    {
        isGameOver = isOver;
    }

    public bool IsGamePaused()
    {
        return isPaused;
    }

    private void StartGame()
    {
        spawnManager.StartSpawn();

        bgmController.Play();
    }

    private void GameOver()
    {
        if (menuGameOver.activeInHierarchy == false)
        {
            menuGameOver.SetActive(true);

            spawnManager.StopSpawnEnemy();

            bgmController.Pause();
            sfxController.PlayPlayerDeath();
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private bool TouchAnyWhere()
    {
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
    }

    private void PauseResumeGame()
    {
        isPaused = !isPaused;

        menuPauseGame.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0;
            bgmController.Pause();
            sfxController.PlayPauseGame();
        }
        else
        {
            Time.timeScale = 1;
            bgmController.UnPause();
        }
    }
}
