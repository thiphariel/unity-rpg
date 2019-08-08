using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectActivator : MonoBehaviour
{
    public GameObject toActivate;
    public string quest;
    public bool activeOnCmmplete;

    private bool isChecked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChecked)
        {
            CheckStatus();
            isChecked = true;
        }
    }

    public void CheckStatus()
    {
        if (QuestManager.instance.CheckStatus(quest))
        {
            toActivate.SetActive(activeOnCmmplete);
        }
    }
}
