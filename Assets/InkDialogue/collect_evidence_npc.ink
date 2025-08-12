=== collectEvidence ===
{ CollectEvidenceQuestState :
    - "REQUIREMENTS_NOT_MET": -> requirementsNotMet
    - "CAN_START": -> canStart
    - "IN_PROGRESS": -> inProgress
    - "CAN_FINISH": -> canFinish
    - "FINISHED": -> finished
    - else: -> END
}

= requirementsNotMet
// Not possible for this quest, but putting something here anyways
You do not meet the requirements for this quest.
-> END

= canStart
Will you collect 2 pieces of evidence and bring them back to me?
* [Yes]
    ~ StartQuest("CollectEvidenceQuest")
    Great! Thank you!
* [No]
    Oh, ok then. Come back if you change your mind.
- -> END

= inProgress
How is collecting that evidence going?
-> END

= canFinish
Oh? You collected the evidence? Good job! You can hand it over to me now.
* [Not right now]
    Alright, come back when you’re ready.
    -> END
* [Hand over evidence]
    ~ FinishQuest(CollectEvidenceQuestId)
    Thank you so much!
    -> END

= finished
Thanks again for collecting that evidence!
-> END