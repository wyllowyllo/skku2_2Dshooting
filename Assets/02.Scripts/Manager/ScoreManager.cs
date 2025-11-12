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
    private int _currentScore = 0;

    private void Start()
    {
        TestLoad();
        Refresh();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            TestSave();
        }
    }

    public void AddScore(in int score)
    {
        if (score <= 0) return;

       
        _currentScore += score;
        Refresh();
    }

    private void Refresh()
    {
        _currentScoreTextUI.text = $"현재 점수 : {_currentScore} 점";
    }

    private void TestSave()
    {
        // 유니티에서는 값을 지정할 때 'PlayerPrefs' 모듈을 씁니다.
        // 저장 가능한 자료형은 : int, float , string
        // 저장을 할 때는 저장할 이름(key)와 값(value) 이 두 형태로 저장을 한다.
        
        // 저장 : set
        // 로드 : get
        
        
        PlayerPrefs.SetInt("score", _currentScore);
        PlayerPrefs.SetString("name", "전민관");
        Debug.Log("저장되었습니다.");
    }

    private void TestLoad()
    {
        if (PlayerPrefs.HasKey("name"))
        {
            int score = PlayerPrefs.GetInt("score");
            string name = PlayerPrefs.GetString("name", "전민관"); // default 인자
        
            Debug.Log($"{name} : {score}");
        }
        
    }
}
