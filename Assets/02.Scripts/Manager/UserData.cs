using System;
using UnityEngine;

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