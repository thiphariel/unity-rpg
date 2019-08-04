using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharacterStats[] charactersStats;
    public bool isMenuOpen;
    public bool isDialogOpen;
    public bool isLoading;

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
        if (isMenuOpen || isDialogOpen || isLoading)
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
}
