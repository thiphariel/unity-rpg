using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string quest;
    public bool complete;
    public bool triggerOnEnter;
    public bool deactivateOnComplete;

    private bool canComplete;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canComplete && Input.GetButtonDown("Fire1"))
        {
            canComplete = false;
            CompleteQuest();
        }
    }

    public void CompleteQuest()
    {
        if (complete)
        {
            QuestManager.instance.CompleteQuest(quest);
        } else
        {
            QuestManager.instance.ImcompleteQuest(quest);
        }

        gameObject.SetActive(!deactivateOnComplete);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (triggerOnEnter)
            {
                CompleteQuest();
            } else
            {
                canComplete = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canComplete &= collision.tag != "Player";
    }
}
