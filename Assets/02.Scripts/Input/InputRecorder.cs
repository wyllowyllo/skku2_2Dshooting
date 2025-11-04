
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;


public enum InputEventType
{
    Move,
    SpeedUp,
    SpeedDown,
    Dash,
    ResetPosition,
}

[Serializable]
public struct InputEvent
{
    public InputEventType EventType;
    public float Time;
    public Vector2 InputVec;
    public bool boolValue;
}

class InputRecorder
{
    private readonly IInputSource _inputSource;
    private readonly List<InputEvent> _events = new List<InputEvent>();


    //이전 입력 상태 저장용 변수
    private float _startTime;
    private Vector2 _prevMoveInput;
    
    private bool _prevSpeedUp;
    private bool _prevSpeedDown;
    private bool _prevDash;
    private bool _prevResetPosition;


    //Getter
    public IReadOnlyList<InputEvent> Events => _events;
    //생성자
    public InputRecorder(IInputSource inputSource) => _inputSource = inputSource;

    public void StartRecording()
    {
        _events.Clear();
        _startTime = Time.time;
        _prevMoveInput= _inputSource.MoveInput;
       

        //시작 상태 기록
        _events.Add(new InputEvent
        {
            EventType = InputEventType.Move,
            Time = 0f,
            InputVec = _prevMoveInput,
            boolValue = false
        });
    }

    public void Tick()
    {
       float t=Time.time - _startTime;

        Vector2 moveInput = _inputSource.MoveInput;
        if(moveInput!=_prevMoveInput)
        {
            _events.Add(new InputEvent
            {
                EventType = InputEventType.Move,
                Time = t,
                InputVec = moveInput
            });
            _prevMoveInput = moveInput;
        }

        if(_inputSource.SpeedUp)
            _events.Add(new InputEvent { InputVec = Vector2.zero, EventType = InputEventType.SpeedUp, Time = t, boolValue = true });
        if (_inputSource.SpeedDown)
            _events.Add(new InputEvent { InputVec = Vector2.zero, EventType = InputEventType.SpeedDown, Time = t, boolValue = true });
        if (_inputSource.Dash)
            _events.Add(new InputEvent { InputVec = Vector2.zero, EventType = InputEventType.Dash, Time = t, boolValue = true });
        if (_inputSource.ResetPosition)
            _events.Add(new InputEvent { InputVec = Vector2.zero, EventType = InputEventType.ResetPosition, Time = t, boolValue = true });

    }

}
