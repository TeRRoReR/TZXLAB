using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerController : MonoBehaviour
{
    [SerializeField] protected GameObject m_weapon;
    private SearchComponent m_search;
    protected AttackComponent m_attack;
    protected GameObject m_container;
    protected GameObject m_target;
    protected virtual void Start()
    {
        m_search = GetComponent<SearchComponent>();
        m_attack = m_weapon.GetComponent<AttackComponent>();
        m_attack.Init(m_container);
    }

    protected virtual void Shot(GameObject target)
    {
        m_attack.Shot(target);
    }

    protected virtual void Update()
    {
        if(!m_search)
        {
            return;
        }
        m_target = m_search.Current;
        if(m_target)
        {
            Shot(m_target);
        }
    }
}
