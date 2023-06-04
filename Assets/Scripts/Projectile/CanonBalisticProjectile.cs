using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBalisticProjectile : Projectile, IProjectile
{
    private Rigidbody m_rb;
    protected override void OnEnable() 
    {
        base.OnEnable();
        m_rb = GetComponent<Rigidbody>();
    }

	public void Init(GameObject target, float speed, int ID)
	{
        m_ID = ID;
		m_speed = speed;  
        var m_direction = transform.forward;
        m_rb.velocity = m_speed * m_direction; 
	}
}
