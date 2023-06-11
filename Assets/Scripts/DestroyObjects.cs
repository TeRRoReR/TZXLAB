using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<HealthComponent>(out HealthComponent health))
		{
			health.RemoveObject();
		}
    }
}
