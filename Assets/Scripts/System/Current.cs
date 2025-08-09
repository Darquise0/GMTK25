using System.Collections;
using UnityEngine;

public class Current : MonoBehaviour
{
    public static Current Instance { get; private set; }

    // Shared blob that other scripts read/write into.
    public static SaveData CurrentSave;

    [Header("Debug")]
    [Tooltip("Tick to force a save this frame (for testing).")]
    public bool save;

    [Tooltip("Optional: a component that knows how to push its state into CurrentSave immediately.")]
    public Lights lights;

    // Internal guards
    private bool _isLoading = false;
    private bool _isReadyToSave = false;
    private bool _saveQueued = false;

    private void Awake()
    {
        // Singleton + persist
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Load once at boot
        _isLoading = true;
        CurrentSave = SaveSystem.OnLoad() ?? new SaveData();
        _isLoading = false;
    }

    private void Start()
    {
        // Flip this once all Awake()s have run so early lifecycle autosaves don't write empty data.
        _isReadyToSave = true;
    }

    private void Update()
    {
        // Manual test save from the Inspector
        if (save)
        {
            save = false; // consume the click so it only fires once
            if (lights != null) lights.SaveGameNow(); // push any state into CurrentSave
            RequestSave(); // coalesced save
        }
    }

    /// <summary>
    /// Preferred entry point: queue a save once this frame.
    /// Anyone can call this; it won't spam PlayerPrefs.
    /// </summary>
    public static void RequestSave()
    {
        if (Instance == null) return;
        if (Instance._saveQueued) return;

        Instance._saveQueued = true;
        Instance.StartCoroutine(Instance.SaveAtEndOfFrame());
    }

    private IEnumerator SaveAtEndOfFrame()
    {
        // Wait until everything for this frame updated CurrentSave
        yield return new WaitForEndOfFrame();
        _saveQueued = false;
        SaveGame(); // actual write
    }

    /// <summary>
    /// Immediate save. Use RequestSave() unless you really need to force it now.
    /// </summary>
    public static void SaveGame()
    {
        if (Instance == null) return;

        // Guard against early/duplicate/unsafe saves
        if (Instance._isLoading || !Instance._isReadyToSave)
        {
            Debug.Log("[Save] Blocked: loading or not ready yet.");
            return;
        }

        // Do the actual persistence
        SaveSystem.OnSave(CurrentSave);
        // Optionally log once:
        // Debug.Log($"[Save] Game Saved: {JsonUtility.ToJson(CurrentSave)}");
    }

    /// <summary>
    /// Call this *before* writing to CurrentSave if you are doing a blocking load.
    /// </summary>
    public static void BeginLoad()
    {
        if (Instance == null) return;
        Instance._isLoading = true;
        Instance._isReadyToSave = false;
    }

    /// <summary>
    /// Call this after you finish distributing loaded data to all systems.
    /// </summary>
    public static void EndLoad()
    {
        if (Instance == null) return;
        Instance._isLoading = false;
        Instance._isReadyToSave = true;
    }
}
