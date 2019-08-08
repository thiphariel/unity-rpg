using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    public GameObject menu;
    public GameObject buy;
    public GameObject sell;
    public string[] sellItems;
    public Text gils;

    public MenuItem[] buyMenuItems;
    public MenuItem[] sellMenuItems;

    public Item active;
    public Text buyName, buyDescription, buyValue;
    public Text sellName, sellDescription, sellValue;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !menu.activeInHierarchy)
        {
            OpenShop();
        }
    }

    public void OpenShop()
    {
        menu.SetActive(true);
        OpenBuy();

        GameManager.instance.isShopping = true;

        gils.text = GameManager.instance.gils + "g";
    }

    public void CloseShop()
    {
        menu.SetActive(false);

        GameManager.instance.isShopping = false;
    }

    public void OpenBuy()
    {
        buyMenuItems[0].Click();

        buy.SetActive(true);
        sell.SetActive(false);

        for (int i = 0; i < buyMenuItems.Length; i++)
        {
            buyMenuItems[i].position = i;

            if (sellItems[i] != "")
            {
                buyMenuItems[i].image.gameObject.SetActive(true);
                buyMenuItems[i].image.sprite = GameManager.instance.GetItem(sellItems[i]).sprite;
                buyMenuItems[i].amount.text = "";
            }
            else
            {
                buyMenuItems[i].image.gameObject.SetActive(false);
                buyMenuItems[i].amount.text = "";
            }
        }
    }

    public void OpenSell()
    {
        sellMenuItems[0].Click();

        buy.SetActive(false);
        sell.SetActive(true);

        ShowSellItems();
    }

    private void ShowSellItems()
    {
        GameManager.instance.SortItems();

        for (int i = 0; i < sellMenuItems.Length; i++)
        {
            sellMenuItems[i].position = i;

            if (GameManager.instance.itemsOwned[i] != "")
            {
                sellMenuItems[i].image.gameObject.SetActive(true);
                sellMenuItems[i].image.sprite = GameManager.instance.GetItem(GameManager.instance.itemsOwned[i]).sprite;
                sellMenuItems[i].amount.text = GameManager.instance.itemsCount[i].ToString();
            }
            else
            {
                sellMenuItems[i].image.gameObject.SetActive(false);
                sellMenuItems[i].amount.text = "";
            }
        }
    }

    public void SelectBuyItem(Item item)
    {
        active = item;

        buyName.text = item.name;
        buyDescription.text = item.description;
        buyValue.text = "Value : " + item.price + "g";
    }

    public void SelectSellItem(Item item)
    {
        active = item;

        sellName.text = item.name;
        sellDescription.text = item.description;
        sellValue.text = "Value : " + Mathf.FloorToInt(item.price * .7f) + "g";
    }

    public void Buy()
    {
        if (GameManager.instance.gils >= active.price)
        {
            GameManager.instance.gils -= active.price;
            GameManager.instance.AddItem(active.name);
        }

        gils.text = GameManager.instance.gils + "g";
    }

    public void Sell()
    {
        if (active != null)
        {
            GameManager.instance.gils += Mathf.FloorToInt(active.price * .7f);
            GameManager.instance.RemoveItem(active.name);
        }

        gils.text = GameManager.instance.gils + "g";

        ShowSellItems();
    }
}
