using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LerpType
{
    Constant,
    Slow,
    Fast,
    SmoothStep
}
public class MovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    public float MoveSpeed => moveSpeed;  

    public Rigidbody2D rb;   
    public CharacterFacing2D characterFacing;
    private void Awake()
    {
        characterFacing = GetComponent<CharacterFacing2D>();
        rb = GetComponent<Rigidbody2D>();
        
    }
    public void MoveToPosition(Vector3 destination, float moveTime, float timeFromLastMove, LerpType lerpType)
    {
        float timePercent = (Time.time - timeFromLastMove) / moveTime;
        switch (lerpType)
        {
            case LerpType.Constant:
                rb.MovePosition(Vector3.Lerp(transform.position, destination + (transform.position - characterFacing.FishMouthPos), timePercent));
                break;
            case LerpType.Slow:                
                timePercent = Mathf.Sin(timePercent * Mathf.PI * 0.5f);
                rb.MovePosition(Vector3.Lerp(transform.position, destination + (transform.position - characterFacing.FishMouthPos), timePercent));
                break;
            case LerpType.Fast:                
                rb.MovePosition(Vector3.Lerp(transform.position, destination, moveTime / 100));
                break;
            case LerpType.SmoothStep:                
                timePercent = timePercent * timePercent * (3f - 2f * timePercent);
                rb.MovePosition(Vector3.Lerp(transform.position, destination + (transform.position - characterFacing.FishMouthPos), timePercent));
                break;
            default:
                rb.MovePosition(Vector3.Lerp(transform.position, destination + (transform.position - characterFacing.FishMouthPos), timePercent));
                break;
        }
        
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

    
}
