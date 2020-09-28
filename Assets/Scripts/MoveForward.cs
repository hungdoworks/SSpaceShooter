using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private Vector3 direction;

    private float speed = 10;
    private float bulletSpeed = 20;

    private bool fireFromEnemy = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Obstacle"))
        {
            Vector3 move = Vector3.forward * Time.deltaTime * -speed;

            transform.position += move;
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }
        else if (gameObject.CompareTag("Bullet"))
        {
            Vector3 move = Vector3.forward * Time.deltaTime * bulletSpeed * (fireFromEnemy ? -1 : 1);

            transform.position += move;
        }
    }

    public void SetFireFromEnenmy(bool fireFromEnemy)
    {
        this.fireFromEnemy = fireFromEnemy;
    }

    public void SetDirection(Vector3 point)
    {
        direction = point - transform.position;
        direction.Normalize();
    }
}
