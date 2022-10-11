using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
   private Rigidbody2D rigidbody2D;
   private void Awake()
   {
      rigidbody2D = GetComponent<Rigidbody2D>();
   }

   private void OnCollisionEnter2D(Collision2D col)
   {
      if (col.gameObject.GetComponent<EnemyController>())
      {
        gameObject.SetActive(false);
      }
      if (col.gameObject.GetComponent<PlayerController>())
      {
         Destroy(gameObject,5);
      }
   }
}
