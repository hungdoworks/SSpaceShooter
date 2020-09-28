using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject player;
    public GameObject obstacle;
    public List<GameObject> enemies;

    private Vector3 lastPlayerPos;

    private float startDelay = 1;
    private float repeatRate = 1;
    private float maxSpawnPos = 40;
    private float startPos = 20;

    private int obstacleNum = 80;
    private int enemyNum = 10;

    // Start is called before the first frame update
    void Start()
    {
        lastPlayerPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateObstacle()
    {
        for (int i = 0; i < obstacleNum; i++)
        {
            Vector2 randPos = RandomPosition();
            Vector3 spawnPos = new Vector3(randPos.x, randPos.y, startPos);

            Instantiate(obstacle, spawnPos, Quaternion.Euler(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180)));
        }
    }

    void CreateEnemy()
    {
        for (int i = 0; i < enemyNum; i++)
        {
            Vector2 randPos = RandomPosition();
            Vector3 spawnPos = new Vector3(randPos.x, randPos.y, startPos);

            int typeNum = Random.Range(0, enemies.Count);
            Instantiate(enemies[typeNum], spawnPos, enemies[typeNum].transform.rotation);
        }
    }

    Vector2 RandomPosition()
    {
        float baseSpawnPosX = player != null ? player.gameObject.transform.position.x : lastPlayerPos.x;
        float baseSpawnPosY = player != null ? player.gameObject.transform.position.y : lastPlayerPos.y;
        float randX = Random.Range(baseSpawnPosX - maxSpawnPos, baseSpawnPosX + maxSpawnPos);
        float randY = Random.Range(baseSpawnPosY - maxSpawnPos, baseSpawnPosY + maxSpawnPos);

        return new Vector2(randX, randY);
    }

    public void StartSpawn()
    {
        InvokeRepeating("CreateObstacle", startDelay, repeatRate);
        InvokeRepeating("CreateEnemy", startDelay + 4, repeatRate + 2);
    }

    public void StopSpawnEnemy()
    {
        CancelInvoke("CreateEnemy");
    }

    public void StopSpawnObstacle()
    {
        CancelInvoke("CreateObstacle");
    }

    public void SetLastPlayerPosition(Vector3 pos)
    {
        lastPlayerPos = pos;
    }
}
