using UnityEngine;
using UnityEngine.InputSystem;
using Ink.Runtime;
using System.Collections;
using System;

public class DialogueManager : MonoBehaviour
{
    [Header("Ink Story")]
    [SerializeField] private TextAsset inkJson;

    private Story story;
    private int currentChoiceIndex = -1;

    private bool dialoguePlaying = false;

    private InkExternalFunctions inkExternalFunctions;
    private InkDialogueVariables inkDialogueVariables;

    private void Awake()
    {
        story = new Story(inkJson.text);
        inkExternalFunctions = new InkExternalFunctions();
        inkExternalFunctions.Bind(story);
        inkDialogueVariables = new InkDialogueVariables(story);
    }

    private void OnDestroy()
    {
        inkExternalFunctions.Unbind(story);
    }

    private void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue += EnterDialogue;
        InputManager.OnInteract += SubmitPressed;
        GameEventsManager.instance.dialogueEvents.onUpdateChoiceIndex += UpdateChoiceIndex;
        GameEventsManager.instance.dialogueEvents.onUpdateInkDialogueVariable += UpdateInkDialogueVariable;
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue -= EnterDialogue;
        InputManager.OnInteract -= SubmitPressed;
        GameEventsManager.instance.dialogueEvents.onUpdateChoiceIndex -= UpdateChoiceIndex;
        GameEventsManager.instance.dialogueEvents.onUpdateInkDialogueVariable -= UpdateInkDialogueVariable;
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    private void SubmitPressed()
    {
        // If dialogue isn't playing, we never want to register input here
        if (!dialoguePlaying)
        {
            return;
        }

        ContinueOrExitStory();
    }

    private void UpdateChoiceIndex(int choiceIndex)
    {
        this.currentChoiceIndex = choiceIndex;
    }

    private void UpdateInkDialogueVariable(string name, Ink.Runtime.Object value)
    {
        inkDialogueVariables.UpdateVariableState(name, value);
    }

    private void QuestStateChange(Quest quest)
    {
        GameEventsManager.instance.dialogueEvents.UpdateInkDialogueVariable(quest.info.id + "State", new StringValue(quest.state.ToString()));
    }

    private void EnterDialogue(string knotName)
    {
        // Don't enter dialogue if we've already entered
        if (dialoguePlaying)
        {
            return;
        }

        dialoguePlaying = true;

        // Inform other parts of our system that we've started dialogue
        GameEventsManager.instance.dialogueEvents.DialogueStarted();

        // Freeze player movement
        PlayerMovement.frozen = true;

        // Jump to the knot
        if (!knotName.Equals(""))
        {
            story.ChoosePathString(knotName);
        }
        else
        {
            Debug.LogWarning("Knot name was the empty string when entering dialogue.");
        }

        // Start listening for variables
        inkDialogueVariables.SyncVariablesAndStartListening(story);

        // Kick off the story
        ContinueOrExitStory();
    }

    private void ContinueOrExitStory()
    {
        // Make a choice, if applicable
        if (story.currentChoices.Count > 0 && currentChoiceIndex != -1)
        {
            story.ChooseChoiceIndex(currentChoiceIndex);
            currentChoiceIndex = -1;
        }

        if (story.canContinue)
        {
            string dialogueLine = story.Continue();

            // Handle the case where there's an empty line of dialogue by continuing until we get a line with content
            while (IsLineBlank(dialogueLine) && story.canContinue)
            {
                dialogueLine = story.Continue();
            }

            // Handle the case where the last line of dialogue is blank (empty choice, external function, etc...)
            if (IsLineBlank(dialogueLine) && !story.canContinue)
            {
                ExitDialogue();
            }
            else
            {
                GameEventsManager.instance.dialogueEvents.DisplayDialogue(dialogueLine, story.currentChoices);
            }
        }
        else if (story.currentChoices.Count == 0)
        {
            StartCoroutine(ExitDialogue());
        }
    }

    private IEnumerator ExitDialogue()
    {
        yield return null;

        dialoguePlaying = false;

        // Inform other parts of our system that we've finished dialogue
        GameEventsManager.instance.dialogueEvents.DialogueFinished();

        // Unfreeze player movement
        PlayerMovement.frozen = false;

        // Stop listening for dialogue variables        
        inkDialogueVariables.StopListening(story);

        story.ResetState();
    }

    private bool IsLineBlank(string dialogueLine)
    {
        return dialogueLine.Trim().Equals("") || dialogueLine.Trim().Equals("\n");
    }
}
