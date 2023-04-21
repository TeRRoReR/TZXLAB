using UnityEngine;
using System.Collections;

public class SimpleTower : TowerController 
{
	// Кристал

	public override void Shot(GameObject target)
	{
		// foreach (var monster in FindObjectsOfType<Monster>()) 
		// {
		// 	if (Vector3.Distance (transform.position, monster.transform.position) > m_range)
		// 		continue;

			if (Time.time > m_startTime + m_delay)
        	{
				var projectile = Instantiate(m_projectilePrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
				var projectileBeh = projectile.GetComponent<GuidedProjectile> ();
				projectileBeh.Init(target);

				m_startTime = Time.time;
			}
		//}
	}
}
