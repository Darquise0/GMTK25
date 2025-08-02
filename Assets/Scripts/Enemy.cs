using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float movementSpeed = 2f;
    Rigidbody2D rb;
    Collider2D enemyCollider;
    [SerializeField] public Transform target;
    [SerializeField] public Collider2D lightCollider;
    Vector2 moveDirection;

    // Variables added (yas)
    public AudioSource rustle;
    [SerializeField] private Collider2D chaseTrigger;
    private bool hasMadeDecision = false;
    private bool wasTouching = false;
    private bool isInSpotlightTrigger = false;
    private bool isInLight = false;
    private float drainTimer = 0f;
    public float drainDelay = 1f;
    private bool isCommittedToChase = false;
    private Vector3 startPosition;

    public Lights playerLights;
    public Image healthBarFillImage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        startPosition = transform.position;

        GameObject player = Global.playerInstance;
        target = player.transform;
        playerLights = player.GetComponent<Lights>();
        lightCollider = playerLights.playerLight.gameObject.GetComponent<Collider2D>();
        healthBarFillImage = GameObject.Find("Canvas").transform.GetChild(2).transform.GetChild(2).GetComponent<Image>();
    }

    void Update()
    {
        bool isTouching = lightCollider.IsTouching(enemyCollider);

        if (isTouching && !wasTouching)
        {
            rustle.Play();
        }

        wasTouching = isTouching;

        if (isCommittedToChase && target && lightCollider != null && enemyCollider != null && !lightCollider.IsTouching(enemyCollider))
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;

            // Can be used to rotate the enemy when the follow:
            // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // rb.rotation = angle;
        }
        else
        {
            moveDirection = Vector2.zero;
        }

        if (isInSpotlightTrigger && !isInLight && playerLights != null)
        {
            drainTimer += Time.deltaTime;
            if (drainTimer >= drainDelay)
            {
                playerLights.DimPlayerLight();
                drainTimer = 0f;
            }
        }
        else
        {
            drainTimer = 0f;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * movementSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (lightCollider.IsTouching(enemyCollider) && !hasMadeDecision)
        {
            Debug.Log("Entered flashlight, trying conversion");
            TryConvertToAnimal();
        }

        if (other.CompareTag("Player"))
        {
            if (chaseTrigger != null && chaseTrigger.IsTouching(other))
            {
                Debug.Log("Player entered chase zone");
                CommitToChase();
            }
        }


        if (other.CompareTag("PlayerSpotlightTrigger"))
        {
            isInSpotlightTrigger = true;
        }

        isInLight = false;

        if (lightCollider != null && enemyCollider != null)
        {
            if (lightCollider.CompareTag("Flashlight") && lightCollider.IsTouching(enemyCollider))
            {
                isInLight = true;
            }
        }
    }
    public void CommitToChase()
    {
        isCommittedToChase = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerSpotlightTrigger"))
        {
            isInSpotlightTrigger = false;
        }
    }

    private void TryConvertToAnimal()
    {
        hasMadeDecision = true;

        if (Random.value > 0.4f) return;

        Debug.Log("converted");

        string prefabName = Random.value > 0.9f ? "Bunny" : "Deer";
        GameObject animalPrefab = Resources.Load<GameObject>($"Prefabs/{prefabName}");

        if (animalPrefab == null)
        {
            Debug.LogError($"Missing prefab: Resources/Animals/{prefabName}.prefab");
            return;
        }

        GameObject animal = Instantiate(animalPrefab, transform.position, Quaternion.identity);
        AnimalFlee fleeScript = animal.GetComponent<AnimalFlee>();
        if (fleeScript != null && target != null)
        {
            fleeScript.SetFleeTarget(target.position);
        }
        
        Destroy(gameObject);
    }

    public void ReturnToStart()
    {
        transform.position = startPosition;
        moveDirection = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        isCommittedToChase = false;
    }
}
