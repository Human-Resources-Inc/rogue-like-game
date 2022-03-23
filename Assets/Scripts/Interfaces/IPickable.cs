using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable
{
    public void PickUp();
    public void Discard();
    public void Grab();
    public void Throw();
    public void Use();
}
