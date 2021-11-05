using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpPower = 5;
    public int maxHealth = 10;
    public float currentHealth;
    public HealthBar healthbar;
    public bool isHidden;

    Vector2 movement = new Vector2();
    bool grounded;
    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        Physics2D.queriesStartInColliders = false;
        isHidden = false;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        Jump();

        movement.x = x * speed;



        movement.x = x * speed;

        transform.rotation = Quaternion.identity;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(2);
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.3f);
        //Debug.DrawRay(transform.position, Vector2.right * 0.3f, Color.red, 0.2f);
        if (hit.collider != null)
        {
            if (movement.x > 1)
            {
                movement.x = 0;
            }
        }

        hit = Physics2D.Raycast(transform.position, Vector2.left, 0.3f);
        //Debug.DrawRay(transform.position, Vector2.right * 0.3f, Color.red, 0.2f);
        if (hit.collider != null)
        {
            if (movement.x < -1)
            {
                movement.x = 0;
            }
        }

        hit = Physics2D.Raycast(transform.position, Vector2.down, 0.3f);
        //Debug.DrawRay(transform.position, Vector2.right * 0.3f, Color.red, 0.2f);
        if (hit.collider != null)
        {

        }
    }


    void FixedUpdate()
    {
        movement.y = rb2d.velocity.y;
        rb2d.velocity = movement;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthbar.setHealth(currentHealth);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            //rb2d.velocity = new Vector2(rb2d.velocity.x, jumpPower);

            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        grounded = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Barrel"))
        {
            isHidden = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        grounded = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Barrel"))
        {
            isHidden = false;
        }
        else
        {
            grounded = false;
        }
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
