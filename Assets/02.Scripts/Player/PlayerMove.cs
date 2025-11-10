using UnityEngine;
using UnityEngine.Rendering;

public enum EControlMode
{
    Ctrl_AUTO = 1,
    Ctrl_MANUAL = 2,
}
public class PlayerMove : MonoBehaviour
{

    //목표
    // 키보드 입력에 따라 방향을 구하고 그 방향으로 이동시키고 싶다

    //1. 키보드 입력

    //2. 방향 구하는 방법

    //3. 이동

    [Header(("컨트롤 모드"))]
    [SerializeField] private EControlMode _controlMode = EControlMode.Ctrl_MANUAL;
    
    [Header("필요 속성")]
    [SerializeField] private float _speed=3;
    [SerializeField] private float _maxSpeed = 10;
    [SerializeField] private float _minSpeed = 1;
    [SerializeField] private float _dashSpeed = 2f;
    [SerializeField] private float _minInterceptMoveRange=1f; //요격기동 최소범위

    [Header("시작위치")]
    private Vector2 originPosition = Vector2.zero;
    private Vector2 recordStartPos = Vector2.zero;

    [Header("플레이어 입력 처리 모듈")]
    private InputController _input;

    [Header("녹화&리플레이 객체")]
    private InputRecorder _inputRecorder;
    private InputReplayer _inputReplayer;

    [Header("스캐너 모듈")]
    private Scanner _scanner;
    
    [Header("플래그 변수")]
    private bool _isRecording = false;
    private bool _isReplaying = false;
   

    private int _currentHealthCnt;
    private Vector2 _prevPosition=Vector2.zero;
    private const float _marginOfPosDiff = 1e-9f;

    private void Start()
    {
        originPosition = transform.position;
       
        _scanner=GetComponent<Scanner>();
        _input=GetComponent<InputController>();
        
        if(_input==null) _input=gameObject.AddComponent<InputController>();
        if(_scanner==null) _scanner=gameObject.AddComponent<Scanner>();
    }
    private void Update()
    {
        //녹화/리플레이 처리
        /*_inputRecorder?.Tick();
        _inputReplayer?.Tick();*/

        //컨트롤 모드 설정
        SwitchAtkMode();
        
        //움직임 적용
        //ModifySpeed();
        MovePlayer();
    }
    public void StartRecording()
    {
        /*if (_isRecording)   return;
          

        _isRecording = true;

        _input = new InputController();
        _inputRecorder = new InputRecorder(_input);
        _inputRecorder.StartRecording();

        recordStartPos = transform.position;*/
    }

    public void StartReplaying()
    {
        /*if (_isReplaying)  return;
           


        _isReplaying = true;

        transform.position = recordStartPos;

        _inputReplayer = new InputReplayer(_inputRecorder.Events);

        _inputReplayer.ReplayFinished.AddListener(ReplayingFinished);
        _inputReplayer.StartReplaying();

        _input = _inputReplayer;*/


    }


    public void SpeedUp(float speedIncrement)
    {
        _speed += speedIncrement;
        _speed = Mathf.Min(_speed, _maxSpeed);
    }

    private void MovePlayer()
    {
        if (_controlMode == EControlMode.Ctrl_AUTO)
        {
            AutoMove();
            return; 
        }

        bool onReturn = _input.ResetPosition;
        bool onDash= _input.Dash;


        /*
       //새로운 위치= 현재위치 +방향*속력*시간
       //새로운 위치 = 현재 위치 +속도 +시간

       ///Time.deltaTime: 이전 프레임으로부터 현재 프레임까지 시간이 얼마나 흘렀는지.. 나타내는 값 (1초 / fps 값과 비슷)

       //이동속도: 10
       //컴퓨터 1: 50fps : Update -> 초당 50번 실행 -> 10*50= 500
       //컴퓨터 2: 100fps : Update -> 초당 100번 실행 -> 10*100= 1000

       // -> Time.deltaTime을 곱해주면 두 값이 같아진다
       // (10*50= 500) * Time.deltaTime
       // (10*100= 1000) * Time.deltaTime

       //tip) 코딩 규칙 - -1,0,1 이 세개 빼고는 다 매직 넘버이므로 숫자 그대로 쓰지 말고 따로 변수로 빼야 한다.
       */

        //2. 입력으로부터 방향을 구한다
        Vector2 direction = _input.MoveInput;

        //방향을 크기 1로 만드는 정규화를 한다.
        direction.Normalize(); // or direction = direction.normalized;
        
        float additionalSpeed = onDash ? _dashSpeed : 1f;
        float finalSpeed = _speed * additionalSpeed;

        //3. 그 뱡향으로 이동을 한다.
        Vector2 position = transform.position;
        Vector2 newPosition = position + direction * finalSpeed * Time.deltaTime; //새로운 위치
        newPosition = BoardBounds.Instance.MoveClamp(newPosition);
        
        //원점으로 이동
        if (onReturn)
        {
            TranslateToOrigin(finalSpeed);
        }
        else
            transform.position = newPosition;
    }

    private void AutoMove()
    {
        if (_scanner == null) return;
        
        Transform targetTr = _scanner.NeareastTargetTr;
        if (targetTr == null) return;
        
        
        Vector2 myPos = transform.position;
        Vector2 targetPos = targetTr.position;
        float curDiff = Vector2.Distance(myPos, targetPos);

        Vector2 direction = Vector2.zero;
        
        if (curDiff < _minInterceptMoveRange)
        {
            //회피 기동
            direction = -(targetPos - myPos).normalized;
        }
        else
        {
            //요격 기동
            direction = (targetPos - myPos).normalized;
        }
        
        float autoMoveSpeed = _speed * _dashSpeed;
        Vector2 newPosition = myPos + direction * autoMoveSpeed * Time.deltaTime; //새로운 위치
        newPosition = BoardBounds.Instance.MoveClamp(newPosition);
            
        if(Vector2.Distance(newPosition, _prevPosition)>_marginOfPosDiff)  transform.position = newPosition;
            
       _prevPosition = newPosition;
    }
    private void SwitchAtkMode()
    {
        if(_input.AutoMode) _controlMode = EControlMode.Ctrl_AUTO;
        
        if(_input.ManualMode) _controlMode = EControlMode.Ctrl_MANUAL;
           
    }
    private void ModifySpeed()
    {
        /* bool speedUp=Input.GetKeyDown(KeyCode.Q);
         bool speedDown=Input.GetKeyDown(KeyCode.E);*/

         bool speedUp = _input.SpeedUp;
         bool speedDown = _input.SpeedDown;



        if (speedUp)
            _speed++;
            
        if (speedDown)
            _speed --;

      

        _speed = Mathf.Clamp(_speed, _minSpeed, _maxSpeed);
    }

    private void TranslateToOrigin(float speed)
    {
        Vector2 dirVec = (originPosition - (Vector2)transform.position).normalized;
        transform.Translate(dirVec * speed * Time.deltaTime);
    }

   
    private void ReplayingFinished()
    {

        _inputReplayer.ReplayFinished.RemoveListener(ReplayingFinished);

        _isRecording = false;
        _isReplaying = false;
        _inputReplayer = null;
        _inputRecorder = null;
        _input = new InputController();

    }

   
    
}