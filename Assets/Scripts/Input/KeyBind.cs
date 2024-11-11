using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyBind
{
    [field: SerializeField] public PlayerAction Action {get; private set;}
    [SerializeField] private List<KeyCode> keys;
    [SerializeField] private bool canHold = false;

    public bool PressingKeys()
    {
        for (int key = 0; key < keys.Count; key++)
        {
            if (canHold)
            {
                if (!Input.GetKey(keys[key]))
                {
                    return false;
                }
            }
            else
            {
                if (!Input.GetKeyDown(keys[key]))
                {
                    return false;
                }
            }
        }
        
        return true;
    }
}