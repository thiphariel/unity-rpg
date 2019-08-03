using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public new string name;
    public int level = 1;
    public int exp;
    public int[] exps;
    public int maxLevel = 100;

    public int hp;
    public int maxHp = 100;
    public int mp;
    public int maxMp = 30;

    public int strength;
    public int constitution;

    public int weaponPower;
    public int armorPower;

    public string weapon;
    public string armor;

    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        exps = new int[maxLevel];
        exps[1] = 2;

        for (int i = 2; i < exps.Length; i++)
        {
            float multiplier = 1 + (.5f / ((i / 5) + 1));
            exps[i] = Mathf.FloorToInt(exps[i - 1] * multiplier) + i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            AddExp(50);
        }
    }

    public void AddExp(int exp)
    {
        this.exp += exp;

        if (level < maxLevel)
        {
            if (this.exp > exps[level])
            {
                this.exp -= exps[level];
                level++;

                // Add some stats
                if (level % 2 == 0)
                {
                    strength++;
                }
                else
                {
                    constitution++;
                }

                // Add HP & MP
                maxHp = Mathf.FloorToInt(maxHp * 1.08f);
                maxMp = Mathf.FloorToInt(maxMp * 1.06f);
                hp = maxHp;
                mp = maxMp;
            }
        }

        // Max level
        if (level >= maxLevel)
        {
            this.exp = 0;
        }
    }
}
