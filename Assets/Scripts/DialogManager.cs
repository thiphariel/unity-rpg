using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private bool isNew;

    public GameObject dialogBox;
    public GameObject nameBox;
    public Text dialog;
    public new Text name;

    public string[] dialogLines;
    public int line;

    public static DialogManager instance;

    private string quest;
    private bool complete;
    private bool shouldUpdateQuest;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogBox.activeInHierarchy)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                if (!isNew)
                {
                    line++;

                    if (line >= dialogLines.Length)
                    {
                        dialogBox.SetActive(false);
                        GameManager.instance.isDialogOpen = false;

                        if (shouldUpdateQuest)
                        {
                            shouldUpdateQuest = false;

                            if (complete)
                            {
                                QuestManager.instance.CompleteQuest(quest);
                            } else
                            {
                                QuestManager.instance.ImcompleteQuest(quest);
                            }
                        }
                    }
                    else
                    {
                        IsNameLine();
                        dialog.text = dialogLines[line];
                    }
                } else
                {
                    isNew = false;
                }
            }
        }
    }

    // Display dialog on the screen
    public void ShowDialog(string[] lines, bool isPerson)
    {
        dialogLines = lines;
        line = 0;

        IsNameLine();
        dialog.text = dialogLines[line];

        dialogBox.SetActive(true);
        nameBox.SetActive(isPerson);

        isNew = true;

        GameManager.instance.isDialogOpen = true;
    }

    private void IsNameLine()
    {
        if (dialogLines[line].StartsWith("n-", System.StringComparison.Ordinal))
        {
            name.text = dialogLines[line].Replace("n-", "");
            line++;
        }
    }

    public void ActivateQuest(string quest, bool complete)
    {
        this.quest = quest;
        this.complete = complete;

        shouldUpdateQuest = true;
    }
}
