using UnityEngine;
using UnityEngine.Rendering;
public class PlayerMove : MonoBehaviour
{

    //목표
    // 키보드 입력에 따라 방향을 구하고 그 방향으로 이동시키고 싶다

    //1. 키보드 입력

    //2. 방향 구하는 방법

    //3. 이동

    //필요 속성
    [SerializeField] private float _speed=3;
    [SerializeField] private float _maxSpeed = 10;
    [SerializeField] private float _minSpeed = 1;


    private Vector2 originPos = Vector2.zero;

    private void Start()
    {
        originPos = transform.position;

    }
    private void Update()
    {
        ModifySpeed();
        MovePlayer();
    }

    void MovePlayer()
    {
        // 1. 키보드 입력을 감지한다.
        // 유니티에서는 Input이라고 하는 모듈이 입력에 관한 모든것을 담당한다.(키보드,마우스, 리모컨, 콘솔 등...)
        /* float h = Input.GetAxis("Horizontal"); //수평 입력에 대한 값을 -1~0~1 로  가져온다 -> 서서히 부드럽게 변함(가속이 있음. inputmanager의 sensitivity 값에 따라 달라짐) 
         float v = Input.GetAxis("Vertical");//수직 입력에 대한 값을 -1~0~1 로  가져온다 -> 서서히 부드럽게 변함(가속이 있음. inputmanager의 sensitivity 값에 따라 달라짐) 
 */
        float h = Input.GetAxisRaw("Horizontal"); //수평 입력에 대한 값을 -1,0,1 로  가져온다
        float v = Input.GetAxisRaw("Vertical");//수직 입력에 대한 값을 -1,0,1 로  가져온다
        bool r= Input.GetKey(KeyCode.R);
        bool onDash = Input.GetKey(KeyCode.LeftShift);

        //2. 입력으로부터 방향을 구한다
        Vector2 direction = new Vector2(h, v);

        //방향을 크기 1로 만드는 정규화를 한다.
        direction.Normalize(); // or direction = direction.normalized;


        //3. 그 뱡향으로 이동을 한다.
        Vector2 position = transform.position;

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

        float additionalSpeed = onDash ? 2f : 1f;

        Vector2 newPosition = position + direction * _speed*additionalSpeed * Time.deltaTime; //새로운 위치
        newPosition = BoardBounds.Instance.MoveClamp(newPosition);
        

        //원점으로 이동
        if (r)
        {
     
            Vector2 dirVec= (originPos - newPosition).normalized;
            transform.Translate(dirVec* _speed*Time.deltaTime);

        }
        else
            transform.position = newPosition;
    }
    void ModifySpeed()
    {
        bool speedUp=Input.GetKeyDown(KeyCode.Q);
        bool speedDown=Input.GetKeyDown(KeyCode.E);
      


        if (speedUp)
            _speed++;
            
        if (speedDown)
            _speed --;

      

        _speed =Mathf.Clamp(_speed, _minSpeed, _maxSpeed);
    }

    
}
