using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    void OnAttacked(GameObject attacker, Attack attack);
}
