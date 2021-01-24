using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioClip gBreak;
    [Space]
    public float speed;
    public float maxHealth;
    public float currentHealth;

    private float radius = 0.5f;
    //Move
    public bool canMove = true;
    Rigidbody2D rb;

    float horizontal;
    float vertical;

    private CircleCollider2D cc;


    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        cc.radius = radius;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CanMove())
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            Move();
        }
        else
            rb.velocity = new Vector2(0, 0);
    }
    private void Move()
    {
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
    void Init(float radius, float speed)
    {
        this.radius = radius;
        this.speed = speed;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Const.PLAYERCOLOR;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Gamemanager.Instance.PlayClip(gBreak);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        currentHealth -= Time.deltaTime;
        if(currentHealth <= 0)
        {
            EventManager.GameOver();
        }
        EventManager.TakeDamage(maxHealth, currentHealth);
        Debug.Log(collision.name);
    }
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        EventManager.TakeDamage(maxHealth, currentHealth);
    }

    bool CanMove()
    {
        return (canMove && (Gamemanager.Instance.currentState == Gamemanager.Gamestate.playing));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Char"))
            EventManager.GameOver();
        if (collision.gameObject.CompareTag("EndPos"))
            Gamemanager.Instance.Finished();
        Debug.Log(collision.gameObject.name);
    }
}

