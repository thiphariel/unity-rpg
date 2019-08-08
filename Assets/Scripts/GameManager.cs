using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharacterStats[] charactersStats;
    public bool isMenuOpen;
    public bool isDialogOpen;
    public bool isLoading;
    public bool isShopping;
    public bool isBattle;

    public string[] itemsOwned;
    public int[] itemsCount;
    public Item[] items;

    public int gils;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);

        SortItems();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMenuOpen || isDialogOpen || isLoading || isShopping || isBattle)
        {
            PlayerController.instance.canMove = false;
        } else
        {
            PlayerController.instance.canMove = true;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            AddItem("Heavy Armor");
            AddItem("Potion");
            AddItem("Blabla");

            RemoveItem("Ether");
            RemoveItem("Ahah");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            SaveData();
        }
    }

    public Item GetItem(string item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].name == item)
            {
                return items[i];
            }
        }

        return null;
    }

    public void SortItems()
    {
        bool isNextNotEmpty = true;

        while(isNextNotEmpty)
        {
            isNextNotEmpty = false;

            for (int i = 0; i < itemsOwned.Length - 1; i++)
            {
                if (itemsOwned[i] == "")
                {
                    itemsOwned[i] = itemsOwned[i + 1];
                    itemsOwned[i + 1] = "";

                    itemsCount[i] = itemsCount[i + 1];
                    itemsCount[i + 1] = 0;

                    if (itemsOwned[i] != "")
                    {
                        isNextNotEmpty = true;
                    }
                }
            }
        }
    }

    public void AddItem(string item)
    {
        int position = 0;
        bool foundSpace = false;

        for (int i = 0; i < itemsOwned.Length; i++)
        {
            if (itemsOwned[i] == "" || itemsOwned[i] == item)
            {
                position = i;
                foundSpace = true;
                i = itemsOwned.Length;
            }
        }

        if (foundSpace)
        {
            bool exists = false;

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].name == item)
                {
                    exists = true;
                    i = items.Length;
                }
            }

            if (exists)
            {
                itemsOwned[position] = item;
                itemsCount[position]++;

                Menu.instance.ShowItems();
            } else
            {
                Debug.LogError("Hmmm, nop, that object not exists : " + item);
            }
        }
    }

    public void RemoveItem(string item)
    {
        int position = 0;
        bool found = false;

        for (int i = 0; i < itemsOwned.Length; i++)
        {
            if (itemsOwned[i] == "" || itemsOwned[i] == item)
            {
                position = i;
                found = true;
                i = itemsOwned.Length;
            }
        }

        if (found)
        {
            itemsCount[position]--;

            if (itemsCount[position] <= 0)
            {
                itemsOwned[position] = "";
            }

            Menu.instance.ShowItems();
        } else
        {
            Debug.LogError("Hmmm, nop, could'nt find : " + item);
        }
    }

    public void SaveData()
    {
        Vector3 postiion = PlayerController.instance.transform.position;

        PlayerPrefs.SetString("Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_x", postiion.x);
        PlayerPrefs.SetFloat("Player_y", postiion.y);
        PlayerPrefs.SetFloat("Player_z", postiion.z);

        // Save Players stats
        for (int i = 0; i < charactersStats.Length; i++)
        {
            CharacterStats stats = charactersStats[i];

            Debug.Log("Char " + stats.name + " is active ? " + stats.gameObject.activeInHierarchy);

            PlayerPrefs.SetInt("Player_" + stats.name + "_active", stats.gameObject.activeInHierarchy ? 1 : 0);

            PlayerPrefs.SetInt("Player_" + stats.name + "_exp", stats.exp);
            PlayerPrefs.SetInt("Player_" + stats.name + "_level", stats.level);

            PlayerPrefs.SetInt("Player_" + stats.name + "_hp", stats.hp);
            PlayerPrefs.SetInt("Player_" + stats.name + "_maxHp", stats.maxHp);
            PlayerPrefs.SetInt("Player_" + stats.name + "_mp", stats.mp);
            PlayerPrefs.SetInt("Player_" + stats.name + "_maxMp", stats.maxMp);

            PlayerPrefs.SetInt("Player_" + stats.name + "_strength", stats.strength);
            PlayerPrefs.SetInt("Player_" + stats.name + "_constitution", stats.constitution);

            PlayerPrefs.SetInt("Player_" + stats.name + "_weaponPower", stats.weaponPower);
            PlayerPrefs.SetInt("Player_" + stats.name + "_armorPower", stats.armorPower);
            PlayerPrefs.SetString("Player_" + stats.name + "_weapon", stats.weapon);
            PlayerPrefs.SetString("Player_" + stats.name + "_armor", stats.armor);
        }

        // Save inventory
        for (int i = 0; i < itemsOwned.Length; i++)
        {
            PlayerPrefs.SetString("ItemOwned_" + i, itemsOwned[i]);
            PlayerPrefs.SetInt("ItemCount_" + i, itemsCount[i]);
        }
    }

    public void LoadData()
    {
        float x = PlayerPrefs.GetFloat("Player_x");
        float y = PlayerPrefs.GetFloat("Player_y");
        float z = PlayerPrefs.GetFloat("Player_z");

        PlayerController.instance.transform.position = new Vector3(x, y, z);

        // Load Players stats
        for (int i = 0; i < charactersStats.Length; i++)
        {
            CharacterStats stats = charactersStats[i];

            stats.gameObject.SetActive(PlayerPrefs.GetInt("Player_" + stats.name + "_active") == 1 ? true : false);

            stats.exp = PlayerPrefs.GetInt("Player_" + stats.name + "_exp");
            stats.level = PlayerPrefs.GetInt("Player_" + stats.name + "_level");

            stats.hp = PlayerPrefs.GetInt("Player_" + stats.name + "_hp");
            stats.maxHp = PlayerPrefs.GetInt("Player_" + stats.name + "_maxHp");
            stats.mp = PlayerPrefs.GetInt("Player_" + stats.name + "_mp");
            stats.maxMp = PlayerPrefs.GetInt("Player_" + stats.name + "_maxMp");

            stats.strength = PlayerPrefs.GetInt("Player_" + stats.name + "_strength");
            stats.constitution = PlayerPrefs.GetInt("Player_" + stats.name + "_constitution");

            stats.weaponPower = PlayerPrefs.GetInt("Player_" + stats.name + "_weaponPower");
            stats.armorPower = PlayerPrefs.GetInt("Player_" + stats.name + "_armorPower");
            stats.weapon = PlayerPrefs.GetString("Player_" + stats.name + "_weapon");
            stats.armor = PlayerPrefs.GetString("Player_" + stats.name + "_armor");
        }

        // Load inventory
        for (int i = 0; i < itemsOwned.Length; i++)
        {
            itemsOwned[i] = PlayerPrefs.GetString("ItemOwned_" + i);
            itemsCount[i] = PlayerPrefs.GetInt("ItemCount_" + i);
        }
    }
}
