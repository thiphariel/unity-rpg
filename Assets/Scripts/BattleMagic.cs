using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMagic : MonoBehaviour
{
    public string spell;
    public int mp;
    public Text nameText;
    public Text mpText;

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
        if (BattleManager.instance.activeInBattle[BattleManager.instance.turn].mp >= mp)
        {
            BattleManager.instance.magicMenu.SetActive(false);
            BattleManager.instance.OpenTargetMenu(spell);
            BattleManager.instance.activeInBattle[BattleManager.instance.turn].mp -= mp;
        } else
        {

        }
    }
}
