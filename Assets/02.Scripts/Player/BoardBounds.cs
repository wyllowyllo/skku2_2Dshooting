using System;
using UnityEngine;

 public class BoardBounds:MonoBehaviour
{
    public static BoardBounds Instance;

    [SerializeField] private Transform _playerTransform;
    private float _boardLeftX;
    private float _boardRightX;
    private float _boardTopY;
    private float _boardBottomY;


    public float BoardLeftX { get => _boardLeftX; }
    public float BoardRightX { get => _boardRightX;  }
    public float BoardTopY { get => _boardTopY;  }
    public float BoardBottomY { get => _boardBottomY;}

    private void Awake()
    {
        Instance = this;
        InitBounds();
    }

    private void InitBounds()
    {
        Camera cam = Camera.main;
        float cameraHeight = cam.orthographicSize * 2;
        float cameraWidth = cameraHeight * cam.aspect;


        _boardLeftX = -cameraWidth / 2;
        _boardRightX = cameraWidth / 2;

        _boardBottomY = -cameraHeight / 2;
        _boardTopY = _boardBottomY + (cameraHeight / 2);
    }

    public Vector2 MoveClamp(Vector2 newPosition)
    {

        //움직임 범위 제한

        /*
          return new Vector2(Mathf.Clamp(newPosition.x, _boardLeftX, _boardRightX),
            Mathf.Clamp(newPosition.y, _boardBottomY, _boardTopY));
        */

        //바운더리 넘을 경우 반대 위치로 reposition

        Vector2 modifiedPos = newPosition;
        if(_boardLeftX-_playerTransform.localScale.x/2> newPosition.x)
        {
            modifiedPos.x = _boardRightX + _playerTransform.transform.localScale.x/2;
        }
        else if(_boardRightX+_playerTransform.localScale.x/2< newPosition.x)
        {
            modifiedPos.x = _boardLeftX - _playerTransform.localScale.x/2;
        }

        modifiedPos.y = Mathf.Clamp(newPosition.y, _boardBottomY + _playerTransform.localScale.y / 2, _boardTopY- _playerTransform.localScale.y / 2);
       
        return modifiedPos;
    }

    
}