using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        var rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(rb.transform.up * 15, ForceMode.Impulse);
        rb.AddForce(rb.transform.forward * 15, ForceMode.Impulse);
    }
}
