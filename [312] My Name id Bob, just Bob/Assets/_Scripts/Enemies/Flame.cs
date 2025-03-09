
using _Scripts.Core;
using UnityEngine;

namespace _Scripts.Enemies
{
    
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Animator))]
    public class Flame : Movement2D
    {
        
        [SerializeField] private float _initialStartDelay = 1f;
        private Animator _animator;
        private const float Gravity = 9.81f;
        [SerializeField] private float _initialJumpSpeed = 10f;
        private Vector3 _initialPosition;
        private bool _isHidden = false;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }


        private void Start()
        {
            _dir = new Vector2(0, 1);
            _initialPosition = transform.position;
            transform.position = new Vector3(1000, 1000, 1000);
            _isHidden = true;
            Invoke(nameof(PreJump), Random.Range(_initialStartDelay,_initialStartDelay + 5f) );
            
        }

        private void Update()
        {
            if (_isHidden)
            {
                return;
            }
            
            speed += -Gravity * Time.deltaTime;
            transform.Translate(_dir  * (speed * Time.deltaTime));
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Flame::OnTriggerEnter " + other.tag);
            if (other.CompareTag("Player"))
            {
                Debug.Log("Flame::OnTriggerEnter");
                other.GetComponent<Player.PlayerController>().OnHit();
            }
            if (other.CompareTag("Boundary"))
            {
                Debug.Log("Flame::OnTriggerEnter::Boundary");
                Hide();
            }
        }
        
        
        private void Jump()
        {
            _dir = new Vector2(0, 1);
            speed = _initialJumpSpeed;
            _isHidden = false;
        }

        private void PreJump()
        {
            transform.position = _initialPosition;
            Invoke(nameof(Jump), 1f);
        }

        private void Hide()
        {
            transform.position = new Vector3(1000, 1000, 1000);
            _isHidden = true;
            Invoke(nameof(PreJump), Random.Range(3,10) );
        }
    }
}