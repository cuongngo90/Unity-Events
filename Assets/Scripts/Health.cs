using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    //-------------------------------------------------------
    //Internal variables for health and mana
    public int _hp = 100;
    public int _mana = 50;
    //-------------------------------------------------------

    public int HP
    {
        get { return _hp; }
        set
        {
            _hp = value;
            EventManager.Instance.PostNotification(EVENT_TYPE.HEALTH_CHANGE, this, _hp);

            if (_hp <= 0)
            {
                EventManager.Instance.PostNotification(EVENT_TYPE.HEALTH_EMPTY, this, _hp);
                EventManager.Instance.RemoveListener(EVENT_TYPE.HEALTH_CHANGE, OnHeathChange);
                EventManager.Instance.RemoveListener(EVENT_TYPE.HEALTH_EMPTY, OnHeathChange);
            }
        }
    }

    public int Mana
    {
        get { return _mana; }
        set
        {
            _mana = value;
            EventManager.Instance.PostNotification(EVENT_TYPE.MANA_CHANGE, this, _mana);
            if (_mana <= 0)
                EventManager.Instance.PostNotification(EVENT_TYPE.MANA_EMPTY, this, _mana);
        }
    }

	// Use this for initialization
	void Start () {
        EventManager.Instance.AddListener(EVENT_TYPE.HEALTH_CHANGE, OnHeathChange);
        EventManager.Instance.AddListener(EVENT_TYPE.MANA_CHANGE, OnManaChange);
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    public void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch (Event_Type)
        {
            case EVENT_TYPE.HEALTH_CHANGE:
                OnHeathChange(Sender, (int)Param);
                break;
            case EVENT_TYPE.MANA_CHANGE:
                OnManaChange(Sender, (int)Param);
                break;
        }
    }

    void OnHeathChange(Component Health, object newHP)
    {
        //If health has changed of this object
        if (this.GetInstanceID() != Health.GetInstanceID()) return;

        Debug.Log("Object: " + gameObject.name + "'s Health is: " + newHP.ToString());
    }

    void OnManaChange(Component Health, object newMana)
    {
        //If health has changed of this object
        if (this.GetInstanceID() != Health.GetInstanceID()) return;

        Debug.Log("Object: " + gameObject.name + "'s Mana is: " + newMana.ToString());
    }

}
