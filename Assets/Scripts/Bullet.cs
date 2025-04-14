using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    private Transform target;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    public event Action<GameObject> OnDeactivate;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = bulletSpeed * direction;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        var health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(bulletDamage);
        }

        // Mermiyi yok etmek yerine devre dışı bırak
        OnDeactivate?.Invoke(gameObject); // Havuz yöneticisine geri gönder
        gameObject.SetActive(false);
    }
}
