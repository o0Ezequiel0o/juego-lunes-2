using UnityEngine;
using System;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected Transform _transform;
    [Space]
    [SerializeField] protected Transform tip;
    [Space]
    [SerializeField] protected LayerMask hitLayers;

    [HideInInspector] public int team;

    [HideInInspector] public float range;
    [HideInInspector] public float speed;
    [HideInInspector] public int damage;

    protected Vector3 direction;

    private Vector3 startPosition;
    private Vector3 oldPosition;
    private Vector3 newPosition;

    public Action OnDestroyProjectile;

    protected RaycastHit2D[] hitsCache;

    protected bool destroyThisFrame = false;

    public void ResetProjectile(Vector3 position, Quaternion rotation)
    {
        _transform.position = position;
        _transform.rotation = rotation;

        float zRadians = _transform.eulerAngles.z * Mathf.Deg2Rad;

        direction.x = Mathf.Cos(zRadians);
        direction.y = Mathf.Sin(zRadians);

        startPosition = oldPosition = newPosition = tip.position;

        destroyThisFrame = false;

        OnResetProjectile();
    }

    public virtual void OnResetProjectile() {}

    protected void Move()
    {
        oldPosition = newPosition;

        if ((startPosition - _transform.position).sqrMagnitude >= range*range)
        {
            _transform.position = startPosition + direction * range;
            destroyThisFrame = true;
        }
        else
        {
            _transform.position += direction * speed * Time.deltaTime;
        }
        newPosition = tip.position;
    }

    protected void MoveToPoint(Vector3 hitPoint)
    {
        newPosition.x = hitPoint.x - tip.localPosition.x * direction.x;
        newPosition.y = hitPoint.y - tip.localPosition.x * direction.y;
                        
        _transform.position = newPosition;
    }

    protected RaycastHit2D[] CheckLineCollision()
    {
        return Physics2D.LinecastAll(oldPosition, tip.position, hitLayers);
    }

    private void Update()
    {
        Move();
        hitsCache = CheckLineCollision();

        foreach (var hit in hitsCache)
        {
            Entity entity = hit.collider.GetComponent<Entity>();
            if (entity != null && entity.Team != team)
            {
                entity.TakeDamage(damage);
                Destroy(gameObject);
                return;
            }
        }
        if (destroyThisFrame)
        {
            Destroy(gameObject);
        }
    }
}