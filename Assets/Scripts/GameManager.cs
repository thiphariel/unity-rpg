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

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
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
}
