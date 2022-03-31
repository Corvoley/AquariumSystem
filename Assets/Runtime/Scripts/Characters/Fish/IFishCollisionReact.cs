using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct FishCollisionInfo
{
    public FishAI FishAI;
    public HungerSystem HungerSystem;
    public Collider2D Collider2D;

}
public interface IFishCollisionReact
{
    void ReactToFishCollision(in FishCollisionInfo collisionInfo);
}

