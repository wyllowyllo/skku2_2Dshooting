using UnityEngine.Windows;
using UnityEngine;
using Input=UnityEngine.Input;

public class InputController : IInputSource
{
    public float Horizontal
    {
        get
        {
            float h = Input.GetAxisRaw("Horizontal");
            return h;
        }
    }

    public float Vertical
    {
        get
        {
            float v = Input.GetAxisRaw("Vertical");
            return v;
        }
    }

    public bool SpeedUp => Input.GetKeyDown(KeyCode.Q);

    public bool SpeedDown => Input.GetKeyDown(KeyCode.E);

    public bool Dasth => throw new System.NotImplementedException();

    public bool ResetPosition => throw new System.NotImplementedException();
}
