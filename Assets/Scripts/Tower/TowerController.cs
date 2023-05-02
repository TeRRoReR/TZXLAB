using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerController : MonoBehaviour
{
    [SerializeField] private GameObject m_weapon;
    private SearchComponent m_search;
    protected RotationComponent m_rotation;
    private AttackComponent m_attack;
    protected GameObject m_container;
    protected virtual void Start()
    {
        m_search = GetComponent<SearchComponent>();
        m_attack = m_weapon.GetComponent<AttackComponent>();
        m_rotation = m_weapon.GetComponent<RotationComponent>();
        m_attack.Init(m_container);
    }

    protected virtual void Shot(GameObject target)
    {
        m_attack.Shot(target);
    }

    protected virtual void Rotation(GameObject target){}

    private void Update()
    {
        var target = m_search.Current;
        if(target)
        {
            Rotation(target);
            Shot(target);
        }
    }
}
