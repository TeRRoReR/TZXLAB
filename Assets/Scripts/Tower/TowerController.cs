using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerController : MonoBehaviour
{
    [SerializeField] protected float m_delay = 0.5f;
    [SerializeField] protected float m_range = 4f;
    [SerializeField] protected float m_speedProjectile = 20f;
    [SerializeField] protected GameObject m_projectilePrefab;
	[SerializeField] protected Transform m_shootPoint;
    [SerializeField] private LayerMask m_layerMask;

    protected float m_startTime = 0f;

    private void Start()
    {
        m_startTime = Time.time;
    }
    private void Shot(GameObject target)
    {
        if (Time.time > m_startTime + m_delay)
        {
            var projectile = Instantiate(m_projectilePrefab, m_shootPoint.position, m_shootPoint.rotation);
            var projectileBeh = projectile.GetComponent<IProjectile>(); 
            projectileBeh.Init(target, m_speedProjectile);
            m_startTime = Time.time;
        }
    }

    public virtual void Rotation(GameObject target){}

    private void Update()
    {
        var monster = Physics.OverlapSphere(transform.position, m_range, m_layerMask);
        if(monster.Length > 0)
        { 
            Rotation(monster[0].gameObject);
            Shot(monster[0].gameObject);
        }
    }

    
}
