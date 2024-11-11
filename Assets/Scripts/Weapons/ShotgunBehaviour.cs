using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBehaviour : Gun
{
    [SerializeField] private List<Transform> barrels;

    public override void Use(bool holdingButton)
    {
        if (currentState != WeaponState.Idle)
        {
            return;
        }
        
        switch (CanFire(holdingButton))
        {
            case CanFireState.Ready:
                Fire();
                break;

            case CanFireState.Empty:
                if (!holdingButton)
                {
                    emptySound.PlayAtSource(audioSource);
                }
                break;

            case CanFireState.Busy:
                //nothing
                break;
        }
    }

    protected override void OnUpdate() {}

    void Fire()
    {
        ChangeState(WeaponState.Firing);

        for (int i = 0; i < barrels.Count; i++)
        {
            CreateBullet(barrels[i].transform, projectile);
        }

        fireSound.Play(transform.position);

        EnableMuzzleFlash();
        
        Ammo -= 1;

        timeSinceFired = 0f;
        ChangeState(WeaponState.Idle);
    }
}