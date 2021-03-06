﻿using System;
using UnityEngine;

public abstract class ApplicationBase<T> : Singleton<T>
    where T : MonoBehaviour
{
    protected void RegisterController(string eventName, Type controllerType)
    {
        MVC.RegisterController(eventName, controllerType);
    }

    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}
