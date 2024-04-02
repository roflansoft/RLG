using UnityEngine;
using UnityEngine.EventSystems;

public class Clicker : MonoBehaviour, IPointerClickHandler
{
    private BoxCollider2D _boxCollider2D;

    private SpriteRenderer _spriteRenderer;

    private Transform _objectPos;

    private Collider2D _collision;

    public Transform playerPos;

    public Sprite open;

    public Sprite close;

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _objectPos = GetComponent<Transform>();
        _collision = GetComponent<Collider2D>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        double sizeObjPlay = Mathf.Sqrt(
            (playerPos.position.x - _objectPos.position.x) * (playerPos.position.x - _objectPos.position.x) +
            (playerPos.position.y - _objectPos.position.y) * (playerPos.position.y - _objectPos.position.y));
        if (!(0.9f < sizeObjPlay) || !(sizeObjPlay < 1.8f)) return;
        if (_boxCollider2D.isTrigger == false)
        {
            Debug.Log("Open**");
            _boxCollider2D.isTrigger = true;
            _spriteRenderer.sprite = open;
            _collision.tag = "Open_door";
        }
        else
        {
            Debug.Log("Close**");
            _boxCollider2D.isTrigger = false;
            _spriteRenderer.sprite = close;
            _collision.tag = "buildings";
        }
    }
}
