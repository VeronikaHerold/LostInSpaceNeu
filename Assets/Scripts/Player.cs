using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private FragmentManager f;
    public float speed = 5f;
    public float jumph = 5f;
    private bool isgrounded = false;

    private Rigidbody2D rb2D;
    private Animator anim;

    public LayerMask GroundLayer;
    public LayerMask WallLayer;

    public GameObject panel;
    public GameObject death_effect;

    public buttonManager buttonManager;
    public CameraFollow cameraFollow;

    void Start()
    {
        this.rb2D = this.GetComponent<Rigidbody2D>();
        this.anim = GetComponent<Animator>();

        cameraFollow.SetTarget(transform);

        f = GameObject.FindGameObjectWithTag("fragment").GetComponent<FragmentManager>();
    }

    void Update()
    {
        float direction = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(direction * speed, rb2D.velocity.y);
        if (direction < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (direction > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (direction != 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        if (isgrounded == false)
        {
            anim.SetBool("IsJumping", true);
        }
        else
        {
            anim.SetBool("IsJumping", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isgrounded)
        {
            rb2D.AddForce(Vector2.up * jumph, ForceMode2D.Impulse);
            isgrounded = false;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && isgrounded)
        {
            rb2D.AddForce(Vector2.up * jumph, ForceMode2D.Impulse);
            isgrounded = false;
        }
        else if (Input.GetKeyDown(KeyCode.W) && isgrounded)
        {
            rb2D.AddForce(Vector2.up * jumph, ForceMode2D.Impulse);
            isgrounded = false;
        }

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
    }

    void OnDestroy()
    {
        // Entferne das Kamerafolge-Ziel, wenn der Spieler zerstört wird
        cameraFollow.SetTarget(null);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name);
        if (collision.gameObject.tag == "enemy")
        {
            AudioManagerNew.Instance.PlaySFX("Death");
            Destroy(gameObject);
            panel.SetActive(true);
        }
        if (collision.gameObject.tag == "ground")
        {
            isgrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "fragment")
        {
            f.Addfragment();
            Destroy(other.gameObject);
            AudioManagerNew.Instance.PlaySFX("Collect");
        }
        if (other.gameObject.tag == "spike")
        {
            Instantiate(death_effect, transform.position, Quaternion.identity);
            panel.SetActive(true);
            Destroy(gameObject);
            AudioManagerNew.Instance.PlaySFX("Death");
        }
        if (other.gameObject.tag == "finish")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            AudioManagerNew.Instance.musicSource.Stop();
            AudioManagerNew.Instance.PlaySFX("Next");
        }
    }
}
