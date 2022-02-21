using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    public float MoveSpeed => moveSpeed;

    private Rigidbody2D rb;
    Vector2 moveDirection;
    private SpriteRenderer sprite;
    float oldXpos;
    private bool isFacingRight;
    
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        oldXpos = transform.position.x;
    }
    private void FixedUpdate()
    {
        //Movement(moveDirection, speed);
    }
    private void Update()
    {
        
        moveDirection = GetMovementInput();
        CheckMovingDirection();
        SpriteFlipDiretion();
    }
    public void MoveToPositionFast(Vector3 destination, float moveTime)
    {
        float timePercent =  moveTime / 100;        
        rb.MovePosition(Vector3.Lerp(transform.position, destination, timePercent));
    }

    public void MoveToPositionSlowing(Vector3 destination, float moveTime, float timeFromLastMove)
    {
        float timePercent = (Time.time - timeFromLastMove) / moveTime;
        timePercent = Mathf.Sin(timePercent * Mathf.PI * 0.5f);
        rb.MovePosition(Vector3.Lerp(transform.position, destination, timePercent));   
    }

    public void MoveToPosition(Vector3 destination, float moveTime, float timeFromLastMove)
    {
        float timePercent = (Time.time - timeFromLastMove) / moveTime;
        rb.MovePosition(Vector3.Lerp(transform.position, destination, timePercent));

    }
    public void MoveToPosSmoothStep(Vector3 destination, float moveTime, float timeFromLastMove)
    {
        float timePercent = (Time.time - timeFromLastMove) / moveTime;
        timePercent = timePercent * timePercent * (3f - 2f * timePercent);
        rb.MovePosition(Vector3.Lerp(transform.position, destination, timePercent));

    }
   

    public void Movement(Vector2 movementInput, float speed)
    {
        float speedX = movementInput.x * speed;
        float speedY = movementInput.y * speed;
       
        rb.velocity = new Vector2(speedX, speedY);        
    }
    public void Movement(float dirX, float dirY, float speed)
    {
        float speedX = dirX * speed;
        float speedY = dirY * speed;

        rb.velocity = new Vector2(speedX, speedY);
    }

    private Vector2 GetMovementInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        return new Vector2(horizontalInput, verticalInput);
    }
    private void CheckMovingDirection()
    {
        if (transform.position.x > oldXpos)
        {
            oldXpos = transform.position.x;
            isFacingRight = true;

        }
        else if(transform.position.x < oldXpos)
        {
            oldXpos = transform.position.x;
            isFacingRight = false;
        }     

    }
    private void SpriteFlipDiretion()
    {
        if (isFacingRight)
        {
            sprite.flipX = true;
        }
        if (!isFacingRight)
        {
            sprite.flipX = false;
        }
    }
}
