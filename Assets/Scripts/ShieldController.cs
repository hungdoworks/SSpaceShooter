using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Do nothing
        }
        else
        {
            Destroy destroyComponent = other.gameObject.GetComponent<Destroy>();
            if (destroyComponent != null)
            {
                destroyComponent.PlayDestroy();
            }
        }
    }
}
