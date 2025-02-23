using System;
using UnityEngine;

namespace _Scripts.Core
{
    public class VampireImproved : MonoBehaviour
    {
        private VampireState _state;
        private Animator _animator;
        private BoxCollider2D _collider;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _collider = GetComponentInChildren<BoxCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
        }


        private void Start()
        {
            _state = VampireState.Bat;
            _animator.Play("batflying");

        }

        private void Update()
        {
            switch (_state)
            {
                case VampireState.Bat:
                    break;
                case VampireState.Transforming:
                    break;
                case VampireState.Grounded:
                    break;
                case VampireState.Burning:
                    _animator.SetTrigger("Burn");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        enum VampireState
        {
            Bat,
            Transforming,
            Grounded,
            Burning
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_state == VampireState.Bat && other.gameObject.CompareTag("Ground"))
            {
                _state = VampireState.Transforming;
                _animator.Play("transform");
                var transformingTime = _animator.GetCurrentAnimatorStateInfo(0).length;
                Invoke(nameof(SetGrounded), transformingTime);
            }

            if (_state == VampireState.Grounded && other.gameObject.CompareTag("Bolt"))
            {
                Debug.Log("Vampire hit by bolt");
                Burn();
                Destroy(other.gameObject);
            }
        }

        private void Burn()
        {
            Debug.Log("Vampire burned");
            _state = VampireState.Burning;
            _animator.SetTrigger("Burn");
            _animator.Play("burn");
            _collider.enabled = false;
            _rigidbody.linearVelocityX = 0;
        }

        private void SetGrounded()
        {
            _animator.Play("idle");
            _state = VampireState.Grounded;
        }
    }
}