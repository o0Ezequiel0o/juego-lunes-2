using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Gun : MonoBehaviour
{
    [Header("Gun Data")]
    [SerializeField] protected Transform mainBarrel;
    [SerializeField] protected LayerMask barrelBlockLayers;
    [Space]
    [SerializeField] protected SortingGroup sortingGroup;
    [Space]
    [SerializeField] public GameObject projectile;
    [Header("Visual")]
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private float muzzleFlashSeconds = 0.05f;
    [Header("Sound")]
    [SerializeField] protected AudioSource audioSource;
    [Space]
    [SerializeField] protected Sound fireSound;
    [SerializeField] protected Sound emptySound;
    [field: Header("Stats")]
    [field: SerializeField] public bool IsAutomatic { get; private set;} = false;
    [SerializeField] protected float baseFireCooldown;
    [Space]
    [SerializeField] protected int baseDamage;
    [SerializeField] protected float baseRange;
    [SerializeField] protected float baseSpeed;
    [Space]
    [SerializeField] protected float baseReloadTime;
    [field: Space]
    [field: SerializeField] public int Ammo { get; protected set;} = 1;
    [field: SerializeField] public int TotalAmmo { get; protected set;} = 0;
    [Space]
    [SerializeField] private float spread;

    protected float Spread => Random.Range(-spread, spread);

    protected float FireCooldown => baseFireCooldown * entityData.fireCooldownMultiplier;

    protected float ReloadTime => baseReloadTime * entityData.reloadTimeMultiplier;

    protected float Speed => baseSpeed * entityData.bulletSpeedMultiplier;

    protected float Range => baseRange * entityData.rangeMultiplier;

    protected int Damage => (int)Mathf.Round(baseDamage * entityData.damageMultiplier);

    public int MaxAmmo => (int)Mathf.Ceil(baseMaxAmmo * entityData.ammoMultiplier);

    protected int maxTotalAmmo = 0;
    protected int baseMaxAmmo = 0;

    public virtual bool CanUnequip => currentState == WeaponState.Idle;

    public bool IsDropped {get; protected set;} = true;

    public bool IsFull => Ammo >= MaxAmmo;
    public bool HasAmmo => Ammo != 0;

    protected float timeSinceFired = 0f;
    private float muzzleFlashTimer = 0f;

    protected EntityData entityData;

    protected WeaponState currentState = WeaponState.Idle;

    protected enum WeaponState
    {
        Idle,
        Firing,
        Reloading
    }

    protected enum CanFireState
    {
        Ready,
        Empty,
        Busy
    }

    protected const int GRABBED_LAYER = 0; //Default
    protected const int DROPPED_LAYER = 52322489; //Floor

    public virtual void Use(bool holdingButton) {}
    
    public virtual void AltUse(bool holdingButton) {}

    protected virtual void OnGrab(Entity entity) {}

    protected virtual void OnDrop(Entity entity) {}

    protected virtual void OnStart() {}

    protected virtual void OnUpdate() {}

    protected void ChangeState(WeaponState newState)
    {
        currentState = newState;
    }

    void Start()
    {
        if (IsDropped)
        {
            sortingGroup.sortingLayerID = DROPPED_LAYER;
        }
        else
        {
            sortingGroup.sortingLayerID = GRABBED_LAYER;
        }

        maxTotalAmmo = Ammo;
        baseMaxAmmo = Ammo;

        timeSinceFired = baseFireCooldown;

        OnStart();
    }

    void Update()
    {
        if (currentState == WeaponState.Idle)
        {
            timeSinceFired += Time.deltaTime;
        }

        if (muzzleFlashTimer < muzzleFlashSeconds)
        {
            muzzleFlashTimer += Time.deltaTime;

            if (muzzleFlashTimer >= muzzleFlashSeconds)
            {
                DisableMuzzleFlash();
            }
        }

        OnUpdate();
    }

    public void Grab(EntityData entityData) 
    {
        sortingGroup.sortingLayerID = GRABBED_LAYER;

        this.entityData = entityData;
        IsDropped = false;
    }

    public void Drop()
    {
        sortingGroup.sortingLayerID = DROPPED_LAYER;

        entityData = null;
        IsDropped = true;
    }

    protected CanFireState CanFire(bool holdingMouse)
    {
        if (!HasAmmo)
        {
            return CanFireState.Empty;
        }
        if (holdingMouse && !IsAutomatic)
        {
            return CanFireState.Busy;
        }
        if (timeSinceFired < FireCooldown || BarrelBlocked())
        {
            return CanFireState.Busy;
        }

        return CanFireState.Ready;
    }

    public virtual void Reload()
    {
        if (currentState == WeaponState.Idle)
        {
            if (!IsFull && TotalAmmo > 0)
            {
                StartCoroutine(Reloading());
            }
        }
    }

    private IEnumerator Reloading()
    {
        ChangeState(WeaponState.Reloading);

        yield return new WaitForSeconds(ReloadTime);
        
        OnReload();
        ChangeState(WeaponState.Idle);
    }

    private void OnReload()
    {
        int ammoToReload = Mathf.Min(TotalAmmo, MaxAmmo - Ammo);

        TotalAmmo -= ammoToReload;
        Ammo += ammoToReload;
    }

    protected void CreateBullet(Transform newTransform, GameObject prefab)
    {
        Projectile bullet = ProjectilePoolManager.Instance.objectPool.GetObject(prefab).GetComponent<Projectile>();

        bullet.ResetProjectile(newTransform.position, Quaternion.Euler(0, 0, newTransform.rotation.eulerAngles.z + Spread));
        
        bullet.team = entityData.Entity.Team;

        bullet.range = Range;
        bullet.speed = Speed;

        bullet.damage = Damage;
    }

    public bool BarrelBlocked()
    {
        return Physics2D.Linecast(entityData.Center, mainBarrel.position, barrelBlockLayers);
    }

    protected void EnableMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
        muzzleFlashTimer = 0f;
    }

    protected void DisableMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
        muzzleFlashTimer = muzzleFlashSeconds;
    }

    void OnDisable()
    {
        DisableMuzzleFlash();
    }
}