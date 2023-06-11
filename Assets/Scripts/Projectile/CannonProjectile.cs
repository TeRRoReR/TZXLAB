using UnityEngine;
using System.Collections;

public class CannonProjectile : Projectile , IProjectile
{
	protected Rigidbody m_rb;
	protected Vector3 m_direction;

    protected override void OnEnable()
	{
		base.OnEnable();
		m_direction = transform.forward;
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
