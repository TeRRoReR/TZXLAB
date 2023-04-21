using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float m_speed = 20f;
    [SerializeField] private int m_damage = 10;

    private void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        var translation = Vector3.forward * m_speed * Time.deltaTime;
		transform.Translate (translation);
    }

    private void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.TryGetComponent<HealthComponent>(out HealthComponent health))
		{
			health.TakeDamage(m_damage);
		}
		Destroy(gameObject);
	}

    public void Init()
    {
        
    }
}
