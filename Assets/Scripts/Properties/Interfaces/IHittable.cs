using UnityEngine;

public interface IHittable
{
    void Hit(Vector2 point, float forceMultiplier = 1f);
}