using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class RandomizeInputSwitch : OnOffSwitch
{

    private NewInput input;

    private string[] keyboardPaths = { "<Keyboard>/w", "<Keyboard>/s", "<Keyboard>/a", "<Keyboard>/d" };
    private string[] randomizedKeyboardBindings = new string[4];
    private bool[] usedIndexes;
    private string[] gamePadPaths = { "<Gamepad>/leftStick/up", "<Gamepad>/leftstick/down", "<Gamepad>/leftstick/left", "<Gamepad>/leftstick/right" };
    private string[] randomizedGamepadBindings = new string[4];

    private void Start()
    {
        usedIndexes = new bool[4];
        input = GameManager.Instance.GameInput;
        for(int i = 0; i < 4; ++i)
        {
            usedIndexes[i] = true;
        }
        Randomize();
    }


    public void Randomize()
    {
        System.Random rand = new System.Random();
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
        InputAction temp = input.Gameplay.Move;
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

    public void Unbind()
    {
        InputAction temp = input.Gameplay.Move;
        int inputKeyboardIndex = 0;
        int inputGamePadIndex = 0;
        for (int i = 0; i < temp.bindings.Count; ++i)
        {
            if (temp.bindings[i].path.Contains("<Keyboard>"))
            {
                InputBinding newBinding = temp.bindings[i];
                newBinding.overridePath = keyboardPaths[inputKeyboardIndex];
                inputKeyboardIndex++;
                InputActionRebindingExtensions.ApplyBindingOverride(temp, i, newBinding);
            }
            else if (temp.bindings[i].path.Contains("<Gamepad>"))
            {
                InputBinding newBinding = temp.bindings[i];
                newBinding.overridePath = gamePadPaths[inputGamePadIndex];
                inputGamePadIndex++;
                InputActionRebindingExtensions.ApplyBindingOverride(temp, i, newBinding);
            }
        }
    }
}
