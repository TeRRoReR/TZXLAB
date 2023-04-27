﻿using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour 
{
	[SerializeField] private ObjectPoolSO objectPool;
	[SerializeField] private GameObject m_moveTarget;
	[SerializeField] private GameObject m_prefabMonster;
	[SerializeField] private float m_interval = 3;
	private float m_lastSpawn = -1;
	private int currentEnemies = 0;
	private void Start()
	{
		objectPool.Initialize();
		SpawnEnemy();
	}
	private void Update () 
	{
		if (Time.time > m_lastSpawn + m_interval) 
		{
			
			m_lastSpawn = Time.time;
		}
	}

	private void SpawnEnemy()
	{
		GameObject enemy = objectPool.GetObject();
        enemy.transform.position = transform.position;
        enemy.SetActive(true);
        currentEnemies++;
		var enemyBeh = enemy.GetComponent<Monster>();
		enemyBeh.Init(m_moveTarget);

		HealthComponent death = enemyBeh.GetComponent<HealthComponent>();
		death.onDeath += RemoveEnemy;
	}
	public void RemoveEnemy(GameObject enemy)
    {
        HealthComponent deathHandler = enemy.GetComponent<HealthComponent>();
        deathHandler.onDeath -= RemoveEnemy;

        objectPool.ReturnObject(enemy);
        currentEnemies--;
    }
}
