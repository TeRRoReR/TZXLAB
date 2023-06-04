using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] private float m_delay = 0.5f;
    [SerializeField] private float m_speedProjectile = 20f;
    [SerializeField] private ObjectPoolSO[] m_objectPool;
	[SerializeField] private Transform m_shootPoint;
    public float speedProjectile => m_speedProjectile;
    public Transform shootPoint => m_shootPoint;
    private int currentProjectile = 0;
    private float m_startTime = 0f;
    private GameObject m_container;
    private int m_currentBullet = 0;
    public void ChangeBullet(int ID)
    {
        m_currentBullet = ID;
    }
    
    private void Start()
    {
        foreach(ObjectPoolSO go in m_objectPool)
        {
            go.Initialize(m_container);
        }
        m_startTime = Time.time;
    }

    public void Init(GameObject container)
    {
        m_container = container;
    }

    public void Shot(GameObject target)
    {
        if (Time.time > m_startTime + m_delay)
        {
            GameObject projectile = m_objectPool[m_currentBullet].GetObject(m_container);
            projectile.SetActive(true);
            projectile.transform.rotation = m_shootPoint.transform.rotation;
            projectile.transform.position = m_shootPoint.transform.position;
            currentProjectile++;
            var projectileBeh = projectile.GetComponent<IProjectile>();
            projectileBeh.Init(target, m_speedProjectile, m_currentBullet);
            projectileBeh.onDestroy += RemoveProjectile;
            m_startTime = Time.time;
        }
    }

    private void RemoveProjectile(GameObject projectile, int ID)
    {
        var projectileBeh = projectile.GetComponent<IProjectile>();
        projectileBeh.onDestroy -= RemoveProjectile;

        m_objectPool[ID].ReturnObject(projectile);
        currentProjectile--;
    }
}
