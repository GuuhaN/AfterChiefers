using System;
using UnityEngine;

namespace Player
{
    public class PlayerDamageHandler : MonoBehaviour
    {
        [SerializeField, Range(1, 10)] private int DamageMultiplier;

        private Animator m_Animator;
        
        private PlayerStats _PlayerStats;

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            _PlayerStats = GetComponent<PlayerStats>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _PlayerStats.Damage(2 * DamageMultiplier);
                m_Animator.SetTrigger("Damaged");
            }
        }
    }
}
