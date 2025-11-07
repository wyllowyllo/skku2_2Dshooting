using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health = 3f;
    [SerializeField] private float _maxHealth=3f;

    private void Start()
    {
        _health = _maxHealth;
    }
    public void Hit(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(float increment)
    {
        _health += increment;
        _health = Mathf.Min(_health, _maxHealth);
    }
    
   
}
