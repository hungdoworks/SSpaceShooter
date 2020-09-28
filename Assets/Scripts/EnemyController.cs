using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");

        MoveForward moveForwardComponent = GetComponent<MoveForward>();
        moveForwardComponent.SetDirection(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
