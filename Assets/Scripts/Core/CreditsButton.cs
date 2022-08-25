using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsButton : MonoBehaviour
{
    public GameObject creditsContainer;
    bool showingCredits;
    public bool isMouseOver;

    // Start is called before the first frame update
    void Start()
    {
        creditsContainer.SetActive(false);
        showingCredits = false;
    }
    
    public void ToggleCredits() {
        if (showingCredits)
        {
            showingCredits = false;
            creditsContainer.SetActive(false);
        }
        else
        {
            showingCredits = true;
            creditsContainer.SetActive(true);
        }
    }

    public void OnMouseEnter()
    {
        isMouseOver = true;
    }

    public void OnMouseExit()
    {
        isMouseOver = false;
    }
}
