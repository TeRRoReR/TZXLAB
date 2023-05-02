using UnityEngine;
using System.Collections;

public class CannonProjectile : Projectile , IProjectile
{
	private Rigidbody m_rb;
	private Vector3 m_direction;

	protected override void OnEnable()
	{
		base.OnEnable();
		//m_rb = GetComponent<Rigidbody>();
		m_direction = Vector3.forward;
	}
	
	protected override void Move()
    {
		var translation = m_direction * m_speed * Time.deltaTime;
		transform.Translate (translation);
	}

	public void Init(GameObject target, float speed)
	{
		m_target = target;
		m_speed = speed;
	}
}
