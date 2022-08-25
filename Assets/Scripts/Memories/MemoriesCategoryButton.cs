using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoriesCategoryButton : MonoBehaviour
{
    private Image imageComponent;
    public Sprite hardingSprite;
    public Sprite interrogatorSprite;
    public MemoriesPanel memoriesPanel;
    public AudioClip mouseOverSound;
    public AudioClip pressSound;
    private AudioSource audioComponent;

    // Start is called before the first frame update
    void Start()
    {
        imageComponent = this.GetComponent<Image>();
        audioComponent = this.GetComponent<AudioSource>();
    }

    // Change sprites when memories switched
    public void UpdateSprite()
    {
        if (MemoriesShiftButton.isShowingInterrogatorMemories)
        {
            imageComponent.sprite = interrogatorSprite;
        }
        else { imageComponent.sprite = hardingSprite; }

    }

    public void MouseOver()
    {
        audioComponent.clip = mouseOverSound;
        audioComponent.volume = 1f;
        audioComponent.Play();
    }
}
