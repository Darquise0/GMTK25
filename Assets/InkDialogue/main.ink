// external functions
EXTERNAL StartQuest(questId)
EXTERNAL AdvanceQuest(questId)
EXTERNAL FinishQuest(questId)

// quest names (questId + "Id" for variable name)
VAR CollectEvidenceQuestId = "CollectEvidenceQuest"

// quest states (questId + "State" for variable name)
VAR CollectEvidenceQuestState = "REQUIREMENTS_NOT_MET"

// ink files
INCLUDE collect_evidence_npc.ink