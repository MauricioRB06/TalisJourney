﻿
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
// Collaborators: @barret50cal3011 @DanielaCaO @Kradyn
// 
// The Purpose Of This Script Is:
//
//  Sets the behavior of the boat stop zone.
//
// Documentation and References:
//
//  Unity OnTriggerEnter2D: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerEnter2D.html
//
//  -----------------------------
// Last Update: 14/08/2022 By MauricioRB06

using UnityEngine;

namespace Platforms
{
    // Component required for this script to work.
    [RequireComponent(typeof(BoxCollider2D))]
    
    public class BoatZone : MonoBehaviour
    {
        
        // When a boat touches the zone, its function is called to stop it and then this object is destroyed.
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.transform.CompareTag("Boat")) return;
            col.transform.GetComponent<Boat>().DestroyBoat();
            Destroy(gameObject);
        }
        
    }
}
