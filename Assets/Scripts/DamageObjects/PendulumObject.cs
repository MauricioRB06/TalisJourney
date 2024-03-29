﻿
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
// Collaborators: @barret50cal3011 @DanielaCaO @Kradyn
// 
// The Purpose Of This Script Is:
//
//  Define the behavior of pendulum type damage objects.
//
// Documentation and References:
//
//  Unity Awake: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//  Unity FixedUpdate: https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
//  Unity OnCollisionEnter2D: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnCollisionEnter2D.html
//
//  -----------------------------
// Last Update: 14/08/2022 By MauricioRB06

using Player;
using UnityEngine;

namespace DamageObjects
{
    // Component required for this Script to work.
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(PolygonCollider2D))]
    
    public class PendulumObject : MonoBehaviour
    {
        
        [Header("Axis Settings")] [Space(5)]
        [Tooltip("Determine the pivot point of rotation of the pendulum.")]
        [SerializeField] private Transform rotationAxis;
        [Space(15)]
        
        [Header("Oscillation Settings")] [Space(5)]
        [Tooltip("Sets the oscillation speed.")]
        [Range(50f, 250f)][SerializeField] private float oscillationSpeed = 100f;
        [Tooltip("Sets the left limit of the oscillation angle.")]
        [Range(0.3f, 0.8f)][SerializeField] private float oscillationLeftLimit = 0.8f;
        [Tooltip("Sets the right limit of the oscillation angle.")]
        [Range(-0.3f, -0.8f)][SerializeField] private float oscillationRightLimit= -0.8f;
        [Space(15)]
        
        [Header("Damage Settings")] [Space(5)]
        [Tooltip("Sets the amount of damage it causes when it collides.")]
        [Range(5f, 50f)][SerializeField] private float damageToGive = 10f;
        [Tooltip("Strength to be applied to the player.")]
        [Range(20.0f, 100.0f)][SerializeField] private float knockbackForce = 80;
        [Tooltip("It is the duration that the Knockback will last.")]
        [Range(1.0F, 2.0f)][SerializeField] private float knockbackDuration = 1;
        
        // Check that the components necessary for the operation are not empty and instantiate the sound effect.
        private void Awake()
        {
            if (rotationAxis == null)
            {
                Debug.LogError(
                    "<color=#D22323><b>The pendulum rotation axis cannot be empty, please add one</b></color>");
            }
        }
        
        // While the pendulum is running, it performs a rotation of the pendulum based on the limits and the set speed.
        private void FixedUpdate()
        {
            transform.RotateAround(rotationAxis.position, Vector3.forward,
                oscillationSpeed * Time.deltaTime);
            
            if (transform.rotation.z > oscillationLeftLimit)
            {
                oscillationSpeed *= -1;
            }
            else if (transform.rotation.z < oscillationRightLimit)
            {
                oscillationSpeed *= -1;
            }
        }
        
        // Check if it collided with the player, to cause the established damage.
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;
            
            collision.transform.GetComponent<PlayerController>().PlayerHealth.TakeDamage(damageToGive);
            collision.transform.GetComponent<PlayerController>().KnockBackAnimation();
            collision.transform.GetComponent<PlayerController>().KnockBack(knockbackDuration,
                knockbackForce, transform);
        }

    }
}
