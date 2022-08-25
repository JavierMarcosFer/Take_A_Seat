using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoriesInfo : MonoBehaviour
{
    public static MemoriesInfo instance;
    public MemoriesItem prefabItem;
    static List<MemoriesItem> itemList = new List<MemoriesItem>();    // List of currently available buttons
    public VerticalLayoutGroup vLayoutGroup;    // Manages the arrangement of buttons on the screen
    public Text fullNameText, genderAndAgeText, occupationText, descriptionText;
    public Image largeImageBox;
    [Header("Images")]
    public Sprite thirdFloorSprite;
    public Sprite broochSprite;
    public Sprite evidenceSprite;
    public Sprite hardingSprite;
    public Sprite horseSprite;

    public enum repoSprites
    {
        thirdFloor,
        brooch,
        evidence,
        harding,
        horse
    }
    // Used so List Items can grab their images
    public Sprite GetSprite(repoSprites sprite)
    {
        if (sprite == repoSprites.thirdFloor) { return thirdFloorSprite; }
        else if (sprite == repoSprites.brooch) { return broochSprite; }
        else if (sprite == repoSprites.evidence) { return evidenceSprite; }
        else if (sprite == repoSprites.harding) { return hardingSprite; }
        else if (sprite == repoSprites.horse) { return horseSprite; }
        else return evidenceSprite;
    }

    // Delete all current options
    public static void ClearList()
    {
        foreach (MemoriesItem button in itemList)
        {
            DestroyImmediate(button.gameObject);
        }
        itemList.Clear();
    }

    // Clears information on the details screen
    public void clearDetails()
    {
        fullNameText.text = "";
        genderAndAgeText.text = "";
        occupationText.text = "";
        descriptionText.text = "";
        largeImageBox.color = new Color(255, 255, 255, 0);
    }

    // Add an item to the list
    void AddItem(string displayName, string fullName, string genderAndAge, string occupation, string description, repoSprites sprite)
    {
        // Create a new item out of the Prefab and activate it
        GameObject o = Instantiate(instance.prefabItem.gameObject, instance.prefabItem.transform.parent);
        o.SetActive(true);

        MemoriesItem newItem = o.GetComponent<MemoriesItem>();
        // Add info to item
        newItem.displayName = displayName;
        newItem.setDisplayName(displayName);
        newItem.fullName = fullName;
        newItem.genderAndAge = genderAndAge;
        newItem.occupation = occupation;
        newItem.description = description;
        newItem.image = GetSprite(sprite);
        newItem.listImageBox.sprite = GetSprite(sprite);

        itemList.Add(newItem);
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    
    /* 
     *  ======================================================================================
     *  The following functions are placeholder functions to load items into the memories list
     *  Each will load all items in their respective placeholder category
     *  ======================================================================================
     */
    public void LoadPeople()
    {
        ClearList();
        //Check which whose memories to display
        if (!MemoriesShiftButton.isShowingInterrogatorMemories)
        {
            // Own memories
            int len = PeopleRepo.displayName.Length;
            for (int i = 0; i < len; i++)
            {
                AddItem(PeopleRepo.displayName[i], PeopleRepo.fullName[i],
                    PeopleRepo.genderAndAge[i], PeopleRepo.occupation[i],
                    PeopleRepo.description[i], PeopleRepo.sprites[i]);
            }
        }
        else
        {
            // Interrogator's memories
            int len = InterrogatorPeopleRepo.displayName.Length;
            for (int i = 0; i < len; i++)
            {
                AddItem(InterrogatorPeopleRepo.displayName[i], InterrogatorPeopleRepo.fullName[i],
                    InterrogatorPeopleRepo.genderAndAge[i], InterrogatorPeopleRepo.occupation[i],
                    InterrogatorPeopleRepo.description[i], InterrogatorPeopleRepo.sprites[i]);
            }
        }
    }
    public void LoadItems()
    {
        ClearList();
        //Check which whose memories to display
        if (!MemoriesShiftButton.isShowingInterrogatorMemories)
        {
            int len = ItemRepo.displayName.Length;
            for (int i = 0; i < len; i++)
            {
                AddItem(ItemRepo.displayName[i], ItemRepo.fullName[i],
                    ItemRepo.genderAndAge[i], ItemRepo.occupation[i],
                    ItemRepo.description[i], ItemRepo.sprites[i]);
            }
        }
        else
        {
            // Interrogator's memories
            int len = ItemRepo.displayName.Length;
            for (int i = 0; i < len; i++)
            {
                AddItem(InterrogatorItemRepo.displayName[i], InterrogatorItemRepo.fullName[i],
                    InterrogatorItemRepo.genderAndAge[i], InterrogatorItemRepo.occupation[i],
                    InterrogatorItemRepo.description[i], InterrogatorItemRepo.sprites[i]);
            }
        }
    }
    public void LoadFlashbacks()
    {
        ClearList();
        //Check which whose memories to display
        if (!MemoriesShiftButton.isShowingInterrogatorMemories)
        {
            int len = FlashbacksRepo.displayName.Length;
            for (int i = 0; i < len; i++)
            {
                AddItem(FlashbacksRepo.displayName[i], FlashbacksRepo.fullName[i],
                    FlashbacksRepo.genderAndAge[i], FlashbacksRepo.occupation[i],
                    FlashbacksRepo.description[i], FlashbacksRepo.sprites[i]);
            }
        }
        else
        {
            int len = FlashbacksRepo.displayName.Length;
            for (int i = 0; i < len; i++)
            {
                // Interrogator's memories
                AddItem(InterrogatorFlashbackRepo.displayName[i], InterrogatorFlashbackRepo.fullName[i],
                    InterrogatorFlashbackRepo.genderAndAge[i], InterrogatorFlashbackRepo.occupation[i],
                    InterrogatorFlashbackRepo.description[i], InterrogatorFlashbackRepo.sprites[i]);
            }
        }
    }
    
}
