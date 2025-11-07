using UnityEngine;

public class BezierFly : MonoBehaviour
{
    [Header("대기 시간")]
    [SerializeField] private float _waitTime = 2f;

    [Header("곡선 조정 변수")]
    [SerializeField] private float _duration = 1.0f; // 날아가는데 소요되는 시간
    [SerializeField] private float _ctrlPointDistanceFromStart = 3f; //시작점으로부터의 거리 factor
    [SerializeField] private float _ctrlPointDistanceFromTarget = 3f; //목표점으로부터의 거리 factor
    [SerializeField] private float _randomXMin = -1f;
    [SerializeField] private float _randomXMax = 1f;
    [SerializeField] private float _randomYMin = -0.15f;
    [SerializeField] private float _randomYMax = 1f;

    private const string _targetTag = "Player";

    private const int _bezierPointCnt = 4;
    private Transform _targetTransform;
    private float _timer = 0f;
    private float _currentTime=0f;

    private Vector2[] _pathPoints;

    private void Start()
    {
        _targetTransform = GameObject.FindWithTag(_targetTag)?.transform;
        _pathPoints = new Vector2[_bezierPointCnt];

        InitPathPoints();
    }
    private void Update()
    {
        

        _timer += Time.deltaTime;

        if (_timer < _waitTime || _targetTransform == null) return;


        _pathPoints[3] = _targetTransform.position;

        FlyToTaget();
    }

    private void InitPathPoints()
    {
        if (_targetTransform == null) return;



        _pathPoints[0] = transform.position;

        Vector3 offset =
            (transform.right * Random.Range(_randomXMin, _randomXMax) * _ctrlPointDistanceFromStart ) +
            (transform.up * Random.Range(_randomYMin, _randomYMax) * _ctrlPointDistanceFromStart);


        _pathPoints[1] = transform.position + offset;



        offset=
             (_targetTransform.right * Random.Range(_randomXMin, _randomXMax) * _ctrlPointDistanceFromTarget) +
            (_targetTransform.up * Random.Range(_randomYMin, _randomYMax) * _ctrlPointDistanceFromTarget);

        _pathPoints[2] = _targetTransform.position + offset;



        _pathPoints[3] = _targetTransform.position;
    }
    private void FlyToTaget()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > _duration) _currentTime = _duration;

        transform.position = new Vector3(
            BezierCurve(_pathPoints[0].x, _pathPoints[1].x, _pathPoints[2].x, _pathPoints[3].x),
            BezierCurve(_pathPoints[0].y, _pathPoints[1].y, _pathPoints[2].y, _pathPoints[3].y),
            0
        );
    }

    private float BezierCurve(float startPoint, float secondPoint, float thirdPoint, float endPoint)
    {
        // (0~1)의 값에 따라 베지어 곡선 값을 구하기 때문에, 비율에 따른 시간을 구했다.
        float t = _currentTime / _duration; // (현재 경과 시간 / 최대 시간)

       
        float ab = Mathf.Lerp(startPoint, secondPoint, t);
        float bc = Mathf.Lerp(secondPoint, thirdPoint, t);
        float cd = Mathf.Lerp(thirdPoint, endPoint, t);

        float abbc = Mathf.Lerp(ab, bc, t);
        float bccd = Mathf.Lerp(bc, cd, t);

        return Mathf.Lerp(abbc, bccd, t);
    }
}