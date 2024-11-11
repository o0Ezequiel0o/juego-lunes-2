using UnityEngine;

public class Physics : EntityComponent
{
    public void LateUpdate()
    {
        entityData.forces -= entityData.drag * Time.deltaTime * entityData.forces;

        if (Mathf.Abs(entityData.forces.x) <= 0.01f && Mathf.Abs(entityData.forces.y) <= 0.01f)
        {
            entityData.forces = Vector2.zero;
        }
        else
        {
            entityData.rb.velocity += entityData.forces;
        }
    }
}