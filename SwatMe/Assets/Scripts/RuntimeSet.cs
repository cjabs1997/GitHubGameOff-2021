using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeSet<T> : ScriptableObject
{
    [SerializeField] protected List<T> Items = new List<T>();

    public void Add(T thing)
    {
        if (!Items.Contains(thing))
            Items.Add(thing);
    }

    public virtual void Remove(T thing)
    {
        if (Items.Contains(thing))
            Items.Remove(thing);
    }

    private void OnDisable()
    {
        Items = new List<T>();
    }
}
