using UnityEngine;
using System.Collections;

public class GuidedProjectile : Projectile , IProjectile, IMove
{
	protected override void Update()
	{
		base.Update();
		Move();
	}
	
	public void Move()
    {
        var translation = m_target.transform.position - transform.position;
		translation = translation.normalized * m_speed * Time.deltaTime;
		transform.Translate (translation);
    }

	public void Init(GameObject target, float speed, int ID)
	{
		m_ID = ID;
		m_target = target;
		m_speed = speed;
		HealthComponent death = m_target.GetComponent<HealthComponent>();
		death.onDeath += RemoveObj;
	}
}
