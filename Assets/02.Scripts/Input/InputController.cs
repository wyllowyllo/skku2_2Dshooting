using UnityEngine.Windows;
using UnityEngine;
using Input=UnityEngine.Input;

public class InputController : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick;
   public Vector2 MoveInput
    {
        get
        {
            /*float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");*/
            
            float h = _joystick.Horizontal;
            float v = _joystick.Vertical;
            return new Vector2(h, v).normalized;
        }
    }

    public bool SpeedUp => Input.GetKeyDown(KeyCode.Q);

    public bool SpeedDown => Input.GetKeyDown(KeyCode.E);

    public bool Dash => Input.GetKey(KeyCode.LeftShift);
    
    public bool Fire => Input.GetKey(KeyCode.Space);

    public bool ResetPosition => Input.GetKey(KeyCode.R);
    
    public bool AutoMode => Input.GetKey(KeyCode.Alpha1);
    
    public bool ManualMode => Input.GetKey(KeyCode.Alpha2);
    
    public bool Boom => Input.GetKeyDown(KeyCode.Alpha3);
}
