using System.Collections.Generic;
using UnityEngine;

public class GunController : EntityComponent
{
    [SerializeField] private Transform castCenter;
    [SerializeField] private float grabRadius;
    [SerializeField] private LayerMask grabLayer;
    [Space]
    [SerializeField] private Transform grabPoint;
    [Space]
    [SerializeField] private int maxSlots = 2;

    private readonly List<Gun> guns = new List<Gun>();

    public int CurrentGun {get; private set;} = 0;

    public bool HasGun => guns.Count > 0;

    public Gun EquippedGun => guns[CurrentGun];

    void Start()
    {
        entityData.grabWeapon += GrabWeapon;
        entityData.dropWeapon += DropWeapon;
        entityData.fire += FireWeapon;
        entityData.reload += ReloadWeapon;

        entityData.switchWeapon += SwitchGun;
    }

    public void FireWeapon(bool holdingButton)
    {
        if (HasGun)
        {
            EquippedGun.Use(holdingButton);
        }
    }

    public void ReloadWeapon()
    {
        if (HasGun)
        {
            EquippedGun.Reload();
        }
    }

    public bool HasSlots()
    {
        return maxSlots > guns.Count;
    }

    public bool CanGrab(GameObject obj)
    {
        if (!HasSlots())
        {
            return false;
        }
        if (!obj.TryGetComponent(out Gun gun))
        {
            return false;
        }
        if (!gun.IsDropped)
        {
            return false;
        }

        return true;
    }

    public void GrabWeapon()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(castCenter.position, grabRadius, Vector2.zero, 0, grabLayer);
        
        for (int i = 0; i < hits.Length; i++)
        {
            if (CanGrab(hits[i].collider.gameObject))
            {
                Gun gun = hits[i].collider.gameObject.GetComponent<Gun>();

                if (HasGun)
                {
                    gun.gameObject.SetActive(false);
                }

                gun.Grab(entityData);

                hits[i].collider.gameObject.transform.parent = grabPoint;
                hits[i].collider.gameObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                hits[i].collider.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0,0,-90f));

                guns.Add(gun);
            }
        }
    }

    public void SwitchGun(int num)
    {
        if (num >= guns.Count)
        {
            return;
        }
        if (!EquippedGun.CanUnequip)
        {
            return;
        }

        EquippedGun.gameObject.SetActive(false);
        CurrentGun = num;
        EquippedGun.gameObject.SetActive(true);
    }

    public void DropWeapon()
    {
        if (!HasGun || !EquippedGun.CanUnequip)
        {
            return;
        }

        EquippedGun.transform.position = entityData.Center;
        EquippedGun.transform.parent = null;

        EquippedGun.Drop();

        guns.Remove(EquippedGun);

        if (HasGun)
        {
            if (CurrentGun >= guns.Count)
            {
                CurrentGun -= 1;
                SwitchGun(CurrentGun);
            }
        }
    }
}