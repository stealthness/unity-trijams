using System;
using _Scripts.Managers;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public Sprite OpenDoorSprite;
    public Sprite ClosedDoorSprite;
    
    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Start()
    {
        _spriteRenderer.sprite = ClosedDoorSprite;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has reached the exit");
            _spriteRenderer.sprite = OpenDoorSprite;
            GameManager.Instance.LevelComplete();
        }
    }
}
