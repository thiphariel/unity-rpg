using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public float duration;
    public int sfx;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySfx(sfx);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, duration);
    }
}
