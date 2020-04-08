using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        var isCollisionWithPlayer = other.gameObject.tag == "Player";
        if (isCollisionWithPlayer)
        {
            Debug.Log("collision with player");
            var rb = other.gameObject.GetComponent<Rigidbody>() ;
            rb.AddForce(rb.gameObject.transform.forward * -50.0f, ForceMode.Impulse);
            
        }
    }
}
