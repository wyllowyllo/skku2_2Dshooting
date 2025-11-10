using System.Numerics;
using Unity.Profiling;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
public interface IInputSource
{
 

    public Vector2 MoveInput { get; }

    public bool SpeedUp { get; }
    public bool SpeedDown { get; }
    public bool Dash { get; }
    public bool ResetPosition { get; }
    
    public bool AutoMode { get; }
    public bool ManualMode { get; }
}
