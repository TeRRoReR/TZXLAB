using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public event System.Action<GameObject> onDestroy;
    [SerializeField] private int m_damage = 10;
    protected float m_speed = 20f;
    protected GameObject m_target;

    protected virtual void Update()
    {
        Move(Vector3.forward); 
    }

    protected abstract void Move(Vector3 dir);

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
        //Destroy(gameObject);
        onDestroy?.Invoke(gameObject);
    }
}
