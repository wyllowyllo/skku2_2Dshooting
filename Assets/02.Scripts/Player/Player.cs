using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("체력")]
    [SerializeField] private float _health = 3f;
    [SerializeField] private float _maxHealth=3f;
    
    [Header("VFX")]
    [SerializeField] private GameObject _deadVfxPrefab;
    private void Start()
    {
        _health = _maxHealth;
    }
    public void Hit(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            PlayDeadVFX();
            Destroy(gameObject);
        }
    }

    public void Heal(float increment)
    {
        _health += increment;
        _health = Mathf.Min(_health, _maxHealth);
    }

    private void PlayDeadVFX()
    {
        if (_deadVfxPrefab == null) return;
        
        Instantiate(_deadVfxPrefab, transform.position, Quaternion.identity);
    }
   
}
