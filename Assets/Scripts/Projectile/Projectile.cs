using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] private int m_damage = 10;
    protected float m_speed = 20f;
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
            health.onDeath -= RemoveObj;
		}
        DestroyObject();
		
	}

    protected void RemoveObj(GameObject target)
    {
        HealthComponent deathHandler = target.GetComponent<HealthComponent>();
        deathHandler.onDeath -= RemoveObj;
        DestroyObject();
    }

    protected void DestroyObject()
    {
        Destroy(gameObject);
    }
}
