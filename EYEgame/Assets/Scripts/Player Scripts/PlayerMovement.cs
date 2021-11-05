using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public AudioSource JumpSound;
    public AudioSource Walking;
    private float speed = 5;
    public float jumpPower = 5;
    public int maxHealth = 10;
    public float currentHealth;
    public HealthBar healthbar;
    public bool isHidden;
    public Animator animator;

     
    Vector2 movement = new Vector2();
    public bool isGrounded = false;
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
        Jump();

        Movement();
        float x = Input.GetAxis("Horizontal");

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

        if(isHidden != true)
        {
            StopMoving();
        }

        if(currentHealth <= 0)
        {
            SceneManager.LoadScene("EndScene");
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

    public void Movement()
    {
        float x = Input.GetAxis("Horizontal");

        movement.x = x * speed;
        if (Input.GetKey(KeyCode.A))
        {
            Walking.Play();
            animator.transform.eulerAngles = new Vector3(0, -180, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Walking.Play();
            animator.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //rb2d.velocity = new Vector2(rb2d.velocity.x, jumpPower);
            JumpSound.Play();
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
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
        isGrounded = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Barrel"))
        {
            isHidden = false;
        }
        else
        {
            isGrounded = false;
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
