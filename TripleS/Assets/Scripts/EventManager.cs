// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventManager.cs" company="SSS">
//   MIT
// </copyright>
// <summary>
//  A simple messaging system which will allow items in our projects to subscribe to events, and have events trigger
//  actions in our games. This will reduce dependencies and allow easier maintenance of our projects.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// There needs to be one active EventManger script on a GameObject in your scene.
/// When you want to fire an Event, use "EventManager.TriggerEvent(eventName)".
/// For more information visit: https://unity3d.com/learn/tutorials/topics/scripting/events-creating-simple-messaging-system
/// </summary>
public class EventManager : MonoBehaviour
{
    /// <summary>
    /// The event manager.
    /// </summary>
    private static EventManager eventManager;

    /// <summary>
    /// The dictionary data structure.
    /// </summary>
    private Dictionary<string, UnityEvent> eventDictionary;

    /// <summary>
    /// Gets the singleton instance of this static class.
    /// </summary>
    private static EventManager Instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (eventManager == null)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    /// <summary>
    /// Register an Event to and Even name.
    /// Example Usage:
    ///     void OnEnable()
    ///     {
    ///         EventManager.StartListening("Destroy", Destroy);
    ///         ...
    ///     }
    /// </summary>
    /// <param name="eventName">Name of the Event that you want to stop listening</param>
    /// <param name="listener">Action that you want to stop listening</param>
    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    /// <summary>
    /// Un listening to an event
    /// Example Usage:
    ///     void OnDisable()
    ///     {
    ///         EventManager.StopListening("Destroy", Destroy);
    ///         ...
    ///     }
    /// </summary>
    /// <param name="eventName">Name of the Event that you want to stop listening</param>
    /// <param name="listener">Action that you want to stop listening</param>
    public static void StopListening(string eventName, UnityAction listener)
    {
        if (eventManager == null)
        {
            return;
        }

        UnityEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// To fire an Event, call "EventManager.TriggerEvent(eventName)".
    /// When event 'eventName' is triggered, all listener actions will be invoked.
    /// </summary>
    /// <param name="eventName"> The name of the event you want to trigger </param>
    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }

    /// <summary>
    /// Simply Initialize the dictionary
    /// </summary>
    private void Init()
    {
        if (this.eventDictionary == null)
        {
            this.eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }
}