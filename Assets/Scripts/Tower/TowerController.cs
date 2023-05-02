using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerController : MonoBehaviour
{
    [SerializeField] protected float m_delay = 0.5f;
    [SerializeField] protected float m_speedProjectile = 20f;
    [SerializeField] protected GameObject m_projectilePrefab;
    [SerializeField] private ObjectPoolSO m_objectPool;
	[SerializeField] protected Transform m_shootPoint;
    private int currentProjectile = 0; 
    protected SearchComponent m_search;

    protected float m_startTime = 0f;

    protected virtual void Start()
    {
        m_search = GetComponent<SearchComponent>();
        m_objectPool.Initialize(transform.root.gameObject);
        m_startTime = Time.time;
    }

    private void Shot(GameObject target)
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

    protected virtual void Rotation(GameObject target){}

    private void Update()
    {
        var target = m_search.Current;
        if(target)
        {
            Rotation(target);
            Shot(target);
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
