using UnityEngine;
using UnityEngine.Rendering;
public class PlayerMove : MonoBehaviour
{

    //목표
    // 키보드 입력에 따라 방향을 구하고 그 방향으로 이동시키고 싶다

    //1. 키보드 입력

    //2. 방향 구하는 방법

    //3. 이동

    [Header("필요 속성")]
    [SerializeField] private float _speed=3;
    [SerializeField] private float _maxSpeed = 10;
    [SerializeField] private float _minSpeed = 1;
    [SerializeField] private int _totalHealthCnt = 3;

    [Header("시작위치")]
    private Vector2 originPosition = Vector2.zero;
    private Vector2 recordStartPos = Vector2.zero;

    [Header("플레이어 입력 처리 모듈")]
    private IInputSource _input=new InputController();

    [Header("녹화&리플레이 객체")]
    private InputRecorder _inputRecorder;
    private InputReplayer _inputReplayer;

    [Header("플래그 변수")]
    private bool _isRecording = false;
    private bool _isReplaying = false;
    private bool _isDead = false;

    private int _currentHealthCnt;

    private void Start()
    {
        originPosition = transform.position;
        _currentHealthCnt = _totalHealthCnt;

    }
    private void Update()
    {
        //입력 처리 (커넥터 연결)
        _inputRecorder?.Tick();
        _inputReplayer?.Tick();

        //움직임 적용
        ModifySpeed();
        MovePlayer();
    }

    private void MovePlayer()
    {
        /*
      
        // 1. 키보드 입력을 감지한다.
        // 유니티에서는 Input이라고 하는 모듈이 입력에 관한 모든것을 담당한다.(키보드,마우스, 리모컨, 콘솔 등...)
        float h = Input.GetAxis("Horizontal"); //수평 입력에 대한 값을 -1~0~1 로  가져온다 -> 서서히 부드럽게 변함(가속이 있음. inputmanager의 sensitivity 값에 따라 달라짐) 
        float v = Input.GetAxis("Vertical");//수직 입력에 대한 값을 -1~0~1 로  가져온다 -> 서서히 부드럽게 변함(가속이 있음. inputmanager의 sensitivity 값에 따라 달라짐) 
        float h = Input.GetAxisRaw("Horizontal"); //수평 입력에 대한 값을 -1,0,1 로  가져온다
         float v = Input.GetAxisRaw("Vertical");//수직 입력에 대한 값을 -1,0,1 로  가져온다
         bool r= Input.GetKey(KeyCode.R);
         bool onDash = Input.GetKey(KeyCode.LeftShift);

        float h= _input.Horizontal;
        float v= _input.Vertical;


          */
        

        bool onReturn = _input.ResetPosition;
        bool onDash= _input.Dash;



        //2. 입력으로부터 방향을 구한다
        //Vector2 direction = new Vector2(h, v);
        Vector2 direction = _input.MoveInput;

        //방향을 크기 1로 만드는 정규화를 한다.
        direction.Normalize(); // or direction = direction.normalized;


       
        //3. 그 뱡향으로 이동을 한다.
        Vector2 position = transform.position;

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

        float additionalSpeed = onDash ? 2f : 1f;
        float finalSpeed = _speed * additionalSpeed;

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
    private void ModifySpeed()
    {
        /* bool speedUp=Input.GetKeyDown(KeyCode.Q);
         bool speedDown=Input.GetKeyDown(KeyCode.E);*/

         bool speedUp=_input.SpeedUp;
         bool speedDown= _input.SpeedDown;



        if (speedUp)
            _speed++;
            
        if (speedDown)
            _speed --;

      

        _speed =Mathf.Clamp(_speed, _minSpeed, _maxSpeed);
    }

    private void TranslateToOrigin(float speed)
    {
        Vector2 dirVec = (originPosition - (Vector2)transform.position).normalized;
        transform.Translate(dirVec * speed * Time.deltaTime);
    }

    public void StartRecording()
    {
        if (_isRecording)
            return;

        _isRecording = true;

        _input = new InputController();
        _inputRecorder = new InputRecorder(_input);
        _inputRecorder.StartRecording();

        recordStartPos= transform.position;
    }

    public void StartReplaying()
    {
        if (_isReplaying)
            return;

        
        _isReplaying = true;

        transform.position = recordStartPos;

        _inputReplayer = new InputReplayer(_inputRecorder.Events);

        _inputReplayer.ReplayFinished.AddListener(ReplayingFinished);
        _inputReplayer.StartReplaying();

        _input = _inputReplayer;

        
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Enemy") || _isDead) return;


        _currentHealthCnt--;
        if (_currentHealthCnt <= 0)
        {
            Dead();
        }

    }

    private void Dead()
    {
        _isDead= true;
        Destroy(gameObject);
    }
}
