using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


class InputReplayer : IInputSource
{
    public UnityEvent ReplayFinished;

    private IReadOnlyList<InputEvent> _events;
    private int _currentEventIndex;
    private float _startTime;
    public Vector2 MoveInput { get; private set; }
    public bool SpeedUp { get; private set; }
    public bool SpeedDown { get; private set; }
    public bool Dash { get; private set; }
    public bool ResetPosition { get; private set; }

    //생성자
    public InputReplayer(IReadOnlyList<InputEvent> events)
    {
        _events = events;
    }
    public void StartReplaying()
    {
        _currentEventIndex = 0;
        _startTime = Time.time;
        //초기 상태 설정
        if (_events.Count > 0 && _events[0].EventType == InputEventType.Move)
        {
            MoveInput = _events[0].InputVec;
            _currentEventIndex++;
        }
    }
    public void Tick()
    {
        if (_currentEventIndex >= _events.Count)
            return;
        float elapsedTime = Time.time - _startTime;
        while (_currentEventIndex < _events.Count && _events[_currentEventIndex].Time <= elapsedTime)
        {
            var inputEvent = _events[_currentEventIndex];
            switch (inputEvent.EventType)
            {
                case InputEventType.Move:
                    MoveInput = inputEvent.InputVec;
                    break;
                case InputEventType.SpeedUp:
                    SpeedUp = inputEvent.boolValue;
                    break;
                case InputEventType.SpeedDown:
                    SpeedDown = inputEvent.boolValue;
                    break;
                case InputEventType.Dash:
                    Dash = inputEvent.boolValue;
                    break;
                case InputEventType.ResetPosition:
                    ResetPosition = inputEvent.boolValue;
                    break;
            }
            _currentEventIndex++;
        }

        ReplayFinished?.Invoke();
    }
}