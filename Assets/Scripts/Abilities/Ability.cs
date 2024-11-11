public abstract class Ability
{
    public abstract float CooldownTime {get;}

    public float CooldownTimer {get; protected set;} = 0f;
    
    public bool IsActive {get; protected set;} = false;

    public virtual void Initialize() {}

    public virtual void Activate() {}

    public virtual void Update() {}

    public virtual void Deactivate() {}
}