using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Boss : MonoBehaviour
{
    [SerializeField] private float _moveDuration = 1.5f;
    [SerializeField] private float _skillCoolTime = 3f;

    private float _bossSizeFactor = 1.5f;
    private Vector3 _bossPosition;

    private float _skillTimer = 0f;
    private void Awake()
    {
        Vector3 spawnPosition = transform.position;

        float targetY = BoardBounds.Instance.BoardTopY - (transform.localScale.y * _bossSizeFactor);
        _bossPosition = new Vector3(spawnPosition.x, targetY, spawnPosition.z);
    }
    private void Start()
    {
        StartCoroutine(Appear());
    }

    private void Update()
    {
        _skillTimer += Time.deltaTime;
        if (_skillTimer >= _skillCoolTime)
        {

            _skillTimer = 0f;
        }
    }
    private IEnumerator Appear()
    {
        float timer = 0f;

        Vector3 startPos = transform.position;
        Vector3 targetPos = _bossPosition;

        while (timer <= _moveDuration)
        {
            timer += Time.deltaTime;

            float t = timer / _moveDuration;
            transform.position = Vector3.Lerp(startPos, targetPos, t);

            yield return null;
        }

        transform.position = targetPos;
        
    }

    private void CycloneShot()
    {

    }
}
