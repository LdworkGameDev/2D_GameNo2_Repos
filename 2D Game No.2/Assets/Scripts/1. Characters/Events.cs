using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events
{
    [System.Serializable] public class EventInteger : UnityEvent<int> { };
    [System.Serializable] public class EventBool : UnityEvent<bool> { };
}
