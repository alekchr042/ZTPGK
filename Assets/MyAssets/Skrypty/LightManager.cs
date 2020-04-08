using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Light mainLight;

    private GameObject playerFlashlight;
    // Start is called before the first frame update
    void Start()
    {
        var playerCharacter = GameObject.FindGameObjectWithTag("Player");
        playerCharacter.GetComponentsInChildren(typeof(Transform), true).ToList().ForEach(x =>
        {
            var flashlight = x.gameObject.transform.Find("Flashlight");
            if(flashlight != null)
            {
                playerFlashlight = flashlight.gameObject;
            }
        });
        Debug.Log(playerFlashlight == null);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            mainLight.gameObject.SetActive(!mainLight.gameObject.activeSelf);
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            playerFlashlight.SetActive(!playerFlashlight.activeSelf);
        }
    }
}
