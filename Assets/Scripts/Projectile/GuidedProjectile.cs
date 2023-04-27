using UnityEngine;
using System.Collections;

public class GuidedProjectile : Projectile , IProjectile
{
	public override void Move()
    {
		if (m_target == null) {
			Destroy (gameObject);
			return;
		}
        var translation = m_target.transform.position - transform.position;
		translation = translation.normalized * m_speed * Time.deltaTime;
		transform.Translate (translation);
    }

	public void Init(GameObject target, float speed)
	{
		m_target = target;
		m_speed = speed;
	}
}
