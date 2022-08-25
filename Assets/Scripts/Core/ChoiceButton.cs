using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    public Text ButtonText;
    [HideInInspector] public int choiceIndex = -1;

    public string text { get { return ButtonText.text; }  set { ButtonText.text = value; } }
}
