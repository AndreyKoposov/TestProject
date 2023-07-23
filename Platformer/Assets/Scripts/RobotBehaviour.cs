using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehaviour : Entity
{
    private float _speed = 3f;
    private Vector3 _direction;
    private SpriteRenderer _sprite;


    private void Awake()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _direction = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.1f + transform.right * 0.5f * _direction.x, 0.1f);

        if(colliders.Length > 1)
        {
            _direction *= -1f;
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, Time.deltaTime);

        FlipSprite(_direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
        }
    }

    public override void FlipSprite(Vector3 dir)
    {
        _sprite.flipX = dir.x > 0;
    }
}
