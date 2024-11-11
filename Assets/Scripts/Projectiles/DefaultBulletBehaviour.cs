using UnityEngine;

public class DefaultBulletBehaviour : Projectile
{
    void Update()
    {
        Move();

        hitsCache = CheckLineCollision();

        for (int i = 0; i < hitsCache.Length; i++)
        {
            OnHit(hitsCache[i].collider.gameObject, hitsCache[i].point);

            if (destroyThisFrame)
            {
                MoveToPoint(hitsCache[i].point);
                break;
            }
        }

        if (destroyThisFrame)
        {
            gameObject.SetActive(false);
        }
    }   

    private void OnHit(GameObject obj, Vector2 point)
    {
        if (obj.TryGetComponent(out IHittable hittable))
        {
            if (obj.TryGetComponent(out IDamageable damageable))
            {
                if (team == damageable.Team)
                {
                    return;
                }

                damageable.TakeDamage(damage);
            }

            hittable.Hit(point);

            destroyThisFrame = true;
        }
    }
}