using UnityEditor;
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
    public AudioSource breathing;
    [SerializeField] private Collider2D chaseTrigger;
    [HideInInspector] public bool hasMadeDecision = false;
    private Animator animator;
    private string prefabType;
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
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        startPosition = transform.position;

        prefabType = Random.value > 0.5f ? "Bunny" : "Deer";
        if (prefabType == "Bunny")
        {
            animator.SetBool("isBunny", true);
        }

        GameObject player = Global.playerInstance;
        target = player.transform;
        playerLights = player.GetComponent<Lights>();
        lightCollider = playerLights.playerLight.gameObject.GetComponent<Collider2D>();
        healthBarFillImage = GameObject.Find("Canvas").transform.GetChild(2).transform.GetChild(2).GetComponent<Image>();
        if (target != null)
        {
            animator.SetBool("isRight", target.position.x > transform.position.x);
        }
    }

    void Update()
    {
        // if (!hasMadeDecision && hasConverted)
        // {
        //     Debug.Log("Trying conversion from Update");
        //     TryConvertToAnimal();
        // }

        bool isMoving = moveDirection != Vector2.zero;
        animator.SetBool("isMoving", isMoving);

        if (isMoving)
        {
            animator.SetBool("isRight", moveDirection.x > 0f);
        }

        bool isTouching = lightCollider.IsTouching(enemyCollider);

        if (isTouching && !wasTouching)
        {
            breathing.Play();
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
        // if (lightCollider.IsTouching(enemyCollider) && !hasMadeDecision)
        // {
        //     Debug.Log("Entered flashlight, trying conversion");
        //     TryConvertToAnimal();
        // }

        // if (other.CompareTag("Player"))
        // {
        //     if (chaseTrigger != null && chaseTrigger.IsTouching(other))
        //     {
        //         Debug.Log("Player entered chase zone");
        //         CommitToChase();
        //     }
        // }

        CommitToChase();


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
            breathing.Stop();
            isInSpotlightTrigger = false;
        }
    }

    public void TryConvertToAnimal()
    {
        hasMadeDecision = true;

        if (Random.value > 0.3f) return;

        Debug.Log("converted");

        string prefabName = prefabType;
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
