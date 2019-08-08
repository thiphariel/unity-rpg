using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public string[] quests;
    public bool[] questsComplete;

    public static QuestManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        questsComplete = new bool[quests.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(CheckStatus("quest test"));
            CompleteQuest("quest test");
            ImcompleteQuest("quest 2");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadQuestData();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            SaveQuestData();
        } 
    }

    public int GetQuestPosition(string quest)
    {
        for (int i = 0; i < quests.Length; i++)
        {
            if (quests[i] == quest)
            {
                return i;
            }
        }

        Debug.LogError("Quest " + quest + " does not exists !");
        return 0;
    }

    public bool CheckStatus(string quest)
    {
        int position = GetQuestPosition(quest);

        if (position != 0)
        {
            return questsComplete[position];
        }

        return false;
    }

    public void CompleteQuest(string quest)
    {
        questsComplete[GetQuestPosition(quest)] = true;
        UpdateQuestObjects();
    }

    public void ImcompleteQuest(string quest)
    {
        questsComplete[GetQuestPosition(quest)] = false;
        UpdateQuestObjects();
    }

    public void UpdateQuestObjects()
    {
        QuestObjectActivator[] questObjects = FindObjectsOfType<QuestObjectActivator>();

        for (int i = 0; i < questObjects.Length; i++)
        {
            questObjects[i].CheckStatus();
        }
    }

    public void SaveQuestData()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            if (questsComplete[i])
            {
                PlayerPrefs.SetInt("Quest_" + quests[i], 1);
            }
        }
    }

    public void LoadQuestData()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            int value = 0;

            if (PlayerPrefs.HasKey("Quest_" + quests[i]))
            {
                value = PlayerPrefs.GetInt("Quest_" + quests[i]);
            }

            questsComplete[i] = value == 1 ? true : false;
        }
    }
}
