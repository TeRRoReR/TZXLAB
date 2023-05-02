using UnityEngine;
using System.Collections;

public class SimpleTower : TowerController, ITower
{
    public void Init(GameObject container)
    {
        m_container = container;
    }
}
