using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject[] m_towers;

    private void Start()
    {
        foreach (var go in m_towers)
        {
            var towerBeh = go.GetComponent<ITower>();
            towerBeh.Init(gameObject);
        }
    }
}
