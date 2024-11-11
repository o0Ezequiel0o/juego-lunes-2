public class DefaultGunBehaviour : Gun
{
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
        CreateBullet(mainBarrel.transform, projectile);

        fireSound.Play(mainBarrel.position);
        EnableMuzzleFlash();
        
        Ammo -= 1;

        timeSinceFired = 0f;
        ChangeState(WeaponState.Idle);
    }
}