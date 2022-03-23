using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, IPickable
{
    private enum ItemType
    {
        Item,
        Interactable,
        Bonus,
        Grabable
    }
    [SerializeField]
    private ItemType itemType = ItemType.Item;

    public int hpRestores;
    public float invinsibilityTime;

    public Collider2D collider;
    public Player player;

    private void Start()
    {
        switch (itemType)
        {
            case ItemType.Item:
                break;
            case ItemType.Interactable:
                break;
            case ItemType.Bonus:
                collider.isTrigger = true;
                break;
            case ItemType.Grabable:
                collider.isTrigger = true;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (itemType)
        {
            case ItemType.Item:
                break;
            case ItemType.Interactable:
                break;
            case ItemType.Bonus:
                Use();
                break;
            case ItemType.Grabable:
                break;

        }
    }

    public void PickUp()
    {
        throw new System.NotImplementedException();
    }

    public void Discard()
    {
        throw new System.NotImplementedException();
    }

    public void Grab()
    {
        throw new System.NotImplementedException();
    }

    public void Throw()
    {
        throw new System.NotImplementedException();
    }

    public void Use()
    {
        
    }
}
