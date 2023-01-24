using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem<T> where T : EventSystem<T> {

    private static Action mAction;

    public static void  Trigger()
    {
        mAction?.Invoke();
    }

    public static void Resgister(Action action)
    {
        mAction += action;
    }

    public static void Delete(Action action) {
        mAction -= action;
    }

}
