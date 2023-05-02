using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] protected float m_delay = 0.5f;
    [SerializeField] protected float m_speedProjectile = 20f;
    [SerializeField] private ObjectPoolSO m_objectPool;
	[SerializeField] protected Transform m_shootPoint;
    private int currentProjectile = 0;
    protected float m_startTime = 0f;

    // Update is called once per frame
    private void Start()
    {
        m_objectPool.Initialize(transform.root.gameObject);
        m_startTime = Time.time;
        if(gameObject.TryGetComponent<RotationComponent>(out RotationComponent rotation))
		{
            rotation.Init(m_shootPoint, m_speedProjectile);
        }
    }

    public void Shot(GameObject target)
    {
        if (Time.time > m_startTime + m_delay)
        {
            GameObject projectile = m_objectPool.GetObject(transform.root.gameObject);
            projectile.transform.rotation = m_shootPoint.transform.rotation;
            projectile.transform.position = m_shootPoint.transform.position;
            projectile.SetActive(true);
            currentProjectile++;
            var projectileBeh = projectile.GetComponent<IProjectile>();
            projectileBeh.Init(target, m_speedProjectile);		    
            projectileBeh.onDestroy += RemoveProjectile;
            m_startTime = Time.time;
        }
    }

    private void RemoveProjectile(GameObject projectile)
    {
        var projectileBeh = projectile.GetComponent<IProjectile>();
        projectileBeh.onDestroy -= RemoveProjectile;

        m_objectPool.ReturnObject(projectile);
        currentProjectile--;
    }
}
