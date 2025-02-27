using UnityEngine;

namespace _Scripts.Core
{
    public class NPC : MonoBehaviour
    {
        protected Animator _animator;
        protected BoxCollider2D _collider;
        protected Rigidbody2D _rigidbody;
        protected SpriteRenderer _spriteRenderer;
        protected Transform _target;
        protected AudioSource _audioSource;
        protected int _dir;
    }
}