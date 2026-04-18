using System;
using UnityEngine;

public enum DefaultVisibility
{
    Visible,
    Hidden
}

public class ChoiceObject: MonoBehaviour, IComparable<ChoiceObject>
{
    public ChoiceGroup group;
    public int order = 0;
    public DefaultVisibility defaultVisibility = DefaultVisibility.Visible;

    private void Start()
    {
        if(GameManager.Instance != null)
            GameManager.Instance.RegisterChoiceObject(this);
        
        if(defaultVisibility == DefaultVisibility.Hidden)
            gameObject.SetActive(false);
    }

    public int CompareTo(ChoiceObject other)
    {
        return order.CompareTo(other.order);
    }
}
