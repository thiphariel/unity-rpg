using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    public Image image;
    public Text amount;
    public int position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        if (GameManager.instance.itemsOwned[position] != "")
        {
            Menu.instance.SelectItem(GameManager.instance.GetItem(GameManager.instance.itemsOwned[position]));
        }
    }
}
