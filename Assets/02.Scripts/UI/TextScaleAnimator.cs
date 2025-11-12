using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TextScaleAnimator : MonoBehaviour
{
    [SerializeField] private float _upScaleFactor;
    [SerializeField] private float _scaleDuration;

    private Text _targetText;
    
    private Vector3 _originScale = Vector3.one;
    private float _timer = 0f;
    private Coroutine _prevCoroutine = null ;

    private void Start()
    {
        _targetText = GetComponent<Text>();

        if (_targetText != null)
        {
            _originScale = _targetText.transform.localScale;
        }
       
    }

    public void PlayScaleAnimation()
    {
        if (_targetText == null) return;
        
        _timer = 0f;

        if (_prevCoroutine != null)
        {
            StopCoroutine(_prevCoroutine);
            _targetText.transform.localScale = _originScale;
        }
        _prevCoroutine = StartCoroutine(ScaleAnimationRoutine());
    }

    private IEnumerator ScaleAnimationRoutine()
    {
        Vector3 originScale = _targetText.transform.localScale;
        Vector3 targetScale = _targetText.transform.localScale*_upScaleFactor;
        
        // 업스케일
        while (_timer < _scaleDuration)
        {
            _timer += Time.deltaTime;
            _targetText.transform.localScale = Vector3.Lerp(originScale, targetScale, _timer / _scaleDuration);
            yield return null;
        }
        
        // 다시 원래 크기로
        _timer = 0f;
        while (_timer < _scaleDuration)
        {
            _timer += Time.deltaTime;
            _targetText.transform.localScale = Vector3.Lerp(targetScale, originScale, _timer / _scaleDuration);
            yield return null;
        }
        
        _targetText.transform.localScale = originScale; 
        _prevCoroutine = null;
    }

}
