using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    DialogueSystem dialogue;
    Foreground foreground;
    FlagTrack flagTracker;
    int i;
    dialogueID currentID;
    string[] currentLines;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogue = DialogueSystem.instance;
        flagTracker = FlagTrack.instance;
        foreground = Foreground.instance;
        currentLines = DialogueIntroduction.lines;
        i = 1;
        currentID = dialogueID.Introduction;
        Say(currentLines[0]);
    }

    // Detect when the user presses spacebar to advance text
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AdvanceDialogue();
        }
    }

    // Advance through dialogue
    // Trigered when DialogueManager detects a Spacebar press
    // or when Foreground detects a left-click
    public void AdvanceDialogue()
    {
        if (!dialogue.isSpeaking || dialogue.isWaitingForUserInput)
        {
            if (i < currentLines.Length)
            {
                // Continue dialogue
                Say(currentLines[i]);
                i++;
            }
            // Dialogue is finished
            else
            {
                NextDialogue();
            }
        }
    }

    void Say(string s)
    {
        // Variables to extract
        string soundFile;
        string speech;
        string speaker;
        bool isAdd;
        CharacterSprite.detectiveSprites sprite;
        // Temporary variables
        string[] parts;
        string temp;

        // New function
        // Extract additive
        // If symbol is missing, retrieving substring is not neccesary
        if (s[0] == '+')
        {
            isAdd = true;
            s = s.Substring(1);
        }
        if (s[0] == '-')
        {
            isAdd = false;
            s = s.Substring(1);
        }
        else
        {
            isAdd = false;
        }

        // Extract sprite number
        // no number means NoChange, also, retrieving substring is not neccesary
        if (s[0] == '0')
        {
            sprite = CharacterSprite.detectiveSprites.regular;
            s = s.Substring(1);
        }
        else if (s[0] == '1')
        {
            sprite = CharacterSprite.detectiveSprites.serious;
            s = s.Substring(1);
        }
        else if (s[0] == '2')
        {
            sprite = CharacterSprite.detectiveSprites.surprised;
            s = s.Substring(1);
        }
        else if (s[0] == '3')
        {
            sprite = CharacterSprite.detectiveSprites.happy;
            s = s.Substring(1);
        }
        else if (s[0] == '4')
        {
            sprite = CharacterSprite.detectiveSprites.noChange;
            s = s.Substring(1);
        }
        else
        {
            sprite = CharacterSprite.detectiveSprites.noChange;
        }
        
        // Extract sounfile name
        // If soundfile is missing, no further action is neccesary
        if (s[0] == '[')
        {
            // Remove encasing
            temp = s.Substring(1);
            parts = temp.Split(']');
            // Get filename
            soundFile = parts[0];
            // Extract encasing from rest of string
            s = parts[1];
        }
        else { soundFile = ""; }

        // Extract speech
        parts = s.Split(':');
        speech = parts[0];

        // Extract speaker
        speaker = (parts.Length >= 2) ? parts[1] : "";
        
        // Perform dialogue
        dialogue.Say(speech, isAdd, soundFile, speaker, sprite);
    }

    enum dialogueID
    {
        Introduction,
        ChoiceIntroduction,
        GiveName,
        Joke,
        RemainSilent,
        TheNightRoyDied,
        TellMeMore,
        TellMeMoreTwo,
        Injuries,
        Key,
        Info,
        NoFriend,
        SympathyCard,
        SympathyCardTwo,
        Disgusting,
        MyFriend,
        HowDoYouKnowHim,
        Callout,
        LocationLocation,
        LocationContinuation,
        OnlyThem,
        Youdonthavetoknow,
        GoodThing,
        GoodThingSuspicious,
        Silence,
        SilenceTwo,
        SilenceThree,
        NothingSpecial,
        DontSpeak,
        AWhatNow,
        LikeAnIdiot,
        Photo,
        Yes,
        FalseCall,
        Caught,
        Dunno,
        BelieveIt,
        CloserLook,
        TheBrooch,
        Annoyed,
        NeverSaid,
        Compromised,
        Phone,
        PlayAlong,
        Expose,
        ExposeAnnoyed,
        ExposeSuspect,
        ExposeContinuation,
        FreakOut,
        DenyClaim,
        Excuse,
        Confess,
        TumblingDown,
        TumblingDownAbridged,
        CooperateEnd,
        SilentEnd,
        FightNightEnd,
        Answer,
        Answertwo,
        AnswerThree
    };

    public void NextDialogue()
    {
        int choiceIndex = -1;
        // =====================================================

        if (currentID == dialogueID.Introduction)
        {
            // Check if choice was made
            if (!ChoicePanel.lastChoice.madeSelection)
            {
                ChoicePanel.Show("Give your name", "Joke around", "Remain silent");
            }
            else
            {
                // Set next lines of dialogue
                choiceIndex = ChoicePanel.lastChoice.index;
                if (choiceIndex == 0) { currentLines = DialogueGiveName.lines; currentID = dialogueID.GiveName; }               //Remain Silent
                if (choiceIndex == 1) { currentLines = DialogueJoke.lines; currentID = dialogueID.Joke; }                       //Joke
                if (choiceIndex == 2) { currentLines = DialogueRemainSilent.lines; currentID = dialogueID.RemainSilent; }      //RemainSilent
                // Continue dialogue
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
        }

        else if (currentID == dialogueID.GiveName ||
                 currentID == dialogueID.RemainSilent)
        {
            // Answer
            currentLines = DialogueAnswer.lines;
            currentID = dialogueID.Answer;
            // Continue dialogue
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }

        else if (currentID == dialogueID.Joke)
        {
            // Answer
            flagTracker.setFlag("suspect");
            currentLines = DialogueAnswer.lines;
            currentID = dialogueID.Answer;
            // Continue dialogue
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }

        else if (currentID == dialogueID.Answer)
        {
            // Answer
            currentLines = DialogueAnswerTwo.lines;
            currentID = dialogueID.Answertwo;
            // Set up next dialogue
            ChoicePanel.lastChoice.Reset();
            i = 0;
            // Show tutorial
            foreground.ShowTutorial(false);
        }

        else if (currentID == dialogueID.Answertwo)
        {
            // Answer
            currentLines = DialogueAnswerThree.lines;
            currentID = dialogueID.AnswerThree;
            // Set up next dialogue
            ChoicePanel.lastChoice.Reset();
            i = 0;
            // Show tutorial
            foreground.ShowTutorial(true);
        }

        else if (currentID == dialogueID.AnswerThree)
        {
            // Answer
            currentLines = DialogueTheNightThatRoyDied.lines;
            currentID = dialogueID.TheNightRoyDied;
            // Set up next dialogue
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }

        else if (currentID == dialogueID.TheNightRoyDied)
        {
            // Check if choice was made
            if (!ChoicePanel.lastChoice.madeSelection)
            {
                ChoicePanel.Show("Play the sympathy card", "Ask for more details");
            }
            else
            {
                // Set next lines of dialogue
                choiceIndex = ChoicePanel.lastChoice.index;
                if (choiceIndex == 0) { currentLines = DialogueSympathyCard.lines; currentID = dialogueID.SympathyCard; }   //Sympathy Card
                if (choiceIndex == 1) { currentLines = DialogueTellMeMore.lines; currentID = dialogueID.TellMeMore; }       //Tell me more
                // Continue dialogue
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
        }

        else if (currentID == dialogueID.TellMeMore)
        {
            // Set next lines of dialogue
            choiceIndex = ChoicePanel.lastChoice.index;
            currentLines = DialogueTellMeMore2.lines; currentID = dialogueID.TellMeMoreTwo;   // TellMeMoreTwo
            // Set up next dialogue
            ChoicePanel.lastChoice.Reset();
            i = 0;
            // Fade out image
            foreground.fadeOut();
        }

        else if (currentID == dialogueID.TellMeMoreTwo)
        {
            // Check if choice was made
            if (!ChoicePanel.lastChoice.madeSelection)
            {
                ChoicePanel.Show("Ask about the injuries", "Ask about the room's key");
            }
            else
            {
                // Set next lines of dialogue
                choiceIndex = ChoicePanel.lastChoice.index;
                if (choiceIndex == 0) { currentLines = DialogueInjuries.lines; currentID = dialogueID.Injuries; }   // Injuries
                if (choiceIndex == 1) { currentLines = DialogueKey.lines; currentID = dialogueID.Key; }       // Key
                // Continue dialogue
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
        }

        else if (currentID == dialogueID.Injuries)
        {
            // Info
            currentLines = DialogueInfo.lines;
            currentID = dialogueID.Info;
            // Continue dialogue
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }

        else if (currentID == dialogueID.Info)
        {
            // Phone
            currentLines = DialoguePhone.lines;
            currentID = dialogueID.Phone;
            // Continue dialogue
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }
        
        // Choice straight to silent ending
        else if (currentID == dialogueID.Key)
        {
            // Check if choice was made
            if (!ChoicePanel.lastChoice.madeSelection)
            {
                ChoicePanel.Show("I don't know him", "He was my Friend", "Stay silent");
            }
            else
            {
                // Set next lines of dialogue
                choiceIndex = ChoicePanel.lastChoice.index;
                if (choiceIndex == 0) { currentLines = DialogueDunno.lines; currentID = dialogueID.Dunno; }                // Dunno
                if (choiceIndex == 1) { currentLines = DialogueMyFriend.lines; currentID = dialogueID.MyFriend; }          // My Friend
                if (choiceIndex == 2) { currentLines = DialogueSilence.lines; currentID = dialogueID.Silence; }             // Silence
                // Continue dialogue
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
        }
        
        else if (currentID == dialogueID.SympathyCard)
        {
            // Set next lines of dialogue
            choiceIndex = ChoicePanel.lastChoice.index;
            currentLines = DialogueSympathyCardTwo.lines; currentID = dialogueID.SympathyCardTwo;   // TellMeMoreTwo
            // Set up next dialogue
            ChoicePanel.lastChoice.Reset();
            i = 0;
            // Fade out image
            foreground.fadeOut();
        }

        else if (currentID == dialogueID.SympathyCardTwo)
        {
            // Check if choice was made
            if (!ChoicePanel.lastChoice.madeSelection)
            {
                ChoicePanel.Show("He was my friend", "That's just disgusting");
            }
            else
            {
                // Set next lines of dialogue
                choiceIndex = ChoicePanel.lastChoice.index;
                if (choiceIndex == 0) { currentLines = DialogueMyFriend.lines; currentID = dialogueID.MyFriend; }       // He was my friend
                if (choiceIndex == 1) { currentLines = DialogueTellMeMore.lines; currentID = dialogueID.Disgusting; }   //That's disgusting
                // Continue dialogue
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
        }
        
        else if (currentID == dialogueID.Dunno)
        {
            // Photo
            currentLines = DialoguePhoto.lines;
            currentID = dialogueID.Photo;
            // Set up dialogue
            ChoicePanel.lastChoice.Reset();
            i = 0;
            // Show image
            foreground.showImage(Foreground.imageID.FakePhoto);
        }
        
        else if (currentID == dialogueID.MyFriend)
        {
            // Set flag
            flagTracker.setFlag("myFriend");

            // Check if choice was made
            if (!ChoicePanel.lastChoice.madeSelection)
            {
                ChoicePanel.Show("Make up a story", "You don't have to know");
            }
            else
            {
                // Set next lines of dialogue
                choiceIndex = ChoicePanel.lastChoice.index;
                if (choiceIndex == 0) { currentLines = DialogueOnlyThem.lines; currentID = dialogueID.OnlyThem; }                     // Only Them
                if (choiceIndex == 1) { currentLines = DialogueYouDontHaveToKnow.lines; currentID = dialogueID.Youdonthavetoknow; }   // You don't have to know
                // Continue dialogue
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
        }

        else if (currentID == dialogueID.Disgusting ||
                 currentID == dialogueID.OnlyThem)
        {
            // Go to Location,Location
            currentLines = DialogueLocationLocation.lines;
            currentID = dialogueID.LocationLocation;
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }

        else if (currentID == dialogueID.Youdonthavetoknow)
        {
            // Check if choice was made
            if (!ChoicePanel.lastChoice.madeSelection)
            {
                flagTracker.setFlag("suspect");
                ChoicePanel.Show("Make up a story", "Insist");
            }
            else
            {
                // Set next lines of dialogue
                choiceIndex = ChoicePanel.lastChoice.index;
                if (choiceIndex == 0) { currentLines = DialogueOnlyThem.lines; currentID = dialogueID.OnlyThem; }     // Only Them
                if (choiceIndex == 1) { currentLines = DialogueNoFriend.lines; currentID = dialogueID.NoFriend; }    // No Friend
                // Continue dialogue
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
        }

        else if (currentID == dialogueID.NoFriend)
        {
            // Phone
            currentLines = DialogueFalseCall.lines;
            currentID = dialogueID.FalseCall;
            // Continue dialogue
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }

        // Unused?
        else if (currentID == dialogueID.DontSpeak)
        {
            // Go to False Call
            currentLines = DialogueFalseCall.lines;
            currentID = dialogueID.FalseCall;
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }

        else if (currentID == dialogueID.FalseCall)        {
            if (flagTracker.getFlag("awhatnow"))
            {
                flagTracker.setFlag("myfriend");
                // Go to LocationLocation
                currentLines = DialoguePhone.lines;
                currentID = dialogueID.Phone;
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
            else
            {
                // Go to LocationLocation
                currentLines = DialogueLocationLocation.lines;
                currentID = dialogueID.LocationLocation;
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
        }

        else if (currentID == dialogueID.LocationLocation)
        {
            // If myFriend flag is active
            if (flagTracker.getFlag("myFriend"))
            {
                // Go to callout
                currentLines = DialogueCallout.lines;
                currentID = dialogueID.Callout;
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
            else
            {
                // Go to LocationContinuation
                currentLines = DialogueLocationContinuation.lines;
                currentID = dialogueID.LocationContinuation;
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
        }

        else if (currentID == dialogueID.LocationContinuation)
        {
            // Check if choice was made
            if (!ChoicePanel.lastChoice.madeSelection)
            {
                ChoicePanel.Show("The statue", "Stay silent", "Nothing special");
            }
            else
            {
                // Set next lines of dialogue
                choiceIndex = ChoicePanel.lastChoice.index;
                if (choiceIndex == 0) { currentLines = DialogueGoodThing.lines; currentID = dialogueID.GoodThing; }             // Good Thing
                if (choiceIndex == 1) { currentLines = DialogueSilence.lines; currentID = dialogueID.Silence; }                 // Silence
                if (choiceIndex == 2) { currentLines = DialogueNothingSpecial.lines; currentID = dialogueID.NothingSpecial; }   // Nothing Special

                // Continue dialogue
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
        }

        else if (currentID == dialogueID.SilentEnd)
        {
            ChoicePanel.Show("You got the good ending!");
        }

        else if(currentID == dialogueID.FightNightEnd)
        {
            ChoicePanel.Show("You got the neutral ending!");
        }

        else if (currentID == dialogueID.CooperateEnd)
        {
            ChoicePanel.Show("You got the bad ending...");
        }

        else if (currentID == dialogueID.GoodThing)
        {
            // If not suspicious, go to photo
            if (!flagTracker.getFlag("suspect"))
            {
                // Photo
                currentLines = DialoguePhoto.lines;
                currentID = dialogueID.Photo;
                // Set up dialogue
                ChoicePanel.lastChoice.Reset();
                i = 0;
                // Show image
                foreground.showImage(Foreground.imageID.FakePhoto);
            }
            // If suspicious, go to additional text
            else
            {
                // Go to FalseCall
                currentLines = DialogueGoodThingSuspicious.lines;
                currentID = dialogueID.GoodThingSuspicious;
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
        }

        else if (currentID == dialogueID.GoodThingSuspicious)
        {
            // Check if choice was made
            if (!ChoicePanel.lastChoice.madeSelection)
            {
                ChoicePanel.Show("What?", "How do you know that?");
            }
            else
            {
                // Set next lines of dialogue
                choiceIndex = ChoicePanel.lastChoice.index;

                if (choiceIndex == 0) { currentLines = DialogueAWhatNow.lines; currentID = dialogueID.AWhatNow; }     // AWhatNow
                if (choiceIndex == 1) { currentLines = DialogueLikeAnIdiot.lines; currentID = dialogueID.LikeAnIdiot; }     // LikeAnIdiot

                // Continue dialogue
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
        }

        else if (currentID == dialogueID.NothingSpecial)
        {
            // Go to Photo
            currentLines = DialoguePhoto.lines;
            currentID = dialogueID.Photo;
            ChoicePanel.lastChoice.Reset();
            i = 0;
            // Show image
            foreground.showImage(Foreground.imageID.FakePhoto);
        }
        
        else if (currentID == dialogueID.AWhatNow)
        {
            flagTracker.setFlag("awhatnow");
            // Go to FalseCall
            flagTracker.setFlag("suspect");
            currentLines = DialogueFalseCall.lines;
            currentID = dialogueID.FalseCall;
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }
        
        else if (currentID == dialogueID.LikeAnIdiot)
        {
            // Go to GoodThingSuspicious
            currentLines = DialogueExposed.lines;
            currentID = dialogueID.Expose;
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }

        else if (currentID == dialogueID.Callout)
        {
            //Go to phone
            flagTracker.setFlag("suspect");
            currentLines = DialoguePhone.lines;
            currentID = dialogueID.Phone;
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }

        else if (currentID == dialogueID.Photo)
        {
            // Check if choice was made
            if (!ChoicePanel.lastChoice.madeSelection)
            {
                ChoicePanel.Show("Believe it", "Look closer");
            }
            else
            {
                // Set next lines of dialogue
                choiceIndex = ChoicePanel.lastChoice.index;

                if (choiceIndex == 0) { currentLines = DialogueBelieveIt.lines; currentID = dialogueID.BelieveIt; }           // BelieveIt
                if (choiceIndex == 1) { currentLines = DialogueCloserLook.lines; currentID = dialogueID.CloserLook; }     // LookCloser

                // Set up dialogue
                ChoicePanel.lastChoice.Reset();
                i = 0;
                // Show image
                foreground.fadeOut();
            }
        }
        
        else if (currentID == dialogueID.BelieveIt)
        {
            // Go to GoodThingSuspicious
            currentLines = DialogueCloserLook.lines;
            currentID = dialogueID.CloserLook;
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }

        else if (currentID == dialogueID.CloserLook)
        {
            // Go to FalseCall
            currentLines = DialogueTheBrooch.lines;
            currentID = dialogueID.TheBrooch;
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }

        else if (currentID == dialogueID.TheBrooch)
        {
            // Go to Annoyed
            currentLines = DialogueAnnoyed.lines;
            currentID = dialogueID.Annoyed;
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }
        
        else if (currentID == dialogueID.Annoyed)
        {
            flagTracker.setFlag("annoyed");
            flagTracker.setFlag("suspect");
            // Check if choice was made
            if (!ChoicePanel.lastChoice.madeSelection)
            {
                // Show options
                if (flagTracker.getFlag("myFriend"))
                {
                    // Revealed Roy is your friend
                    // NeverSaid path locked
                    ChoicePanel.Show("Yes", "Stay shut");
                }
                else
                {
                    // Never revealed Roy is your friend
                    // Retort available
                    ChoicePanel.Show("Yes", "Stay shut", "I never said he was my friend");
                }
            }
            
            // Check option chosen
            else
            {
                choiceIndex = ChoicePanel.lastChoice.index;
                if (choiceIndex == 0) { currentLines = DialogueYes.lines; currentID = dialogueID.Yes; }                // Yes
                if (choiceIndex == 1) { currentLines = DialogueSilence.lines; currentID = dialogueID.Silence; }         // SilentEnd
                if (choiceIndex == 2) { currentLines = DialogueNeverSaid.lines; currentID = dialogueID.NeverSaid; }    // NeverSaid

                // Continue dialogue
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
        }

        else if (currentID == dialogueID.NeverSaid)
        {
            //Go to Expose
            currentLines = DialogueExposed.lines;
            currentID = dialogueID.Expose;
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }

        else if(currentID == dialogueID.Yes)
        {
            //Go to Expose
            currentLines = DialogueExposed.lines;
            currentID = dialogueID.Expose;
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }

        else if (currentID == dialogueID.Phone)
        {
            // Check if choice was made
            if (!ChoicePanel.lastChoice.madeSelection)
            {
                ChoicePanel.Show("Play along", "Call him out");
            }
            else
            {
                // Set next lines of dialogue
                choiceIndex = ChoicePanel.lastChoice.index;

                if (choiceIndex == 0) { currentLines = DialoguePlayAlong.lines; currentID = dialogueID.PlayAlong; } // PlayAlong
                if (choiceIndex == 1) { currentLines = DialogueExposed.lines; currentID = dialogueID.Expose; }      // Expose

                // Continue dialogue
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
        }
        
        // Excuse wires directly into annoyed
        else if (currentID == dialogueID.PlayAlong)
        {
            // Check if choice was made
            if (!ChoicePanel.lastChoice.madeSelection)
            {
                ChoicePanel.Show("Deny Claim", "Come up with an excuse", "Are you kidding me?");
            }
            else
            {
                // Set next lines of dialogue
                choiceIndex = ChoicePanel.lastChoice.index;

                if (choiceIndex == 0) { currentLines = DialogueDenyClaim.lines; currentID = dialogueID.DenyClaim; }     // Deny Claim
                else if (choiceIndex == 1) { currentLines = DialogueAnnoyed.lines; currentID = dialogueID.Annoyed; }    // Excuse
                else if (choiceIndex == 2) { currentLines = DialogueFreakOut.lines; currentID = dialogueID.FreakOut; }  // Freak Out

                // Continue dialogue
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
        }

        else if (currentID == dialogueID.DenyClaim)
        {
            //Go to silent ending
            currentLines = DialogueSilence.lines;
            currentID = dialogueID.Silence;
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }

        else if (currentID == dialogueID.FreakOut)
        {
            // Go to tumbling down
            currentLines = DialogueTumblingDownAbridged.lines;
            currentID = dialogueID.TumblingDownAbridged;
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }

        else if (currentID == dialogueID.Expose)
        {
            // Check if annoyed
            if (flagTracker.getFlag("annoyed"))
            {
                //Go to ExposeAnnoyed
                currentLines = DialogueExposeAnnoyed.lines;
                currentID = dialogueID.ExposeAnnoyed;
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
            // Check if Suspect
            else if (flagTracker.getFlag("suspect"))
            {
                //Go to ExposeSuspect
                currentLines = DialogueExposeSuspect.lines;
                currentID = dialogueID.ExposeSuspect;
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
            //Continue
            else
            {
                //Go to ExposeContinuation
                currentLines = DialogueExposeContinuation.lines;
                currentID = dialogueID.ExposeContinuation;
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
        }

        else if (currentID == dialogueID.ExposeAnnoyed)
        {
            // Check if Suspect
            if (flagTracker.getFlag("suspect"))
            {
                //Go to ExposeSuspect
                currentLines = DialogueExposeSuspect.lines;
                currentID = dialogueID.ExposeSuspect;
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
            //Continue
            else
            {
                //Go to ExposeContinuation
                currentLines = DialogueExposeContinuation.lines;
                currentID = dialogueID.ExposeContinuation;
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
        }

        else if (currentID == dialogueID.ExposeSuspect)
        {
            //Go to ExposeContinuation
            currentLines = DialogueExposeContinuation.lines;
            currentID = dialogueID.ExposeContinuation;
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }

        else if (currentID == dialogueID.ExposeContinuation)
        {
            // Check if choice was made
            if (!ChoicePanel.lastChoice.madeSelection)
            {
                ChoicePanel.Show("Confess", "Brawl it out!");
            }
            else
            {
                // Set next lines of dialogue
                choiceIndex = ChoicePanel.lastChoice.index;

                if (choiceIndex == 0) { currentLines = DialogueConfess.lines; currentID = dialogueID.Confess; }              // Confess
                if (choiceIndex == 1) { currentLines = DialogueTumblingDown.lines; currentID = dialogueID.TumblingDown; }    // TumblingDown

                // Continue dialogue
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
        }
        
        else if (currentID == dialogueID.Confess)
        {
            //Go to Cooperation Ending
            currentLines = EndingCooperate.lines;
            currentID = dialogueID.CooperateEnd;
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }
        
        else if (currentID == dialogueID.TumblingDown ||
                 currentID == dialogueID.TumblingDownAbridged)
        {
            //Go to Fight Night Ending
            currentLines = EndingFightNight.lines;
            currentID = dialogueID.FightNightEnd;
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }


        // ======================================================

        // Unused
        else if (currentID == dialogueID.Silence)
        {
            //Go to Silent Ending
            currentLines = EndingSilent.lines;
            currentID = dialogueID.SilentEnd;
            ChoicePanel.lastChoice.Reset();
            i = 1;
            Say(currentLines[0]);
        }
        else if (currentID == dialogueID.SilenceTwo)
        {
            // Check if choice was made
            if (!ChoicePanel.lastChoice.madeSelection)
            {
                ChoicePanel.Show("Good thing", "Silence", "Nothing Special");
            }
            else
            {
                // Set next lines of dialogue
                choiceIndex = ChoicePanel.lastChoice.index;
                if (choiceIndex == 0) { currentLines = DialogueGoodThing.lines; currentID = dialogueID.GoodThing; }             // Good Thing
                if (choiceIndex == 1) { currentLines = DialogueSilence3.lines; currentID = dialogueID.SilenceThree; }                 // Silence
                if (choiceIndex == 2) { currentLines = DialogueNothingSpecial.lines; currentID = dialogueID.NothingSpecial; }   // Nothing Special

                // Continue dialogue
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
        }
        else if (currentID == dialogueID.SilenceThree)
        {
            // Check if choice was made
            if (!ChoicePanel.lastChoice.madeSelection)
            {
                ChoicePanel.Show("Good thing", "Silence", "Nothing Special");
            }
            else
            {
                // Set next lines of dialogue
                choiceIndex = ChoicePanel.lastChoice.index;
                if (choiceIndex == 0) { currentLines = DialogueGoodThing.lines; currentID = dialogueID.GoodThing; }             // Good Thing
                if (choiceIndex == 1) { currentLines = DialogueSilence.lines; currentID = dialogueID.Silence; }                  // Silent Ending
                if (choiceIndex == 2) { currentLines = DialogueNothingSpecial.lines; currentID = dialogueID.NothingSpecial; }   // Nothing Special

                // Continue dialogue
                ChoicePanel.lastChoice.Reset();
                i = 1;
                Say(currentLines[0]);
            }
        }


        //Unreachable
        else if (currentID == dialogueID.Excuse)
        {

        }

        // ======================================================
    }
}
