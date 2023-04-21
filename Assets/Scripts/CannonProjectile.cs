using UnityEngine;
using System.Collections;

public class CannonProjectile : MonoBehaviour 
{
	// Снаряд
	public float m_speed = 0.2f;
	[SerializeField] private int m_damage = 10;

	private void Update () 
	{
		var translation = Vector3.forward * m_speed * Time.deltaTime;
		transform.Translate (translation);
	}

	private void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.TryGetComponent<HealthComponent>(out HealthComponent health))
		{
			health.TakeDamage(m_damage);
		}
		Destroy(gameObject);
	}
}
