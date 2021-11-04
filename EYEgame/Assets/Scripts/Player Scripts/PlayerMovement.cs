using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpPower = 10;
    public int maxHealth = 10;
    public int currentHealth;
    public HealthBar healthbar;

    Vector2 movement = new Vector2();
    bool grounded;
    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            //rb2d.velocity = new Vector2(rb2d.velocity.x, jumpPower);
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        movement.x = x * speed;

        transform.rotation = Quaternion.identity;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(2);
        }
    }
    void FixedUpdate()
    {
        movement.y = rb2d.velocity.y;
        rb2d.velocity = movement;
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.setHealth(currentHealth); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        grounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        grounded = false;
    }

    public void StopMoving()
    {
        speed = 0;
    }

    public void StartMoving()
    {
        speed = 8;
    }
}
