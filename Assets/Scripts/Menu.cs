using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject[] windows;
    public GameObject[] statsButtons;

    public new Text[] name;
    public Text[] hp, mp, level, exp;
    public Slider[] expSlider;
    public Image[] image;
    public GameObject[] holder;

    public Text statusName, statusHp, statusMp, statusStrength, statusConstitution, statusWeapon, statusWeaponPower, statusArmor, statusArmorPower, statusExp;
    public Image statusImage;

    public MenuItem[] items;
    public string selected;
    public Item active;
    public Text selectedName, selectedDescription, use;

    public static Menu instance;

    private CharacterStats[] characterStats;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            bool activeInHierarchy = menu.activeInHierarchy;
            
            if (!activeInHierarchy)
            {
                menu.SetActive(!activeInHierarchy);
                UpdateStats();
                GameManager.instance.isMenuOpen = !activeInHierarchy;
            } else
            {
                CloseMenu();
            }
        }
    }

    public void UpdateStats()
    {
        characterStats = GameManager.instance.charactersStats;

        for (int i = 0; i < characterStats.Length; i++)
        {
            CharacterStats characterStat = characterStats[i];
            bool activeInHierarchy = characterStat.gameObject.activeInHierarchy;

            // Active / Desactive holder depends on the character is active or not
            holder[i].SetActive(activeInHierarchy);

            if (activeInHierarchy)
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

    public void ToggleWindow(int position)
    {
        UpdateStats();

        for (int i = 0; i < windows.Length; i++)
        {
            GameObject window = windows[i];

            if (i == position)
            {
                window.SetActive(!window.activeInHierarchy);
            } else
            {
                window.SetActive(false);
            }
        }
    }

    public void CloseMenu()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }

        menu.SetActive(false);
        GameManager.instance.isMenuOpen = false;
    }

    public void OpenStats()
    {
        UpdateStats();

        // Update first char by default
        UpdateCharacterStats(0);

        for (int i = 0; i < statsButtons.Length; i++)
        {
            statsButtons[i].SetActive(characterStats[i].gameObject.activeInHierarchy);
            statsButtons[i].GetComponentInChildren<Text>().text = characterStats[i].name;
        }
    }

    public void UpdateCharacterStats(int position)
    {
        CharacterStats character = characterStats[position];

        statusName.text = character.name;
        statusHp.text = character.hp + " / " + character.maxHp;
        statusMp.text = character.mp + " / " + character.maxMp;
        statusStrength.text = character.strength.ToString();
        statusConstitution.text = character.constitution.ToString();

        if (character.weapon != "")
        {
            statusWeapon.text = character.weapon;
        } else
        {
            statusWeapon.text = "None";
        }

        statusWeaponPower.text = character.weaponPower.ToString();

        if (character.armor != "")
        {
            statusArmor.text = character.armor;
        }
        else
        {
            statusArmor.text = "None";
        }

        statusArmorPower.text = character.armorPower.ToString();
        statusExp.text = (character.exps[character.level] - character.exp).ToString();

        statusImage.sprite = character.sprite;
    }

    public void ShowItems()
    {
        GameManager.instance.SortItems();

        for (int i = 0; i < items.Length; i++)
        {
            items[i].position = i;

            if (GameManager.instance.itemsOwned[i] != "")
            {
                items[i].image.gameObject.SetActive(true);
                items[i].image.sprite = GameManager.instance.GetItem(GameManager.instance.itemsOwned[i]).sprite;

                if (GameManager.instance.itemsCount[i] > 1)
                {
                    items[i].amount.text = GameManager.instance.itemsCount[i].ToString();
                } else
                {
                    items[i].amount.text = "";
                }
                
            } else
            {
                items[i].image.gameObject.SetActive(false);
                items[i].amount.text = "";
            }
        }
    }

    public void SelectItem(Item item)
    {
        active = item;

        if (item.isItem)
        {
            use.text = "Use";
        } else if (item.isWeapon || item.isArmor)
        {
            use.text = "Equip";
        }

        selectedName.text = item.name;
        selectedDescription.text = item.description;
    }
}
