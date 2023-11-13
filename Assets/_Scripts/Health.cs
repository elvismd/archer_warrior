using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UltEvents;

#if UNITY_EDITOR 
using UnityEditor;
#endif 

[System.Serializable]
public class UnityEvtTransform : UltEvent<Transform> { }

public partial class Health : MonoBehaviour
{
    public float initialValue = 100f;
 
    public float value = 100f;

    public bool invincible = false;

    public UltEvent onDie = new UltEvent();
    public UltEvent onDamage = new UltEvent();
    public UltEvent onHeal = new UltEvent();

    public UnityEvtTransform onDamageTransformRef = new UnityEvtTransform();

    public UltEvent onEndInvincibility = new UltEvent();

    float invincibleElapsed = 0.0f;

    public bool IsInvincible => invincibleElapsed > Time.time || invincible;
    bool prevInvincible;

    private void Start()
    {
        value = initialValue;
    }

    private void Update()
    {

        if (!IsInvincible && prevInvincible)
        {
            onEndInvincibility.Invoke();
        }

        prevInvincible = IsInvincible;
    }

    public void Change(float mod, Transform origin = null)
    {
        if (IsInvincible && mod < 0) return;

        if (((value <= 0) && (value + mod <= 0)) || (value >= initialValue && (value + mod >= initialValue))) return;

        value += mod;
        if (mod < 0)
        {
            onDamage.Invoke();
            onDamageTransformRef.Invoke(origin);
        }
        else if (mod > 0)
            onHeal.Invoke();

        if (value > initialValue)
            value = initialValue;

        if (value <= 0)
        {
            value = 0;
            onDie.Invoke();
        }
    }

    public void InvincibleFor(float seconds)
    {
        invincibleElapsed = Time.time + seconds;
    }

    public void ResetHealth()
    {
        value = initialValue;
    }
}

#if UNITY_EDITOR 
[CustomEditor(typeof(Health))]
public class HealthEditor : Editor
{
    Health h;

    private void OnEnable()
    {
        h = target as Health;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (Application.isPlaying)
        {
            if (GUILayout.Button("Simulate Damage (5)"))
            {
                h.Change(-5);
            }
        }
    }

    private void OnSceneGUI()
    {
        Handles.color = Color.red;
        Handles.Label(h.transform.position + Vector3.up * 1.5f, "health:" + h.value.ToString());
    }
}
#endif