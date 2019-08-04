using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item type")]
	public bool isItem;
	public bool isWeapon;
	public bool isArmor;

    [Header("Item details")]
	public string name;
	public string description;
	public int price;
	public Sprite sprite;

    [Header("Effects")]
	public int amountAffect;
	public bool affectHp, affectMp, affectStrength, affectConsitution;

    [Header("Weapon / Armor")]
	public int weaponPower;
	public int armorPower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use(int character)
    {
        CharacterStats stats = GameManager.instance.charactersStats[character];

        if (isItem)
        {
            if (affectHp)
            {
                stats.hp += amountAffect;

                if (stats.hp > stats.maxHp)
                {
                    stats.hp = stats.maxHp;
                }

                Debug.Log(stats);
            }

            if (affectMp)
            {
                stats.mp += amountAffect;

                if (stats.mp > stats.maxMp)
                {
                    stats.mp = stats.maxMp;
                }
            }

            if (affectStrength)
            {
                stats.strength += amountAffect;
            }

            if (affectConsitution)
            {
                stats.constitution += amountAffect;
            }
        }

        if (isWeapon)
        {
            if (stats.weapon != "")
            {
                GameManager.instance.AddItem(stats.weapon);
            }

            stats.weapon = name;
            stats.weaponPower = weaponPower;
        }

        if (isArmor)
        {
            if (stats.armor != "")
            {
                GameManager.instance.AddItem(stats.armor);
            }

            stats.armor = name;
            stats.armorPower = armorPower;
        }

        GameManager.instance.RemoveItem(name);
    }
}
