using UnityEngine;
using System.Collections;

public class CannonProjectile : Projectile , IProjectile
{
	public void Init(GameObject target, float speed, int damage)
	{
		m_target = target;
		m_speed = speed;
		m_damage = damage;
	}
}
