using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaticStatsUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public StaticAttributeUI[] statsUI;

    private void Start()
    {
        SetActive(false);
        canvasGroup.blocksRaycasts = false;
    }

    public void UpdateStatsUI(PlayerStaticAttribute[] playerStaticAttributes)
    {
        foreach(var stat in statsUI)
        {
            foreach(var attribute in playerStaticAttributes)
            {
                if (stat.staticType == attribute.staticType)
                {
                    stat.text_BaseValue.text = attribute.value.BaseValue.ToString();
                    stat.text_AddValue.text = string.Concat("+ ", (attribute.value.ModifiedValue - attribute.value.BaseValue));
                }
            }
        }
    }

    public void SetActive(bool active)
    {
        float alpha = active ? 1f : 0f;
        canvasGroup.alpha = alpha;
        canvasGroup.interactable = active;
    }
}
