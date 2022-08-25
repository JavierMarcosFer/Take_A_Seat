using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoriesShiftButton : MonoBehaviour
{
    public GameObject toddSprite;
    public GameObject interrogatorSprite;
    public GameObject rootPanel;
    public MemoriesCategoryButton flashbacksButton;
    public MemoriesCategoryButton itemsButton;
    public MemoriesCategoryButton peopleButton;
    private Button buttonComponent;
    private Image rootImageComponent;
    private AudioSource audioSource;
    public static bool isShowingInterrogatorMemories = false;  // If false, show own memories instead

    // Toggles between showing your own memories or the Interrogator's
    // The UI button changes accordingly
    public void ToggleShownMemories()
    {
        audioSource.Play();
        if (isShowingInterrogatorMemories)
        {
            toddSprite.SetActive(true);
            interrogatorSprite.SetActive(false);
            isShowingInterrogatorMemories = false;

            // Turn button and background blue
            ColorBlock color = buttonComponent.colors;
            color.normalColor = new Color32(51, 115, 250, 255);
            color.highlightedColor = new Color32(37, 72, 144, 255);
            buttonComponent.colors = color;
            rootImageComponent.color = new Color32(80, 88, 103, 202);
        }
        else
        {
            toddSprite.SetActive(false);
            interrogatorSprite.SetActive(true);
            isShowingInterrogatorMemories = true;

            // Turn button and background orange
            ColorBlock color = buttonComponent.colors;
            color.normalColor = new Color32(250, 109, 51, 255);
            color.highlightedColor = new Color32(186, 81, 39, 255);
            buttonComponent.colors = color;
            rootImageComponent.color = new Color32(103, 89, 80, 202);
        }
        // Update category button sprites accordingly
        flashbacksButton.UpdateSprite();
        peopleButton.UpdateSprite();
        itemsButton.UpdateSprite();
    }

    // Start is called before the first frame update
    void Start()
    {
        buttonComponent = this.GetComponent<Button>();
        rootImageComponent = rootPanel.GetComponent<Image>();
        audioSource = this.GetComponent<AudioSource>();

        //Default to own memories
        toddSprite.SetActive(true);
        interrogatorSprite.SetActive(false);
        isShowingInterrogatorMemories = false;
        
        ColorBlock color = buttonComponent.colors;
        color.normalColor = new Color32(51, 115, 250, 255);
        color.highlightedColor = new Color32(37, 72, 144, 255);
        buttonComponent.colors = color;
        rootImageComponent.color = new Color32(80, 88, 103, 202);
    }
}
