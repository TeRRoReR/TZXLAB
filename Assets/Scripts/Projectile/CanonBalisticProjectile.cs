using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBalisticProjectile : Projectile, IProjectile
{
    private Rigidbody m_rb;
    protected override void OnEnable() 
    {
        base.OnEnable();
        m_rb.GetComponent<Rigidbody>();
    }
    protected override void Move()
    {
        
    }

	public void Init(GameObject target, float speed)
	{
		
	}
}
