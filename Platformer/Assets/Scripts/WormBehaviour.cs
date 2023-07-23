using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBehaviour : Entity
{
    [SerializeField] private int _lives = 3;
    private SpriteRenderer _sprite;
    private Vector3 _direction;


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
        FlipSprite(_direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            _lives--;
            Hero.Instance.GetDamage();
            Debug.Log(_lives);
        }

        if(_lives < 1)
        {
            Die();
        }
    }

    public override void FlipSprite(Vector3 dir)
    {
        _sprite.flipX = (_sprite.transform.position.x - Hero.Instance.transform.position.x) < 0;
    }
}
