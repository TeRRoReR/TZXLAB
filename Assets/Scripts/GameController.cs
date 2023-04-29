using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject[] m_towerPoints;
    [SerializeField] private GameObject[] m_prefabTower;

    private void Start()
    {
        for (int i = 0; i < m_towerPoints.Length; i++)
        {
            SpawnTower(i);
        }
    }

    private void SpawnTower(int id)
    {
        var tower = Instantiate(m_prefabTower[id], m_towerPoints[id].transform);
    }
}
