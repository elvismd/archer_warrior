using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

public class Damager : MonoBehaviour
{   
    public List<string> tagFilter;
    public float damageValue;
    public Transform customDamagerOrigin;

    public UltEvent onDamageIsDone = new UltEvent();

    private void OnTriggerStay(Collider other) 
    {
        if(tagFilter.Contains(other.gameObject.tag))
        {
            if(other.gameObject.TryGetComponent<Health>(out var health))
            {
                health.Change(-damageValue, customDamagerOrigin != null ? customDamagerOrigin : transform);
                onDamageIsDone.InvokeSafe();
            }
        }
    }
}
