using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomCollision : MonoBehaviour
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
        var isCollisionWithPlayer = other.gameObject.tag == "Player";
        if (isCollisionWithPlayer)
        {
            Debug.Log("collision with player");
            var currentResult = PlayerPrefs.GetInt("PlayerResult", 0);
            PlayerPrefs.SetInt("PlayerResult", ++currentResult);

            Destroy(gameObject);
        }
    }
}
