using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected float m_speed = 20f;
    protected int m_damage = 10;

    protected GameObject m_target;
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

    public void Init(GameObject target)
    {
        m_target = target;
    }
}
