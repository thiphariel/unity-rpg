using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageDisplay : MonoBehaviour
{
    public Text damage;
    public float duration = 1f;
    public float speed = 1f;
    public float offset = .5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, duration);
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }

    public void SetDamage(int damage)
    {
        this.damage.text = damage.ToString();
        transform.position += new Vector3(Random.Range(-offset, offset), Random.Range(-offset, offset), 0);
    }
}
