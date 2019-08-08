using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    private bool canShop;

    public string[] sellItems = new string[40];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canShop && Input.GetButtonDown("Fire1") && PlayerController.instance.canMove && !Shop.instance.menu.activeInHierarchy)
        {
            Shop.instance.sellItems = sellItems;

            Shop.instance.OpenShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canShop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canShop = false;
        }
    }
}
