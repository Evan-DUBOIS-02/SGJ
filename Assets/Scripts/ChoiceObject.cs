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
    [Header("Setup")]
    public List<ChoiceGroup> groups;
    public int order = 0;
    public DefaultVisibility defaultVisibility = DefaultVisibility.Visible;

    [Header("Interaction")]
    [SerializeField] public bool canBeInteracted = false;

    private Material mat;

    private void Awake()
    {
        mat = GetComponentInChildren<Renderer>().material;
    }

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

    public void OnHover()
    {
        mat.SetFloat("_FirstOutlineWidth", 0.1f);
    }

    public void OnUnhover()
    {
        mat.SetFloat("_FirstOutlineWidth", 0.0f);
    }
}
