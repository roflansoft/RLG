using UnityEngine;


public class MoveObject : MonoBehaviour
{
    private bool _direction = true;
    
    public Vector2 moveVector;
    
    public float speed = 3f;
    
    public Rigidbody2D objectRigidbody;

    private SpriteRenderer _spriteRenderer;
    
    public Animator animator;
    
    private void Start() {
        objectRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        walk();
    }

    void walk() {
        moveVector.x = Input.GetAxis("Horizontal");


        objectRigidbody.velocity = new Vector2(moveVector.x * speed, objectRigidbody.velocity.y);
        

        moveVector.y = Input.GetAxis("Vertical");

        if (moveVector.x == 0 && moveVector.y == 0)
        {
            animator.SetFloat("Sp", 0);
        }
        else
        {
            animator.SetFloat("Sp", 1);
        }
        objectRigidbody.velocity = new Vector2(objectRigidbody.velocity.x, moveVector.y * speed);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 myPos = transform.position;
        switch (moveVector.x)
        {
            case > 0 when _direction == false:
            case < 0 when _direction == true:
                Flip();
                break;
        }
    }

    private void Flip()
    {
        _direction = !_direction;
        var theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
