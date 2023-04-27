using UnityEngine;
using System.Collections;

public class CannonProjectile : Projectile , IProjectile
{
	public void Init(GameObject target, float speed)
	{
		m_target = target;
		m_speed = speed;
	}
}
