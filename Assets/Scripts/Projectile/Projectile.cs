using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] private int m_damage = 10;
    [SerializeField] private float m_lifeTime = 5f;
    public event System.Action<GameObject> onDestroy;
    protected float m_speed = 20f;
    protected GameObject m_target;
    private float m_currentLifeTime = 0f;

    protected virtual void OnEnable()
    {
        m_currentLifeTime = m_lifeTime;
    }

    protected virtual void Update()
    {
        m_currentLifeTime -= Time.deltaTime;
		if (m_currentLifeTime <= 0)
		{
			DestroyObject();
		}
    }

    private void OnCollisionEnter(Collision other) 
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
        onDestroy?.Invoke(gameObject);
    }

}
