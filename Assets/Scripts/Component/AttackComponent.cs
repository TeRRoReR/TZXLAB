using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] private float m_delay = 0.5f;
    [SerializeField] private float m_speedProjectile = 20f;
    [SerializeField] private ObjectPoolSO m_objectPool;
	[SerializeField] private Transform m_shootPoint;
    private int currentProjectile = 0;
    private float m_startTime = 0f;
    private GameObject m_container;
    
    private void Start()
    {
        m_objectPool.Initialize(m_container);
        m_startTime = Time.time;
        if(gameObject.TryGetComponent<RotationComponent>(out RotationComponent rotation))
		{
            rotation.Init(m_shootPoint, m_speedProjectile);
        }
    }

    public void Init(GameObject container)
    {
        m_container = container;
    }

    public void Shot(GameObject target)
    {
        if (Time.time > m_startTime + m_delay)
        {
            GameObject projectile = m_objectPool.GetObject(m_container);
            projectile.SetActive(true);
            projectile.transform.rotation = m_shootPoint.transform.rotation;
            projectile.transform.position = m_shootPoint.transform.position;
            currentProjectile++;
            var projectileBeh = projectile.GetComponent<IProjectile>();
            projectileBeh.Init(target, m_speedProjectile);
            // var m_rb = projectile.GetComponent<Rigidbody>();
            // m_rb.velocity = m_speedProjectile * m_shootPoint.forward;
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
