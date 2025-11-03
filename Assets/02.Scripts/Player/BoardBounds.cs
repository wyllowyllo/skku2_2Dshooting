using System;
using UnityEngine;

 public class BoardBounds:MonoBehaviour
{
    public static BoardBounds Instance;

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
        return new Vector2(Mathf.Clamp(newPosition.x, _boardLeftX, _boardRightX),
            Mathf.Clamp(newPosition.y, _boardBottomY, _boardTopY));
    }
}