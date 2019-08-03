using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject fadeScreen;
    public GameObject player;
    public GameObject manager;

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
            Instantiate(manager);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
