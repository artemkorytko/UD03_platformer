using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private WallBoss _wallBoss;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        _wallBoss = FindObjectOfType<WallBoss>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Bomb>())
        {
            gameObject.SetActive(false);
            _wallBoss.gameObject.SetActive(false);
        }
    }
}
