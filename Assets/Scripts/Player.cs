using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private FragmentManager f;
    public float Speed = 200f;
    public float JumpForce = 3f;
    public float WallJumpForce = 5f;
    public float spaceGravityForce = -5f;
    public float maxVerticalSpeed = 3f;
    public float groundGravityScale = 0.5f;
    public float airGravityScale = 0.5f;
    public float dampingJumpMultiplier = 1.98f; 
    public float linearDamping = 0.99f; 

    public LayerMask GroundLayer;
    public LayerMask WallLayer;
    private bool isJumping = false;

    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private Vector2 moveX;
    public GameObject panel;
    public GameObject death_effect;

    public buttonManager buttonManager;
    public CameraFollow cameraFollow;
    void Start()
    {

       // FindObjectOfType<AudioManager>().Play("Background");
        this.rb2D = this.GetComponent<Rigidbody2D>();
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.animator = this.GetComponent<Animator>();
        this.boxCollider = this.GetComponent<BoxCollider2D>();
        rb2D.gravityScale = 0f;

        cameraFollow.SetTarget(transform);

        //       f = GameObject.FindGameObjectWithTag("fragment").GetComponent<FragmentManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (buttonManager.isPaused)
            {
                buttonManager.Continue();
            }
            else
            {
                buttonManager.Pause();
            }
        }
        this.moveX = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        float horizontal = Input.GetAxis("Horizontal");
        bool isGrounded = this.isGrounded();
        bool isTouchingWall = this.isTouchingWall();

        if (horizontal > 0)
        {
            this.spriteRenderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            this.spriteRenderer.flipX = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            // only jump when player is on the ground
            if (isGrounded)
            {
                this.rb2D.gravityScale = groundGravityScale;
                this.rb2D.velocity = new Vector2(this.rb2D.velocity.x, this.JumpForce);
                isJumping = true;
            }
            // wall jump logic
            else if (isTouchingWall && isJumping)
            {
                this.rb2D.gravityScale = airGravityScale;
                this.rb2D.velocity = new Vector2(-horizontal * this.WallJumpForce, this.JumpForce);
            }
        }

        // setting animator parameters
        this.animator.SetBool("isRunning", horizontal != 0);
        this.animator.SetBool("isGrounded", isGrounded);
        this.animator.SetBool("isTouchingWall", isTouchingWall);
        this.animator.SetBool("isFalling", !isGrounded && this.rb2D.velocity.y < 0);

        if (isJumping && !Input.GetButton("Jump"))
        {
            isJumping = false;
        }
    }
    void OnDestroy()
    {
        // Entferne das Kamerafolge-Ziel, wenn der Spieler zerstört wird
        cameraFollow.SetTarget(null);
    }

    private void FixedUpdate()
    {
        this.rb2D.AddForce(this.moveX * Time.fixedDeltaTime * this.Speed);

        if (!isJumping)
        {
            Vector2 spaceGravity = new Vector2(0f, spaceGravityForce);
            this.rb2D.AddForce(spaceGravity * Time.fixedDeltaTime);
            this.rb2D.velocity = new Vector2(this.rb2D.velocity.x, Mathf.Clamp(this.rb2D.velocity.y, -maxVerticalSpeed, maxVerticalSpeed));
            this.rb2D.velocity *= new Vector2(linearDamping, 1f);
        }
        else
        {
            this.rb2D.velocity *= new Vector2(dampingJumpMultiplier, 1f);
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(this.boxCollider.bounds.center, this.boxCollider.bounds.size, 0, Vector2.down, 0.1f, this.GroundLayer);

        return raycastHit.collider != null;
    }

    private bool isTouchingWall()
    {
        Vector2 direction = this.spriteRenderer.flipX ? Vector2.left : Vector2.right;
        RaycastHit2D raycastHit = Physics2D.BoxCast(this.boxCollider.bounds.center, this.boxCollider.bounds.size, 0, direction, 0.1f, this.WallLayer);

        return raycastHit.collider != null;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            Destroy(gameObject);
            panel.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "fragment")
        {
            f.Addfragment();
            Destroy(other.gameObject);
            AudioManagerNew.Instance.PlaySFX("Collect");
            // FindObjectOfType<AudioManager>().Play("Fragment");
        }
        if (other.gameObject.tag == "spike")
        {
            Instantiate(death_effect, transform.position, Quaternion.identity);
            panel.SetActive(true);
            Destroy(gameObject);
            AudioManagerNew.Instance.PlaySFX("Gameover");
         //   FindObjectOfType<AudioManager>().Play("Die");
          //  FindObjectOfType<AudioManager>().Stop("Background");
          //  FindObjectOfType<AudioManager>().Play("Background_Die");
        }
        if (other.gameObject.tag == "finish")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            AudioManagerNew.Instance.musicSource.Stop();
            AudioManagerNew.Instance.PlaySFX("Victory");

            //  FindObjectOfType<AudioManager>().Stop("Background");
            //   FindObjectOfType<AudioManager>().Play("Victory");
        }
    }
}
