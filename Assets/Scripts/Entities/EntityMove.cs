public class EntityMove : EntityComponent
{
    void Start()
    {
        if (entityData.rb == null)
        {
            enabled = false;
        }
    }

    void Update()
    {
        entityData.rb.velocity = entityData.moveDirection * entityData.MoveSpeed;
    }
}