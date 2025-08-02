using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Tilemap grassTilemap;
    [SerializeField] private Tilemap pathTilemap;

    private Vector2 _movement;

    public AudioSource grass;
    public AudioSource path;

    private Rigidbody2D _rb;
    private Animator _animator;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lastHorizontal = "LastHorizontal";
    private const string _lastVertical = "LastVertical";

    public static bool frozen;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!frozen)
        {
            _movement.Set(InputManager.Movement.x, InputManager.Movement.y);
            _rb.linearVelocity = _movement * _moveSpeed;

            _animator.SetFloat(_horizontal, _movement.x);
            _animator.SetFloat(_vertical, _movement.y);

            if (_movement != Vector2.zero)
            {
                HandleFootstepSound();

                _animator.SetFloat(_lastHorizontal, _movement.x);
                _animator.SetFloat(_lastVertical, _movement.y);
            }
            else
            {
                StopAllFootsteps();
            }
        }
        else
        {
            _rb.linearVelocity = Vector2.zero;
            StopAllFootsteps();
        }
    }

    private void HandleFootstepSound()
    {
        Vector3 worldPos = transform.position;
        Vector3Int cellPos = grassTilemap.WorldToCell(worldPos);

        if (pathTilemap.HasTile(cellPos))
        {
            if (!path.isPlaying)
            {
                path.Play();
                if (grass.isPlaying) grass.Stop();
            }
        }
        else if (grassTilemap.HasTile(cellPos))
        {
            if (!grass.isPlaying)
            {
                grass.Play();
                if (path.isPlaying) path.Stop();
            }
        }
        else
        {
            StopAllFootsteps();
        }
    }

    private void StopAllFootsteps()
    {
        if (grass.isPlaying) grass.Stop();
        if (path.isPlaying) path.Stop();
    }

    public static void freeze() => frozen = true;
    public static void unfreeze() => frozen = false;
}
