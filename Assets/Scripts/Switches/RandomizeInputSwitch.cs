using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class RandomizeInputSwitch : Switch
{

    private readonly string[] keyboardPaths = { "<Keyboard>/w", "<Keyboard>/s", "<Keyboard>/a", "<Keyboard>/d" };
    private string[] randomizedKeyboardBindings = new string[4];
    private bool[] usedIndexes;
    private readonly string[] gamePadPaths = { "<Gamepad>/leftStick/up", "<Gamepad>/leftstick/down", "<Gamepad>/leftstick/left", "<Gamepad>/leftstick/right" };
    private string[] randomizedGamepadBindings = new string[4];

    private System.Random rand;


    [field: SerializeField, ReadOnly]
    public bool State { get; protected set; }

    public UnityEvent TurnOn;
    public UnityEvent TurnOff;

    [ContextMenu("Use")]
    public override void Use()
    {
        if (State)
        {
            Off();
        }
        else
        {
            On();
        }
    }

    public void On()
    {
        TurnOn.Invoke();
        State = true;
    }

    public void Off()
    {
        TurnOff.Invoke();
        State = false;
    }

    private void Start()
    {
        rand = new System.Random();
        usedIndexes = new bool[4];
        for (int i = 0; i < 4; ++i)
        {
            usedIndexes[i] = true;
        }
    }


    public void Randomize()
    {
        bool flag = true;
        for (int i = 0; i < 4; ++i)
        {
            flag = true;
            while (flag)
            {
                int rtemp = rand.Next() % 4;
                if(usedIndexes[rtemp])
                {
                    randomizedKeyboardBindings[i] = keyboardPaths[rtemp];
                    randomizedGamepadBindings[i] = gamePadPaths[rtemp];
                    usedIndexes[rtemp] = false;
                    flag = false;
                }
            };

        }
        BindRandom();
    }

    private void BindRandom()
    {
        InputAction temp = LevelManager.Instance.input.Gameplay.Move;
        int inputKeyboardIndex = 0;
        int inputGamePadIndex = 0;
        for (int i = 0; i < temp.bindings.Count; ++i)
        {
            if (temp.bindings[i].path.Contains("<Keyboard>"))
            {
                InputBinding newBinding = temp.bindings[i];
                newBinding.overridePath = randomizedKeyboardBindings[inputKeyboardIndex];
                inputKeyboardIndex++;
                InputActionRebindingExtensions.ApplyBindingOverride(temp, i, newBinding);
            }
            else if (temp.bindings[i].path.Contains("<Gamepad>"))
            {
                InputBinding newBinding = temp.bindings[i];
                newBinding.overridePath = randomizedGamepadBindings[inputGamePadIndex];
                inputGamePadIndex++;
                InputActionRebindingExtensions.ApplyBindingOverride(temp, i, newBinding);
            }
        }
    }

    public void ReloadBindings()
    {
        Unbind();
    }

    private void Unbind()
    {
        InputAction temp = LevelManager.Instance.input.Gameplay.Move;
        InputActionRebindingExtensions.RemoveAllBindingOverrides(temp);
        for (int i = 0; i < 4; ++i)
        {
            usedIndexes[i] = true;
        }
    }
}
