using Unity.Profiling;

public interface IInputSource
{
    public float Horizontal { get; }
    public float Vertical { get; }


    public bool SpeedUp { get; }
    public bool SpeedDown { get; }
    public bool Dasth { get; }
    public bool ResetPosition { get; }
}
