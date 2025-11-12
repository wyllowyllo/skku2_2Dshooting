using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class TextScaleAnimator : MonoBehaviour
{
    [SerializeField] private float _upScaleFactor;
    [SerializeField] private float _scaleDuration;

    private Text _targetText;
    
    private Vector3 _originScale = Vector3.one;
    private Tween _prevTween = null ;

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
        
        _prevTween?.Kill();
        
        _targetText.transform.localScale = _originScale;

        _targetText.transform
            .DOScale(_originScale * _upScaleFactor, _scaleDuration)
            .SetEase(Ease.OutQuad) //초반에 빠르게 커지고, 끝에는 좀 느리게 커짐
            .OnComplete(() =>
                {
                    _targetText.transform
                        .DOScale(_originScale, _scaleDuration)
                        .SetEase(Ease.InQuad); //초반에 느리게 작아지고, 끝에는 좀 빠르게 작아짐
                }
            );
    }
    

}
