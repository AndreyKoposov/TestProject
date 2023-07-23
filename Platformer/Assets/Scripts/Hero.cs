using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    idle,
    run,
    jump,
    fall
}

public class Hero : Entity
{
    public static Hero Instance
    {
        get; 
        set;
    }

    [SerializeField] private float _speed = 3f;
    [SerializeField] private int _lives = 5;
    [SerializeField] private float _jumpForce = 4f;
    private bool _isGrounded = true;

    private Rigidbody2D _body;
    private SpriteRenderer _sprite;
    private Animator _anim;

    public States State
    {
        get { return (States)_anim.GetInteger("State"); }
        set { _anim.SetInteger("State", (int)value); }
    }

    private void Awake()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        Instance = this;
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        if(_isGrounded) State = States.idle;

        if (Input.GetButton("Horizontal"))
        {
            Run();
        }
        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (isFalling())
        {
            if (!_isGrounded) State = States.fall;
        }
        else
        {
            if (!_isGrounded) State = States.jump;
        }

    }

    private void FixedUpdate()
    {
        CheckGrounded();
    }

    private void Run()
    {
        if (_isGrounded) State = States.run;

        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        Vector3 pos = transform.position;

        transform.position = Vector3.MoveTowards(pos, pos + direction, _speed * Time.deltaTime);

        FlipSprite(direction);
    }

    public override void FlipSprite(Vector3 dir)
    {
        _sprite.flipX = dir.x < 0;
    }

    private void Jump()
    {
        _body.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.8f);
        _isGrounded = colliders.Length > 1;
    }

    private bool isFalling()
    {
        return _body.velocity.y < 0;
    }

    public override void GetDamage()
    {
        _lives--;
        Debug.Log(_lives);

        if(_lives < 1)
        {
            Die();
        }
    }

    public override void Die()
    {
        this.gameObject.SetActive(false);
    }
}
