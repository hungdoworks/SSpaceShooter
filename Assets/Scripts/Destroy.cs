using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public ParticleSystem explosion = null;

    private int startPos = 20;
    private GameManager gameManager;
    private SpawnManager spawnManager;
    private SFXController sfxController;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        sfxController = GameObject.Find("SFX").GetComponent<SFXController>();
    }

    // Update is called once per frame
    void Update()
    {
        DestroyIfOutOfBound();
    }

    private void DestroyIfOutOfBound()
    {
        if (transform.position.z < -startPos || transform.position.z > startPos)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Enemy") && collision.gameObject.CompareTag("Bullet"))
        {
            gameManager.UpdateScore(10);

            sfxController.PlayEnemyKilled();
        }
        else if (gameObject.CompareTag("Player"))
        {
            spawnManager.SetLastPlayerPosition(gameObject.transform.position);
            gameManager.SetGameOver(true);
        }

        PlayDestroy();
    }

    public void PlayDestroy()
    {
        explosion.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        Instantiate(explosion, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
