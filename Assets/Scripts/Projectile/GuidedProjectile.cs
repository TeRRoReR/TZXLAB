using UnityEngine;
using System.Collections;

public class GuidedProjectile : Projectile , IProjectile
{
	protected override void Move(Vector3 dir)
    {
        var translation = m_target.transform.position - transform.position;
		translation = translation.normalized * m_speed * Time.deltaTime;
		transform.Translate (translation);
    }

	public void Init(GameObject target, float speed, Vector3 dir)
	{
		m_target = target;
		m_speed = speed;
		HealthComponent death = m_target.GetComponent<HealthComponent>();
		death.onDeath += RemoveObj;
	}
}
