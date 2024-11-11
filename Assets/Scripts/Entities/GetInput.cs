using UnityEngine;

public class GetInput : EntityComponent
{
    [SerializeField] private InputPreset inputPreset;

    public PlayerAction CurrentAction { get; private set; }

    private bool mouseLeftDown = false;
    private bool mouseLeftHold = false;

    public void Update()
    {
        inputPreset.inputAxis2D.x = Input.GetAxisRaw("Horizontal");
        inputPreset.inputAxis2D.y = Input.GetAxisRaw("Vertical");

        CurrentAction = PlayerAction.None;

        for (int i = 0; i < inputPreset.keyBindings.Count; i++)
        {
            if (inputPreset.keyBindings[i].PressingKeys())
            {
                CurrentAction = inputPreset.keyBindings[i].Action;
                break;
            }
        }

        mouseLeftDown = Input.GetMouseButtonDown(0);
        mouseLeftHold = Input.GetMouseButton(0);

        ProcessInput();
    }

    void ProcessInput()
    {
        switch(CurrentAction)
        {
            case PlayerAction.Grab:
                entityData.grabWeapon?.Invoke();
                break;
            case PlayerAction.Drop:
                entityData.dropWeapon?.Invoke();
                break;
            case PlayerAction.Reload:
                entityData.reload?.Invoke();
                break;
            case PlayerAction.Ability1:
                entityData.useAbility?.Invoke(0);
                break;
            case PlayerAction.Ability2:
                entityData.useAbility?.Invoke(1);
                break;
            case PlayerAction.Ability3:
                entityData.useAbility?.Invoke(2);
                break;
            case PlayerAction.SwitchToWeapon1:
                entityData.switchWeapon?.Invoke(0);
                break;
            case PlayerAction.SwitchToWeapon2:
                entityData.switchWeapon?.Invoke(1);
                break;
        }

        if (mouseLeftDown)
        {
            entityData.fire?.Invoke(false);
        }
        else if (mouseLeftHold)
        {
            entityData.fire?.Invoke(true);
        }

        entityData.moveDirection = inputPreset.inputAxis2D;
    }

    public void ChangePreset(InputPreset newInput)
    {
        inputPreset = newInput;
    }

    public Vector3 GetInputAxis()
    {
        return inputPreset.inputAxis2D;
    }
}