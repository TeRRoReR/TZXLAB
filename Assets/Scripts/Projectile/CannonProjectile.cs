using UnityEngine;
using System.Collections;

public class CannonProjectile : Projectile , IProjectile, IMove
{
	private Rigidbody m_rb;
	protected Vector3 m_direction;

	protected override void OnEnable()
	{
		base.OnEnable();
		m_direction = Vector3.forward;
	}
	
	protected override void Update()
	{
		base.Update();
		Move();
	}

	public void Move()
    {
		var translation = m_direction * m_speed * Time.deltaTime;
		transform.Translate (translation);
	}

	public void Init(GameObject target, float speed)
	{
		m_speed = speed;
	}
}
