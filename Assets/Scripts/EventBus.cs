using System;
using System.Collections.Generic;

public static class EventBus<T>
{
    private static readonly Dictionary<GameEvent, Delegate> _events = new();

    public static void Subscribe(GameEvent eventType, Action<T> incomingCallback)
    {
        if (incomingCallback == null) return;

        if (_events.TryGetValue(eventType, out var callbacks))
        {
            _events[eventType] = (Action<T>)callbacks + incomingCallback;
        }
        else
        {
            _events[eventType] = incomingCallback;
        }
    }

    public static void Subscribe(GameEvent eventType, Action incomingCallback)
    {
        if (incomingCallback == null) return;

        if (_events.TryGetValue(eventType, out var callbacks))
        {
            _events[eventType] = (Action)callbacks + incomingCallback;
        }
        else
        {
            _events[eventType] = incomingCallback;
        }
    }

    public static void Unsubscribe(GameEvent eventType, Action<T> incomingCallback)
    {
        if (incomingCallback == null) return;

        if (_events.TryGetValue(eventType, out var callbacks))
        {
            callbacks = (Action<T>)callbacks - incomingCallback;

            if (callbacks == null)
            {
                _events.Remove(eventType);
            }
            else
            {
                _events[eventType] = callbacks;
            }
        }
    }

    public static void Unsubscribe(GameEvent eventType, Action incomingCallback)
    {
        if (incomingCallback == null) return;

        if (_events.TryGetValue(eventType, out var callbacks))
        {
            callbacks = (Action)callbacks - incomingCallback;

            if (callbacks == null)
            {
                _events.Remove(eventType);
            }
            else
            {
                _events[eventType] = callbacks;
            }
        }
    }

    public static void Publish(GameEvent eventType, T value)
    {
        if (_events.TryGetValue(eventType, out var callbacks))
        {
            ((Action<T>)callbacks)?.Invoke(value);
        }
    }

    public static void Publish(GameEvent eventType)
    {
        if (_events.TryGetValue(eventType, out var callbacks))
        {
            ((Action)callbacks)?.Invoke();
        }
    }
}