using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour
{
    Health _heath;
    // Use this for initialization
    void Start()
    {
        _heath = GetComponent<Health>();

        EventManager.Instance.AddListener(EVENT_TYPE.MANA_EMPTY, OnEvent);
    }

    public void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch(Event_Type)
        {
            case EVENT_TYPE.MANA_EMPTY:
                OnManaEmpty(Sender, (int)Param);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _heath.Mana -= 10;
        }
    }

    void OnManaEmpty(Component Health, int newMana)
    {
        if (_heath.GetInstanceID() != Health.GetInstanceID())
            return;

        Debug.Log("Object: " + gameObject.name + " is empty Mana: " + newMana.ToString());
    }
}
