using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    private bool fadeIn;
    private bool fadeOut;

    public static FadeScreen instance;
    public Image fade;
    public float fadeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, Mathf.MoveTowards(fade.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fade.color.a >= 1f)
            {
                fadeIn = false;
            }
        }

        if (fadeOut)
        {
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, Mathf.MoveTowards(fade.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fade.color.a <= 0f)
            {
                fadeOut = false;
            }
        }
    }

    public void FadeIn()
    {
        fadeIn = true;
        fadeOut = false;
    }

    public void FadeOut()
    {
        fadeOut = true;
        fadeIn = false;
    }
}
