using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D body;
    public float speed;
    public Animator animator;
    public string transition;
    public bool canMove = true;

    public static PlayerController instance;

    // Used to retrieve min & max bounds of the map
    private Vector3 min;
    private Vector3 max;

    // Start is called before the first frame update
    void Start()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        int horizontal = (int)Input.GetAxisRaw("Horizontal");
        int vertical = (int)Input.GetAxisRaw("Vertical");

        if (canMove)
        {
            body.velocity = new Vector2(horizontal, vertical) * speed;
        }
        else
        {
            body.velocity = Vector2.zero;
        }

        animator.SetFloat("x", this.body.velocity.x);
        animator.SetFloat("y", this.body.velocity.y);

        if (horizontal == 1 || horizontal == -1 || vertical == 1 || vertical == -1)
        {
            if (canMove)
            {
                animator.SetFloat("lastX", horizontal);
                animator.SetFloat("lastY", vertical);
            }
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min.x, max.x), Mathf.Clamp(transform.position.y, min.y, max.y), transform.position.z);
    }

    // Set bounds of the map
    public void SetBounds(Vector3 min, Vector3 max)
    {
        this.min = min + new Vector3(1f, 1f, 0f);
        this.max = max - new Vector3(1f, 1f, 0f);
    }
}