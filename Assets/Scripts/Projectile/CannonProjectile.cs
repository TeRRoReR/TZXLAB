using UnityEngine;
using System.Collections;

public class CannonProjectile : Projectile , IProjectile
{
	protected override void Update(){}
	
	protected override void Move(Vector3 dir)
    {
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.velocity = dir * m_speed;
	}

	public void Init(GameObject target, float speed, Vector3 dir)
	{
		m_target = target;
		m_speed = speed;
		Move(dir);
	}
}
