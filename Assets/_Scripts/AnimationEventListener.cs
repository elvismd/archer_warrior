using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class AnimationEventListenerEntry
{
    public string name;
    public UnityEvent OnGetCalled = new UnityEvent();
}

public class AnimationEventListener : MonoBehaviour
{
    public List<AnimationEventListenerEntry> listeners = new List<AnimationEventListenerEntry>();
    public bool debugLog = false;

    public void CallEvent(string name)
    {
        var listener = listeners.Find(it => it.name == name);
        if (listener != null)
        {
            listener.OnGetCalled.Invoke();
            if (debugLog)
            {
                Debug.Log("[Animation Event Listener:" + gameObject.name + "] Begin Event " + name);
                // System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                // Debug.Log(t.ToString());
            }
        }
    }
}
