using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 목표 : 적을 죽일 때마다 점수를 올리고, 현재 점수를 UI에 표시하고 싶다
    
    // 필요 속성
    // - 현재 점수 UI (Text 컴포넌트)
    // 규칙 : UI요소는 항상 변수명 뒤에 UI 붙인다.
    // SerializeField : 필드를 유니티가 이해할 수 있게끔 직렬화 한다.
    [SerializeField] private Text _currentScoreTextUI;
    [SerializeField] private Text _bestScoreTextUI;
    
    // - 현재 점수를 기억할 변수
    private int _currentScore = 0;
    private int _bestScore = 0;
    private const string ScoreKey = "Score";
  
    private void Start()
    {
        Load();
        
        Refresh();
    }
    

    // 1. 하나의 메서드는 한 가지 일만 잘 하면 된다.
    // 2. 추상화 수준을 똑같이 해라
    
    public void AddScore(in int score)
    {
        if (score <= 0) return;

       
        _currentScore += score;
        if(_bestScore < _currentScore) _bestScore = _currentScore;
        
        Refresh();
        
        Save();
    }

    private void Refresh()
    {
        _currentScoreTextUI.text = $"현재 점수 : {_currentScore} 점";
        _bestScoreTextUI.text = $"최고 점수 : {_bestScore} 점";
    }

    private void Save()
    {
        PlayerPrefs.SetInt(ScoreKey, _bestScore);
    }

    private void Load()
    {
        _currentScore = 0;
        _bestScore = PlayerPrefs.GetInt(ScoreKey, 0);
    }
 
}
