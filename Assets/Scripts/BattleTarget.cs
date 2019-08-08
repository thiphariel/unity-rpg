using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTarget : MonoBehaviour
{
    public string ability;
    public int target;
    public new Text name;

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
        BattleManager.instance.PlayerAttack(ability, target);
    }
}
