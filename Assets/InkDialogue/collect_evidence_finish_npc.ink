=== collectEvidenceFinish ===
{ CollectEvidenceQuestState:
    - "FINISHED": -> finished
    - else: -> default
}

= finished
Thank you!
-> END

= default
Hm? What do you want?
* [Nothing, I guess.]
    -> END
* { CollectEvidenceQuestState == "CAN_FINISH" } [Here is some evidence.]
    ~ FinishQuest(CollectEvidenceQuestId)
    Oh? This evidence is for me? Thank you!
-> END