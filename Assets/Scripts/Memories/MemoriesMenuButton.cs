using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class MemoriesMenuButton : MonoBehaviour
{

    public Sprite memoriesSprite;
    public Sprite memoriesHoverSprite;
    public Sprite interrogationSprite;
    public Sprite interrogationHoverSprite;
    private Image imgComponent;
    public AudioSource audioSource;
    public AudioClip pressSound;
    public AudioClip mouseOverSound;
    [HideInInspector] public bool isShowingMemoriesMenu;
    [HideInInspector] public bool isMouseOver;

    // Initialize variables
    private void Awake()
    {
        imgComponent = GetComponent<Image>();
        imgComponent.sprite = memoriesSprite;
        audioSource = this.GetComponent<AudioSource>();
        isShowingMemoriesMenu = false;
        isMouseOver = false;
    }


    // Toggles between showing or hiding the memories menu.
    // The UI button changes accordingly
    public void ToggleMemoriesMenu()
    {
        audioSource.volume = 0.53f;
        audioSource.clip = pressSound;
        audioSource.Play();
        if (isShowingMemoriesMenu)
            HideMemories();
        else
            DisplayMemories();
    }

    // Hides the memories menu
    public void HideMemories()
    { 
        // Hide memories panel
        MemoriesPanel.instance.Hide();
        isShowingMemoriesMenu = false;

        // Change button sprites
        imgComponent.sprite = memoriesSprite;
        var spriteState = this.GetComponent<Button>().spriteState;
        spriteState.highlightedSprite = memoriesHoverSprite;
        this.GetComponent<Button>().spriteState = spriteState;
    }

    // Displays the memories menu
    public void DisplayMemories()
    {
        // Show memories panel
        MemoriesPanel.instance.Show();
        isShowingMemoriesMenu = true;

        // Change button sprites
        imgComponent.sprite = interrogationSprite;
        var spriteState = this.GetComponent<Button>().spriteState;
        spriteState.highlightedSprite = interrogationHoverSprite;
        this.GetComponent<Button>().spriteState = spriteState;
    }

    // Check if a left-click should advance dialogue
    // Dialog if mouse is over the memories menu button
    public bool isMouseOverButton()
    {
        return isMouseOver;
    }

    // Called when mouse hovers over button
    // Used to change sprite and play sound effect
    public void MouseOver()
    {
        audioSource.clip = mouseOverSound;
        audioSource.volume = 1f;
        audioSource.Play();
        isMouseOver = true;
    }
    public void MouseExit()
    {
        isMouseOver = false;
    }

    // Called when back button is pressed
    // Used because back button disables itself, and by extension, the audio source
    public void backButtonPress()
    {
        audioSource.clip = pressSound;
        audioSource.volume = 0.53f;
        audioSource.Play();
    }
}
