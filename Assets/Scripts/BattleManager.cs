using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public bool isBattle;
    public GameObject scene;
    public Transform[] playerPositions;
    public Transform[] enemyPositions;

    public BattleChar[] players;
    public BattleChar[] enemies;
    public List<BattleChar> activeInBattle = new List<BattleChar>();

    public int turn;
    public bool isTurnWaiting;

    public GameObject uiActions;

    public BattleAbility[] abilities;
    public GameObject enemyAttackEffect;

    public DamageDisplay damageDisplay;

    public Text[] names, hps, mps;

    public GameObject targetMenu;
    public BattleTarget[] targets;

    public GameObject magicMenu;
    public BattleMagic[] magics;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            BattleStart(new string[] { "Eyeball", "Spider", "Skeleton" });
        }

        if (isBattle)
        {
            if (isTurnWaiting)
            {
                if (activeInBattle[turn].isPlayer)
                {
                    uiActions.SetActive(true);
                }
                else
                {
                    uiActions.SetActive(false);

                    // Enemy attack !
                    StartCoroutine(EnemyMoveCoroutine());
                }
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                NextTurn();
            }
        }
    }

    public void BattleStart(string[] enemies)
    {
        if (!isBattle)
        {
            isBattle = true;
            GameManager.instance.isBattle = true;

            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);

            scene.SetActive(true);

            AudioManager.instance.PlayMusic(0);

            for (int i = 0; i < playerPositions.Length; i++)
            {
                if (GameManager.instance.charactersStats[i].gameObject.activeInHierarchy)
                {
                    for (int j = 0; j < players.Length; j++)
                    {
                        if (players[j].name == GameManager.instance.charactersStats[i].name)
                        {
                            BattleChar player = Instantiate(players[j], playerPositions[i].position, playerPositions[i].rotation);
                            player.transform.parent = playerPositions[i];

                            // Add stats
                            CharacterStats stats = GameManager.instance.charactersStats[i];
                            player.hp = stats.hp;
                            player.maxHp = stats.maxHp;
                            player.mp = stats.mp;
                            player.maxMp = stats.maxMp;
                            player.strength = stats.strength;
                            player.constitution = stats.constitution;
                            player.weaponPower = stats.weaponPower;
                            player.armorPower = stats.armorPower;

                            activeInBattle.Add(player);
                        }
                    }
                }
            }

            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != "")
                {
                    for (int j = 0; j < this.enemies.Length; j++)
                    {
                        if (this.enemies[j].name == enemies[i])
                        {
                            BattleChar enemy = Instantiate(this.enemies[j], enemyPositions[i].position, enemyPositions[i].rotation);
                            enemy.transform.parent = enemyPositions[i];
                            activeInBattle.Add(enemy);
                        }
                    }
                }
            }

            isTurnWaiting = true;
            turn = Random.Range(0, activeInBattle.Count);

            UpdateUiStats();
        }
    }

    public void NextTurn()
    {
        turn++;

        if (turn >= activeInBattle.Count)
        {
            turn = 0;
        }

        isTurnWaiting = true;

        UpdateBattle();
        UpdateUiStats();
    }

    public void UpdateBattle()
    {
        bool isEnemiesDead = true;
        bool isPlayersDead = true;

        for (int i = 0; i < activeInBattle.Count; i++)
        {
            if (activeInBattle[i].hp < 0)
            {
                activeInBattle[i].hp = 0;
            }

            if (activeInBattle[i].hp == 0)
            {
            }
            else
            {
                if (activeInBattle[i].isPlayer)
                {
                    isPlayersDead = false;
                }
                else
                {
                    isEnemiesDead = false;
                }
            }
        }

        if (isEnemiesDead || isPlayersDead)
        {
            if (isEnemiesDead)
            {
                // Victory
            }
            else
            {
                // Defeat
            }

            isBattle = false;
            scene.SetActive(false);
            GameManager.instance.isBattle = false;
        }
        else
        {
            while (activeInBattle[turn].hp == 0)
            {
                turn++;
                if (turn >= activeInBattle.Count)
                {
                    turn = 0;
                }
            }
        }
    }

    public IEnumerator EnemyMoveCoroutine()
    {
        isTurnWaiting = false;

        yield return new WaitForSeconds(1f);
        EnemyAttack();
        yield return new WaitForSeconds(1f);
        NextTurn();
    }

    public void EnemyAttack()
    {
        List<int> targetsAt = new List<int>();

        for (int i = 0; i < activeInBattle.Count; i++)
        {
            if (activeInBattle[i].isPlayer && activeInBattle[i].hp > 0)
            {
                targetsAt.Add(i);
            }
        }

        int target = targetsAt[Random.Range(0, targetsAt.Count)];
        int attack = Random.Range(0, activeInBattle[turn].abilities.Length);
        int power = 0;

        for (int i = 0; i < abilities.Length; i++)
        {
            if (abilities[i].ability == activeInBattle[turn].abilities[attack])
            {
                Instantiate(abilities[i].effect, activeInBattle[target].transform.position, activeInBattle[target].transform.rotation);
                power = abilities[i].power;
            }
        }

        Instantiate(enemyAttackEffect, activeInBattle[turn].transform.position, activeInBattle[turn].transform.rotation);

        DealDamage(target, power);
    }

    public void DealDamage(int target, int power)
    {
        float atkPower = activeInBattle[turn].strength + activeInBattle[turn].weaponPower;
        float defPower = activeInBattle[target].constitution + activeInBattle[target].armorPower;

        float damage = (atkPower / defPower) * power * Random.Range(.9f, 1.1f);

        // Apply damage
        activeInBattle[target].hp -= Mathf.RoundToInt(damage);

        Instantiate(damageDisplay, activeInBattle[target].transform.position, activeInBattle[target].transform.rotation).SetDamage(Mathf.RoundToInt(damage));

        UpdateUiStats();
    }

    public void UpdateUiStats()
    {
        for (int i = 0; i < names.Length; i++)
        {
            if (activeInBattle.Count > i)
            {
                if (activeInBattle[i].isPlayer)
                {
                    BattleChar data = activeInBattle[i];

                    names[i].gameObject.SetActive(true);
                    names[i].text = data.name;
                    hps[i].text = Mathf.Clamp(data.hp, 0, int.MaxValue) + " / " + data.maxHp;
                    mps[i].text = Mathf.Clamp(data.mp, 0, int.MaxValue) + " / " + data.maxMp;
                }
                else
                {
                    names[i].gameObject.SetActive(false);
                }
            }
            else
            {
                names[i].gameObject.SetActive(false);
            }
        }
    }

    public void PlayerAttack(string ability, int target)
    {
        int power = 0;

        for (int i = 0; i < abilities.Length; i++)
        {
            if (abilities[i].ability == ability)
            {
                Instantiate(abilities[i].effect, activeInBattle[target].transform.position, activeInBattle[target].transform.rotation);
                power = abilities[i].power;
            }
        }

        DealDamage(target, power);

        Instantiate(enemyAttackEffect, activeInBattle[turn].transform.position, activeInBattle[turn].transform.rotation);

        uiActions.SetActive(false);
        targetMenu.SetActive(false);
        NextTurn();
    }

    public void OpenTargetMenu(string ability)
    {
        targetMenu.SetActive(true);

        List<int> enemiesAt = new List<int>();

        for (int i = 0; i < activeInBattle.Count; i++)
        {
            if (!activeInBattle[i].isPlayer)
            {
                enemiesAt.Add(i);
            }
        }

        for (int i = 0; i < targets.Length; i++)
        {
            if (enemiesAt.Count > i)
            {
                targets[i].gameObject.SetActive(true);
                targets[i].ability = ability;
                targets[i].target = enemiesAt[i];
                targets[i].name.text = activeInBattle[enemiesAt[i]].name;
            }
            else
            {
                targets[i].gameObject.SetActive(false);
            }
        }
    }

    public void OpenMagicMenu()
    {
        magicMenu.SetActive(true);

        for (int i = 0; i < magics.Length; i++)
        {
            if (activeInBattle[turn].abilities.Length > i)
            {
                magics[i].gameObject.SetActive(true);
                magics[i].spell = activeInBattle[turn].abilities[i];
                magics[i].nameText.text = magics[i].spell;

                for (int j = 0; j < abilities.Length; j++)
                {
                    if (abilities[j].ability == magics[i].spell)
                    {
                        magics[i].mp = abilities[j].mp;
                        magics[i].mpText.text = magics[i].mp.ToString();
                    }
                }
            } else
            {
                magics[i].gameObject.SetActive(false);
            }
        }
    }
}
