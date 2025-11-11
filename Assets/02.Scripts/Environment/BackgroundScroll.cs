using System;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private Material _backgroundMaterial;
    [SerializeField]private float _scrollSpeed=1.0f;

    private void Start()
    {
        _backgroundMaterial = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        //방향을 구한다
        Vector2 direction = Vector2.up;
        
        //움직인다
        _backgroundMaterial.mainTextureOffset += direction*_scrollSpeed*Time.deltaTime;
    }
}
