using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public SceneField next;
    public string transition;
    public Entrance entrance;
    public float loadingTime = 1f;

    private bool shouldLoad;

    // Start is called before the first frame update
    void Start()
    {
        entrance.transition = transition;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldLoad)
        {
            loadingTime -= Time.deltaTime;

            if (loadingTime <= 0f)
            {
                shouldLoad = false;
                SceneManager.LoadScene(next);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            shouldLoad = true;
            GameManager.instance.isLoading = true;

            FadeScreen.instance.FadeIn();

            PlayerController.instance.transition = transition;
        }
    }
}
