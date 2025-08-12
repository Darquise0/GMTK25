=== collectEvidenceStart ===
{ CollectEvidenceQuestState :
    - "REQUIREMENTS_NOT_MET": -> requirementsNotMet
    - "CAN_START": -> canStart
    - "IN_PROGRESS": -> inProgress
    - "CAN_FINISH": -> canFinish
    - "FINISHED": -> finished
    - else: -> END
}

= requirementsNotMet
Come back once you've met the requirements for the quest.
-> END

= canStart
Will you collect 2 pieces of evidence and bring them to my friend over there?
* [Yes]
    ~ StartQuest(CollectEvidenceQuestId)
    Great!
* [No]
    Oh, ok then. Come back if you change your mind.
- -> END

= inProgress
How is collecting that evidence going?
-> END

= canFinish
Oh? You collected the evidence? Give them to my friend down there.
-> END

= finished
Thanks for collecting that evidence!
-> END