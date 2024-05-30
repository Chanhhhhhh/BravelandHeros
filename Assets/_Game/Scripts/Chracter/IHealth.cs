using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth 
{
    public void TakeDamage(int damage);

    public bool IsDie { get; }
    public Transform TF { get; }
    public int MaxHP { get; }
}
