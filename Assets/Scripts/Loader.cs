using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject fadeScreen;
    public GameObject player;
    public GameObject manager;
    public GameObject audioManager;

    // Start is called before the first frame update
    void Start()
    {
        if (!FadeScreen.instance)
        {
            FadeScreen.instance = Instantiate(fadeScreen).GetComponent<FadeScreen>();
        }

        if (!PlayerController.instance)
        {
            PlayerController.instance = Instantiate(player).GetComponent<PlayerController>();
        }

        if (!GameManager.instance)
        {
            GameManager.instance = Instantiate(manager).GetComponent<GameManager>();
        }

        if (!AudioManager.instance)
        {
            AudioManager.instance = Instantiate(audioManager).GetComponent<AudioManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
