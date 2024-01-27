using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class PlayerInputManager : SceneSingleton<PlayerInputManager>
{
    public bool button1;
    public bool button2;
    public bool button3;
    public bool button4;

    protected override void Awake()
    {
        base.Awake();
    }

    // private void Update()
    // {
    //     if (button1)
    //     {
    //         print($"is press button 1");
    //     }

    //     if (button2)
    //     {
    //         print($"is press button 2");
    //     }

    //     if (button3)
    //     {
    //         print($"is press button 3");
    //     }

    //     if (button4)
    //     {
    //         print($"is press button 4");
    //     }
    // }

    private void LateUpdate()
    {
        button1 = false;
        button2 = false;
        button3 = false;
        button4 = false;
    }

#if ENABLE_INPUT_SYSTEM
    public void OnButton1(InputValue value)
    {
        Button1Input(value.isPressed);
    }

    public void OnButton2(InputValue value)
    {
        Button2Input(value.isPressed);
    }

    public void OnButton3(InputValue value)
    {
        Button3Input(value.isPressed);
    }

    public void OnButton4(InputValue value)
    {
        Button4Input(value.isPressed);
    }
#endif

    public void Button1Input(bool newButtonState)
    {
        button1 = newButtonState;
    }

    public void Button2Input(bool newButtonState)
    {
        button2 = newButtonState;
    }

    public void Button3Input(bool newButtonState)
    {
        button3 = newButtonState;
    }

    public void Button4Input(bool newButtonState)
    {
        button4 = newButtonState;
    }
}
