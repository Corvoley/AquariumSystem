using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterFacing2D))]
public class VisionComponent : MonoBehaviour
{
    [SerializeField] private float visionRange = 10;
    [SerializeField] private float visionAngle = 30;
    [SerializeField] private Transform visionPosition;
    private CharacterFacing2D characterFacing;

    private void Awake()
    {
        characterFacing = GetComponent<CharacterFacing2D>();
    }

    public bool IsVisible(GameObject target)
    {

        if (target == null)
        {
            return false;
        }
        if (Vector2.Distance(visionPosition.position, target.transform.position) > visionRange)
        {
            return false;
        }
        Vector2 toTarget = target.transform.position - visionPosition.position;
        Vector2 visionDir = GetVisionDirection();
        if (Vector2.Angle(visionDir, toTarget) > visionAngle / 2)
        {
            return false;
        }
        return true;

    }
    private Vector2 GetVisionDirection()
    {
        if (characterFacing == null)
        {
            return Vector2.left;
        }
        return characterFacing.IsFacingRight ? Vector3.right : Vector3.left;
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere(transform.position, visionRange);

        Vector3 visionDirection = GetVisionDirection();
        Gizmos.DrawLine(visionPosition.position, visionPosition.position + Quaternion.Euler(0, 0, visionAngle / 2) * visionDirection * visionRange);
        Gizmos.DrawLine(visionPosition.position, visionPosition.position + Quaternion.Euler(0, 0, -visionAngle / 2) * visionDirection * visionRange);
    }

    
}
