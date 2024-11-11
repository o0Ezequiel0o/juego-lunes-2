using UnityEngine;
using System;

[RequireComponent(typeof(Entity))]
public class EntityData : MonoBehaviour
{
    [field: Header("Data")]
    [field: SerializeField] public Entity Entity {get; private set;}
    [SerializeField] private Transform center;
    [Header("Physics")]
    public Rigidbody2D rb;
    [field: SerializeField] public Collider2D Collider {get; private set;}
    [Space]
    public Vector2 forces;
    public float drag;
    [Header("Aiming")]
    public Vector2 aimDirection;
    [Header("Movement")]
    public float baseMoveSpeed;
    public Vector2 moveDirection;
    [Header("Armor")]
    [SerializeField] float baseArmor;
    [Header("Multipliers")]
    public float abilityCooldownMultiplier = 1f;
    public float fireCooldownMultiplier = 1f;
    public float bulletSpeedMultiplier = 1f;
    public float reloadTimeMultiplier = 1f;
    public float moveSpeedMultiplier = 1f;
    public float damageMultiplier = 1f;
    public float rangeMultiplier = 1f;
    public float armorMultiplier = 1f;
    public float ammoMultiplier = 1f;

    public Action grabWeapon;
    public Action dropWeapon;
    public Action reload;

    public Action<int> switchWeapon;

    public Action<bool> fire;
    public Action<int> useAbility;

    public float MoveSpeed => Mathf.Max(0, baseMoveSpeed * moveSpeedMultiplier);

    public float Armor => baseArmor * armorMultiplier;

    public float DamageReduction => 1 - (Armor / (100 + Mathf.Abs(Armor)));

    public Vector3 Center => transform.position;
}