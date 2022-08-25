using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSprite : MonoBehaviour
{
    public Sprite regular;
    public Sprite serious;
    public Sprite surprised;
    public Sprite happy;
    private Image imageComponent;
    public static CharacterSprite instance;

    // Used so other functions can more easily track available options
    public enum detectiveSprites
    {
        regular,    // 0
        serious,    // 1
        surprised,  // 2
        happy,      // 3
        noChange    // 4
    };

    // Get image component
    private void Awake()
    {
        instance = this;
        imageComponent = this.GetComponent<Image>();
    }

    // If NoChange is passed then, appropiately, nothing will happen
    public void changeSprite(detectiveSprites sprite)
    {
        if (sprite == detectiveSprites.happy)
        {
            imageComponent.sprite = happy;
        }
        else if (sprite == detectiveSprites.regular)
        {
            imageComponent.sprite = regular;
        }
        else if (sprite == detectiveSprites.serious)
        {
            imageComponent.sprite = serious;
        }
        else if (sprite == detectiveSprites.surprised)
        {
            imageComponent.sprite = surprised;
        }
    }
}
