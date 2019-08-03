using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public string transition;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.instance && transition == PlayerController.instance.transition)
        {
            PlayerController.instance.transform.position = transform.position;
        }

        FadeScreen.instance.FadeOut();

        GameManager.instance.isLoading = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
