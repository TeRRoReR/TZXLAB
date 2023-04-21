using UnityEngine;
using System.Collections;

public class GuidedProjectile : Projectile 
{
	// Снаряд кристала
	// public GameObject m_target;
	// public float m_speed = 0.2f;
	// public int m_damage = 10;

	// private void Update () {
	// 	// if (m_target == null) {
	// 	// 	Destroy (gameObject);
	// 	// 	return;
	// 	// }

	// 	// var translation = m_target.transform.position - transform.position;
	// 	// if (translation.magnitude > m_speed) {
	// 	// 	translation = translation.normalized * m_speed;
	// 	// }
	// 	// transform.Translate (translation * Time.deltaTime);
	// }

	public override void Move()
    {
		if (m_target == null) {
			Destroy (gameObject);
			return;
		}
        var translation = m_target.transform.position - transform.position;
		// if (translation.magnitude > m_speed) {
			translation = translation.normalized * m_speed * Time.deltaTime;
		//}
		transform.Translate (translation);
    }

	// private void OnTriggerEnter(Collider other) 
	// {
	// 	if(other.gameObject.TryGetComponent<HealthComponent>(out HealthComponent health))
	// 	{
	// 		health.TakeDamage(m_damage);
	// 	}
	// 	Destroy(gameObject);
	// }
}
