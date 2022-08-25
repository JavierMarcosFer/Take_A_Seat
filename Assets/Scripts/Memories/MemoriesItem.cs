using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoriesItem : MonoBehaviour
{
    // Text boxes and image box
    public Text displayNameText, fullNameText, genderAndAgeText, occupationText, descriptionText;
    public Image listImageBox, largeImageBox;
    public AudioSource audioSource;
    public static MemoriesItem instance;

    // Actual text to be placed in boxes
    [HideInInspector] public string displayName, fullName, genderAndAge, occupation, description;
    [HideInInspector] public Sprite image;


    public void expandItem( )
    {
        fullNameText.text = fullName;
        genderAndAgeText.text = genderAndAge;
        occupationText.text = occupation;
        descriptionText.text = description;
        largeImageBox.sprite = image;
        largeImageBox.color = new Color(255, 255, 255, 255);
        PlaySound();
    }

    public void setDisplayName(string name)
    {
        displayName = name;
        displayNameText.text = name;
    }

    public void setSprite(Sprite sprite)
    {
        image = sprite;
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    private void Awake()
    {
        instance = this;
        audioSource = this.GetComponent<AudioSource>();
    }
}
