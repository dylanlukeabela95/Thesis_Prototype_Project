using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HoverEffectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private List<string> TypeName = new List<string> {"Type1", "Type2", "Type3", "Type4", "Type5" };
    private List<string> TypeSubName = new List<string> { "Type1.1", "Type1.2", "Type4.1", "Type4.2", "Type4.3", "Type4.4", "Type4.5", "Type4.6", "Type4.7", "Type4.8" };

    void Start()
    {
        this.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0, 161, 255, 255);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {

        if (this.GetComponentInChildren<TextMeshProUGUI>().tag != "Examples")
        {
            this.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 0, 121, 255);

            if (TypeName.Contains(this.name))
            {
                this.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 0, 121, 255);
            }
            else if (TypeSubName.Contains(this.name))
            {
                this.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 0, 121, 255);
            }
        }
        else
        {
            this.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 0, 121, 255);
        }

        UIManager.isHover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.GetComponentInChildren<TextMeshProUGUI>().tag != "Examples")
        {
            this.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0, 161, 255, 255);

            if (TypeName.Contains(this.name))
            {
                this.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0, 161, 255, 255);
            }
            else if (TypeSubName.Contains(this.name))
            {
                this.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0, 161, 255, 255);
            }
        }
        else
        {
            this.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0, 161, 255, 255);
        }

        UIManager.isHover = false;
        UIManager.isClicking = false;
    }
}
