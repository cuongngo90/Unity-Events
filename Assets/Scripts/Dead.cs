using UnityEngine;
using System.Collections;
using System;

public class Dead : MonoBehaviour {

    private Animation _anim;
    private Health _health;
    private bool _isDead;

    bool IsDead
    {
        get { return _isDead; }
        set
        {
            if (value == true)
                EventManager.Instance.PostNotification(EVENT_TYPE.DEAD, this, true);
        }
    }
    void Start()
    {
        _anim = GetComponent<Animation>();
        _health = GetComponent<Health>();

        // Add all events this component listening
        EventManager.Instance.AddListener(EVENT_TYPE.HEALTH_EMPTY, OnHeathEmpty);
    }

    public void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch(Event_Type)
        {
            case EVENT_TYPE.HEALTH_EMPTY:
                OnHeathEmpty(Sender, (int)Param);
            break;
        } 
    }

    private void OnHeathEmpty(Component Health, object newHP)
    {
        if (_health.GetInstanceID() != Health.GetInstanceID())
            return;

        Debug.Log("Object: " + gameObject.name + " is Dead: ");
    }
}
