using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 
[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float walkSpeed = 4; // player left right walk speed
    public bool bIsStealth = false; // is player in stealth mode
    public  float jumpHeight = 2; // height of player's jump
    public float timeToJumpApex = .2f; // time it takes for player to reach jump apex
    private float jumpVelocity;
    private float gravity;

    public float currentHealth;
    public float maxHealth = 3;

    private bool bDamageCooldown = false;
    private bool bCanAttack = true;

    private float attackDamage = 10;
    private bool bInkCooldown = false; 

    Vector3 velocity;
    float velocityXSmoothing;
    float accelerationTimeAirborne = .2f; // acceleration while in the air
    float accelerationTimeGrounded = .1f; // acceleration while grounded

    public GameObject InkSprite; // ink sprite projectile

    Controller2D controller; // handles collision and movement
    Animator anim; // animations
    SpriteRenderer spriteRend;

    AudioSource audio;
    public AudioClip inkShotAudio;
    public AudioClip meleeAttackAudio;
    public AudioClip jumpAudio; 
    // Use this for initialization
    void Start()
    {
        /// Get Components 
        audio = GetComponent<AudioSource>();
        controller = GetComponent<Controller2D>();
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();

        /// Calculate gravity and jump velocity
        gravity = -(jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        currentHealth = maxHealth; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SceneManager.LoadScene(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SceneManager.LoadScene(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SceneManager.LoadScene(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SceneManager.LoadScene(3);
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }
        Vector2 input; // Player input
        if(bDamageCooldown) // If currently in damage cooldown state
        {
            input = new Vector2(0, 0); // player input set to 0
        }
        else // otherwise
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // player input set to controls
        }

        if (Input.GetButtonDown("Jump") && controller.collisions.below && !bDamageCooldown) // If player presses Jump button and is currently grounded
        {
            audio.clip = jumpAudio;
            audio.Play();
            velocity.y = jumpVelocity; // add jump velocity 
        }
        if(Input.GetMouseButtonDown(0) && bCanAttack)
        {
            audio.clip = meleeAttackAudio;
            audio.Play();
            controller.Attack(velocity, attackDamage, spriteRend.flipX);
            StartCoroutine(AttackCooldown(.5f));
        }

        UpdateAnimations(velocity);

        /// Smoothen player horizontal velocity
        float targetVelocityX = input.x * walkSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded: accelerationTimeAirborne) ;

        velocity.y += gravity * Time.deltaTime; // apply gravity to player

        controller.Move(velocity * Time.deltaTime); // move player

        if (Input.GetButtonDown("Stealth") && !bIsStealth && !bDamageCooldown)
        {
            StartCoroutine("StealthMode");
        }

        /// Ink Attack
        if (Input.GetMouseButtonDown(1) && !bDamageCooldown && !bInkCooldown)
        {
            GameObject ink = (GameObject)Instantiate(InkSprite, transform.position, transform.rotation);
            Rigidbody2D rB = ink.GetComponent<Rigidbody2D>();
            float directionX = (spriteRend.flipX) ? -1 : 1;
            rB.AddForce((transform.right * directionX).normalized * 200);
            audio.clip = inkShotAudio;
            audio.Play();
            StartCoroutine("InkCooldown");
        }
    }

    IEnumerator InkCooldown()
    {
        bInkCooldown = true;
        yield return new WaitForSeconds(2);
        bInkCooldown = false; 
    }
    public void TakeDamage(float dmg)
    {
        if(!bDamageCooldown)
        {
            currentHealth -= dmg;
            ApplyKnockback();

            if (currentHealth <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            StartCoroutine(DamageCooldown(3));
            StartCoroutine(DamageCooldown(0.5f));
        }
    }

    public void ApplyKnockback()
    {
        velocity.x = spriteRend.flipX ? 5 : -5; // knockback in opposite direction player is facing
    }
    IEnumerator DamageCooldown(float seconds)
    {
        bDamageCooldown = true;
        yield return new WaitForSeconds(seconds);
        bDamageCooldown = false; 
    }

    IEnumerator AttackCooldown(float seconds)
    {
        anim.SetTrigger("attacking");
        bCanAttack = false;
        yield return new WaitForSeconds(seconds);
        bCanAttack = true;

    }
    //--------------------------------------
    // Player Animation States
    //--------------------------------------
    void UpdateAnimations(Vector3 velocity)
    {
        /// Face player in correct direction based on input
        if (Input.GetAxisRaw("Horizontal") < 0)
            spriteRend.flipX = true;
        else if (Input.GetAxisRaw("Horizontal") > 0)
            spriteRend.flipX = false;

        /// Set Animation states
        anim.SetBool("walking", Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0); // If player walking
        anim.SetBool("grounded", velocity.y == 0);
        anim.SetBool("falling", velocity.y < 0);
        anim.SetBool("jumping", velocity.y > 0);
        anim.SetBool("hit", bDamageCooldown); 
    }

    //--------------------------------------
    // Stealth Mode
    //--------------------------------------
    IEnumerator StealthMode()
    {
        bIsStealth = true;
        spriteRend.color = new Color(1, 1, 1, .5f);
        yield return new WaitForSeconds(3);
        spriteRend.color = new Color(1, 1, 1, 1f);

        bIsStealth = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("Colliding with something");

            Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), other.gameObject.GetComponent<BoxCollider2D>());
        }
    }
}