using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchComponent : MonoBehaviour
{
    [SerializeField] private float m_range;
    [SerializeField] private LayerMask searchLayer;
    private List<GameObject> foundObjects = new List<GameObject>();
    private GameObject m_current;
    private SphereCollider m_collider;

    public GameObject Current
    {
        get 
        {
            if(foundObjects.Count > 0)
            {
                return foundObjects[0];
            }
            else return null; 
        }
    }
   
    private void Start()
    {
        m_collider = GetComponent<SphereCollider>();
        m_collider.radius = m_range;
    }

    private void OnTriggerEnter(Collider other) 
    {
        GameObject target = other.gameObject;
        if (searchLayer == (searchLayer | (1 << target.layer)))
        {
            if (!foundObjects.Contains(target))
            {
                foundObjects.Add(target);
                HealthComponent deathHandler = target.GetComponent<HealthComponent>();
                deathHandler.onDeath += RemoveEnemy;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (searchLayer == (searchLayer | (1 << other.gameObject.layer)))
        {
            if (foundObjects.Contains(other.gameObject))
            {
                foundObjects.Remove(other.gameObject);
            }
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        HealthComponent deathHandler = enemy.GetComponent<HealthComponent>();
        deathHandler.onDeath -= RemoveEnemy;

        if (foundObjects.Contains(enemy.gameObject))
        {
            foundObjects.Remove(enemy.gameObject);
        }
    }
}
