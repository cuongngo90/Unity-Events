using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//-----------------------------------------------------------
//Enum defining all possible game events
//More events should be added to the list
public enum EVENT_TYPE
{
    GAME_INIT,
    GAME_END,
    AMMO_CHANGE,
    HEALTH_CHANGE,
    HEALTH_EMPTY,
    MANA_CHANGE,
    MANA_EMPTY,

    EXCUTE_SKILL_NORMAL_ANIMATION,
    EXCUTE_DEAD_ANIMATION,
    DEAD
};
//-----------------------------------------------------------
//Singleton EventManager to send events to listeners
//Works with IListener implementations
public class EventManager : MonoBehaviour
{
    #region C# properties
    //-----------------------------------------------------------
    //Public access to instance
    public static EventManager Instance
    {
        get { return instance; }
        set { }
    }
    #endregion

    #region variables
    //Internal reference to Notifications Manager instance (singleton design pattern)
    private static EventManager instance = null;

    // Declare a delegate type for events
    public delegate void OnEvent(Component Sender, object Param = null);

    //Array of listener objects (all objects registered to listen for events)
    private Dictionary<EVENT_TYPE, OnEvent> Listeners = new Dictionary<EVENT_TYPE, OnEvent>();
    #endregion
    //-----------------------------------------------------------
    #region methods
    //Called at start-up to initialize
    void Awake()
    {
        //If no instance exists, then assign this instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //Prevent object from being destroyed on scene exit
        }
        else //Instance already exists, so destroy this one. This should be a singleton object
            DestroyImmediate(this);
    }
    //-----------------------------------------------------------
    /// <summary>
    /// Function to add specified listener-object to array of listeners
    /// </summary>
    /// <param name="Event_Type">Event to Listen for</param>
    /// <param name="Listener">Object to listen for event</param>
    public void AddListener(EVENT_TYPE Event_Type, OnEvent Listener)
    {
        //List of listeners for this event
        OnEvent delegateListener;

        //New item to be added. Check for existing event type key. If one exists, add to list
        if (Listeners.TryGetValue(Event_Type, out delegateListener))
        {
            //List exists, so add new item
            delegateListener += Listener;
            return;
        }

        //Otherwise create new list as dictionary key
        delegateListener += Listener;
        Listeners.Add(Event_Type, delegateListener); //Add to internal listeners list
    }

    public void RemoveListener(EVENT_TYPE Event_Type, OnEvent Listener)
    {
        if (Listeners.ContainsKey(Event_Type))
        {
            Listeners[Event_Type] -= Listener;
        }

        Debug.Log("Just doesn't have this " + Listener + "yet");
        return;
    }
    //-----------------------------------------------------------
    /// <summary>
    /// Function to post event to listeners
    /// </summary>
    /// <param name="Event_Type">Event to invoke</param>
    /// <param name="Sender">Object invoking event</param>
    /// <param name="Param">Optional argument</param>
    public void PostNotification(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        //Notify all listeners of an event

        //List of listeners for this event only
        OnEvent delegateListener = null;

        //If no event entry exists, then exit because there are no listeners to notify
        if (!Listeners.TryGetValue(Event_Type, out delegateListener))      
            return;

        //Entry exists. Now notify appropriate listeners
        if (delegateListener != null)
            delegateListener.Invoke(Sender, Param);
        

        //Entry exists. Now notify appropriate listeners
        //for (int i = 0; i < ListenList.Count; i++)
        //{
        //    if (!ListenList[i].Equals(null)) //If object is not null, then send message via interfaces
        //        ListenList[i](Event_Type, Sender, Param);
        //}

    }
    //-----------------------------------------------------------
    //Remove event type entry from dictionary, including all listeners
    public void RemoveEvent(EVENT_TYPE Event_Type)
    {
        //Remove entry from dictionary
        Listeners.Remove(Event_Type);
    }
    //-----------------------------------------------------------
    //Remove all redundant entries from the Dictionary
    public void RemoveRedundancies()
    {
        //Create new dictionary
        Dictionary<EVENT_TYPE, OnEvent> TmpListeners = new Dictionary<EVENT_TYPE, OnEvent>();

        //Cycle through all dictionary entries
        foreach (KeyValuePair<EVENT_TYPE, OnEvent> Item in Listeners)
        {
            //If items remain in list for this notification, then add this to tmp dictionary
            if (Item.Value.Target != null)
                TmpListeners.Add(Item.Key, Item.Value);
        }

        //Replace listeners object with new, optimized dictionary
        Listeners = TmpListeners;
    }
    //-----------------------------------------------------------
    //Called on scene change. Clean up dictionary
    void OnLevelWasLoaded()
    {
        RemoveRedundancies();
    }
    //-----------------------------------------------------------
    #endregion
}
