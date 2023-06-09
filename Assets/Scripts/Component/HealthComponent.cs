using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public event System.Action<GameObject> onDeath;
    [SerializeField] private int m_maxHP = 100;
    private int m_currentHP = 0;
    private void Awake()
    {
        m_currentHP = m_maxHP;
    }

    private void OnEnable()
    {
        m_currentHP = m_maxHP;
    }

    public void TakeDamage(int damage)
    {
        m_currentHP -= damage;
        if(m_currentHP <= 0)
        {
            RemoveObject();
        }
    }

    public void RemoveObject()
    {
        onDeath?.Invoke(gameObject);
    }
}
