using System;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class PlayerMovementHandler : MonoBehaviour
    {
        [SerializeField, Range(100, 2000)] private int JumpForce;
        [SerializeField, Range(1, 10)] private int GroundedDistance;
        [SerializeField, Range(1, 10)] private int MovementSpeed;

        private Animator m_Animator;
        private Rigidbody2D m_Rigidbody2D;
        private SpriteRenderer m_SpriteRenderer;

        private bool _spriteFlipped;

        void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            m_Rigidbody2D.velocity = new Vector2((MovementSpeed * 100) *
                                                 GetControls().x *
                                                 Time.deltaTime, m_Rigidbody2D.velocity.y);
            
            m_Animator.SetFloat("Movement", Mathf.Abs(m_Rigidbody2D.velocity.x));

            if (GetControls().x < 0 && !_spriteFlipped)
                _spriteFlipped = true;
            else if (GetControls().x > 0 && _spriteFlipped)
                _spriteFlipped = false;

            m_SpriteRenderer.flipX = _spriteFlipped;
        }

        void Update()
        {
            m_Animator.SetBool("Jump",!IsGrounded());
            if (IsGrounded() && Input.GetButtonDown("Jump"))
            {
                m_Rigidbody2D.AddForce(Vector2.up * JumpForce);
            }
            
            if(Input.GetButtonDown("Fire1")) 
                m_Animator.SetTrigger("Attack");
        }

        private bool IsGrounded()
        {
            int layerMask = 1 << 9;
            layerMask = ~layerMask;

            return Physics2D.Raycast(transform.position, Vector2.down, GroundedDistance, layerMask);
        }

        private Vector2 GetControls()
        {
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }
}
