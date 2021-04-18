using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public int damage { get; private set; }
    public bool isCritical { get; private set; }
    public Vector2 hitBackForce { get; private set; }

    public Attack(int damage_, bool isCritical_, Vector2 hitBackForce_)
    {
        damage = damage_;
        isCritical = isCritical_;
        hitBackForce = hitBackForce_;
    }
}
