using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    [Header("총알 프리펩")] 
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private GameObject _subBulletPrefab;

    public GameObject MakeBullet(Vector3 position)
    {
        // 필요하다면 여기서 생성 이펙트도 생성하고
        // 필요하다면 인자값으로 데미지도 받아서 넘겨주고..
        
        return Instantiate(_bulletPrefab, position, Quaternion.identity);
    }

    public GameObject MakeSubBullet(Vector3 position)
    {
        return Instantiate(_subBulletPrefab, position, Quaternion.identity);
    }
}
