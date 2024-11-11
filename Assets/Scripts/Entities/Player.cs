using UnityEngine;

[RequireComponent(typeof(Entity))]
public class Player : MonoBehaviour
{
    [SerializeField] private Entity entity;
}