using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [HideInInspector] public bool canMove = true;
    public float moveSpeed;
    public Rigidbody2D rb;

    public GameObject bulletToFire;
    public Transform firePoints;

    public float timeBetweenShots;
    private float shotCounter;

    [SerializeField] private Sprite idleDownSprite;
    [SerializeField] private Sprite runDownSprite;
    [SerializeField] private Sprite runUpSprite;
    [SerializeField] private Sprite runSideSprite;

    public SpriteRenderer spriteRenderer;
    private Vector2 moveInput;
    private Animator spriteAnimator;

    private void Awake()
    {
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove && !LevelManager.instance.isPaused)
        {
            GetMovementInput();
            GetShootingInput();
            UpdateSprite();
            UpdateAnimation();
        }
        else
        {
            rb.velocity = Vector2.zero;
            spriteAnimator.Play("idle_down");
        }
    }

    private void GetMovementInput()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb.velocity = moveInput * moveSpeed;
    }

    private void GetShootingInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Instantiate(bulletToFire, firePoints.GetChild(0).position, firePoints.GetChild(0).rotation);
            AudioManager.instance.PlaySFX(12);
            shotCounter = timeBetweenShots;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Instantiate(bulletToFire, firePoints.GetChild(1).position, firePoints.GetChild(1).rotation);
            AudioManager.instance.PlaySFX(12);
            shotCounter = timeBetweenShots;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Instantiate(bulletToFire, firePoints.GetChild(2).position, firePoints.GetChild(2).rotation);
            AudioManager.instance.PlaySFX(12);
            shotCounter = timeBetweenShots;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Instantiate(bulletToFire, firePoints.GetChild(3).position, firePoints.GetChild(3).rotation);
            AudioManager.instance.PlaySFX(12);
            shotCounter = timeBetweenShots;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            shotCounter -= Time.deltaTime;

            if(shotCounter <= 0)
            {
                Instantiate(bulletToFire, firePoints.GetChild(0).position, firePoints.GetChild(0).rotation);
                AudioManager.instance.PlaySFX(12);
                shotCounter = timeBetweenShots;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                Instantiate(bulletToFire, firePoints.GetChild(1).position, firePoints.GetChild(1).rotation);
                AudioManager.instance.PlaySFX(12);
                shotCounter = timeBetweenShots;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                Instantiate(bulletToFire, firePoints.GetChild(2).position, firePoints.GetChild(2).rotation);
                AudioManager.instance.PlaySFX(12);
                shotCounter = timeBetweenShots;
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                Instantiate(bulletToFire, firePoints.GetChild(3).position, firePoints.GetChild(3).rotation);
                AudioManager.instance.PlaySFX(12);
                shotCounter = timeBetweenShots;
            }
        }
    }

    private void UpdateSprite()
    {
        if (moveInput.x > 0)
        {
            spriteRenderer.sprite = runSideSprite;
            spriteRenderer.flipX = true;
        }
        else if (moveInput.x < 0)
        {
            spriteRenderer.sprite = runSideSprite;
            spriteRenderer.flipX = false;
        }
        else if (moveInput.y > 0)
        {
            spriteRenderer.sprite = runUpSprite;
        }
        else if (moveInput.y < 0)
        {
            spriteRenderer.sprite = runDownSprite;
        }
        else
        {
            spriteRenderer.sprite = idleDownSprite;
        }
    }

    private void UpdateAnimation()
    {
        if (moveInput.x != 0)
        {
            spriteAnimator.Play("run_side");
        }
        else if (moveInput.y > 0)
        {
            spriteAnimator.Play("run_up");
        }
        else if (moveInput.y < 0)
        {
            spriteAnimator.Play("run_down");
        }
        else
        {
            spriteAnimator.Play("idle_down");
        }
    }
}
