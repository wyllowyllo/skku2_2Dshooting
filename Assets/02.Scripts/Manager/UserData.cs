using System;
using UnityEngine;

/// <summary>
/// 유저 최고 점수 데이터
/// </summary>
[Serializable]
public class UserData
{
    [SerializeField] private int _bestScore;
    public int BestScore
    {
        get { return _bestScore; }
        set { _bestScore = value; }
    }
    
    
}