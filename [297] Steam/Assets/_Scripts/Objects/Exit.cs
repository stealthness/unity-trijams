using System;
using _Scripts.Managers;
using UnityEngine;

public class Exit : MonoBehaviour
{
    
    public static Exit Instance;
    
    
    public Sprite OpenDoorSprite;
    public Sprite ClosedDoorSprite;
    [SerializeField] private int _enigineCount;
    
    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        if (Exit.Instance == null)
        {
            Exit.Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Start()
    {
        _enigineCount = 3;
        _spriteRenderer.sprite = ClosedDoorSprite;
    }
    
    public void EngineFixed()
    {
        _enigineCount--;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_enigineCount <= 0)
            {
                            Debug.Log("Player has reached the exit");
                            _spriteRenderer.sprite = OpenDoorSprite;
                            GameManager.Instance.LevelComplete();
            }
            else
            {
                StartMenuManager.Instance.ShowMessage("You need to fix 3 engines to open the door");
            }

        }
    }
}
