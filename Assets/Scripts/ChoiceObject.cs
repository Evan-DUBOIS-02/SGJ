using System;
using System.Collections.Generic;
using UnityEngine;

public enum DefaultVisibility
{
    Visible,
    Hidden
}

public class ChoiceObject: MonoBehaviour, IComparable<ChoiceObject>
{
    public List<ChoiceGroup> groups;
    public int order = 0;
    public DefaultVisibility defaultVisibility = DefaultVisibility.Visible;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            foreach(var group in groups)
                GameManager.Instance.RegisterChoiceObject(this, group);
        }

        if(defaultVisibility == DefaultVisibility.Hidden)
            gameObject.SetActive(false);
    }

    public int CompareTo(ChoiceObject other)
    {
        return order.CompareTo(other.order);
    }
}
