using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string scene;
    public string loadScene;
    public GameObject load;

    // Start is called before the first frame update
    void Start()
    {
        load.SetActive(PlayerPrefs.HasKey("Scene"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Load()
    {
        SceneManager.LoadScene(loadScene);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
