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
}
