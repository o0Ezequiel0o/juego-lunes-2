using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Input Preset", menuName = "ScriptableObjects/Input/InputPreset", order = 1)]
public class InputPreset : ScriptableObject
{
    public Vector2 lookSensitivity = new Vector2(2f,2f);
    [Space]
    [SerializeField] public List<KeyBind> keyBindings;

    [HideInInspector] public Vector3 inputAxis2D = Vector3.zero;
}

public enum PlayerAction
{
    None,
    Interact,
    Grab,
    Drop,
    Reload,
    Ability1,
    Ability2,
    Ability3,
    SwitchToWeapon1,
    SwitchToWeapon2
};