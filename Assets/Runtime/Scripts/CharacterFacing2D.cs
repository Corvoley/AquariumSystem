using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFacing2D : MonoBehaviour
{
    [SerializeField] private GameObject fishMouth;
    Vector3 fishMouthPos;
    public Vector3 FishMouthPos => fishMouth.transform.position;
    float oldXpos;
    private bool isFacingRight;
    public bool IsFacingRight => isFacingRight;
    private SpriteRenderer fishSprite;
    private void Awake()
    {
        fishSprite = GetComponent<SpriteRenderer>();    
    }
    private void Start()
    {
        oldXpos = transform.position.x;
        fishMouthPos = fishMouth.transform.localPosition;
    }
    private void Update()
    {
        CheckMovingDirection();
        SpriteFlipDiretion();
    }
    private void CheckMovingDirection()
    {
        if (transform.position.x > oldXpos)
        {
            oldXpos = transform.position.x;
            isFacingRight = true;

        }
        else if (transform.position.x < oldXpos)
        {
            oldXpos = transform.position.x;
            isFacingRight = false;
        }

    }
    private void SpriteFlipDiretion()
    {

        if (isFacingRight)
        {
            fishSprite.flipX = true;

            //makes the mouth position be positive
            fishMouthPos.x = Mathf.Abs(fishMouthPos.x);
            fishMouth.transform.localPosition = fishMouthPos;
        }


        if (!isFacingRight)
        {
            fishSprite.flipX = false;

            //makes the mouth position be negative
            if (fishMouthPos.x > 0)
            {
                fishMouthPos.x = -fishMouthPos.x;
            }
            else
            {
                fishMouth.transform.localPosition = fishMouthPos;
            }

        }
    }
}
