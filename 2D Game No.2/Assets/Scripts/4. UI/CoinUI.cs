using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public Text text_Amount;

    public void SetValue(int amount)
    {
        text_Amount.text = string.Concat("+ ", amount.ToString());
    }
}
