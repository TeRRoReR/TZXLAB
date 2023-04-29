using UnityEngine;
using System.Collections;

public class CannonProjectile : Projectile , IProjectile
{
	public override void Move()
    {
		var translation = Vector3.forward * m_speed * Time.deltaTime;
		transform.Translate (translation);
	}

	public void Init(GameObject target, float speed)
	{
		m_target = target;
		m_speed = speed;
	}
}
