using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifferentColorOnInteractable : MonoBehaviour
{
    public bool isSelected = true;

    public Button otherButton;
    private Button theButton;
    private ColorBlock colors;
    private ColorBlock otherColors;

    void Awake()
    {
        theButton = this.GetComponent<Button>();
        colors = this.GetComponent<Button>().colors;
        otherColors = otherButton.GetComponent<Button>().colors;
        theButton.onClick.AddListener(Click);
    }

    void Click()
    {
        Debug.Log("click");
        colors.normalColor = new Color(colors.normalColor.r, colors.normalColor.g, colors.normalColor.b, 1);
        colors.pressedColor = new Color(colors.normalColor.r, colors.normalColor.g, colors.normalColor.b, 1);
        theButton.colors = colors;
        
        otherColors.normalColor = new Color(otherColors.normalColor.r, otherColors.normalColor.g, otherColors.normalColor.b, 0.3f);
        otherColors.pressedColor = new Color(otherColors.normalColor.r, otherColors.normalColor.g, otherColors.normalColor.b, 0.3f);
        otherButton.colors = otherColors;
    }

}
