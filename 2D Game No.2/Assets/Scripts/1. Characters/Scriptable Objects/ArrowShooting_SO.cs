using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Data/Attack Stats/Shooting Arrow")]
public class ArrowShooting_SO : AttackDefinition_SO
{
    public GameObject arrow;
    public float maxArrowHeight = 0f;
    public float targetHeight = 0f;

}
