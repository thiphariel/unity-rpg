using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public new Text[] name;
    public Text[] hp, mp, level, exp;
    public Slider[] expSlider;
    public Image[] image;
    public GameObject[] holder;

    private CharacterStats[] characterStats;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            bool active = menu.activeInHierarchy;
            menu.SetActive(!active);

            if (!active)
            {
                UpdateStats();
            }

            GameManager.instance.isMenuOpen = !active;
        }
    }

    public void UpdateStats()
    {
        characterStats = GameManager.instance.charactersStats;

        for (int i = 0; i < characterStats.Length; i++)
        {
            CharacterStats characterStat = characterStats[i];
            bool active = characterStat.gameObject.activeInHierarchy;

            // Active / Desactive holder depends on the character is active or not
            holder[i].SetActive(active);

            if (active)
            {
                name[i].text = characterStat.name;
                hp[i].text = "HP : " + characterStat.hp + " / " + characterStat.maxHp;
                mp[i].text = "MP : " + characterStat.mp + " / " + characterStat.maxMp;
                level[i].text = "Level : " + characterStat.level;
                exp[i].text = characterStat.exp + " / " + characterStat.exps[characterStat.level];
                expSlider[i].maxValue = characterStat.exps[characterStat.level];
                expSlider[i].value = characterStat.exp;
                image[i].sprite = characterStat.sprite;
            }
        }
    }
}
