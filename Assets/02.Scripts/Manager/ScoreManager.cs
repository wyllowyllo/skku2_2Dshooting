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
    
    // - 현재 점수를 기억할 변수
    private float _currentScore = 0;

    private void Start()
    {
        Refresh();

    }

    public void AddScore(in int score)
    {
        if (score <= 0) return;

       
        _currentScore += score;
        Refresh();
    }

    private void Refresh()
    {
        _currentScoreTextUI.text = string.Format("현재 점수 : {0} 점", _currentScore) ;
    }
}
