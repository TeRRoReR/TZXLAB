using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour 
{
	[SerializeField] private float m_interval = 3;
	[SerializeField] private GameObject m_moveTarget;
	[SerializeField] private GameObject m_prefabMonster;

	private float m_lastSpawn = -1;
	private void Start()
	{
		SpawnEnemy();
	}
	private void Update () 
	{
		if (Time.time > m_lastSpawn + m_interval) 
		{
			//SpawnEnemy();
			m_lastSpawn = Time.time;
		}
	}

	private void SpawnEnemy()
	{
		var newEnemy = Instantiate(m_prefabMonster, transform.position, Quaternion.identity);
		//newMonster.transform.position = transform.position;
		var enemyBeh = newEnemy.GetComponent<Monster>();
		enemyBeh.Init(m_moveTarget);
	}
}
