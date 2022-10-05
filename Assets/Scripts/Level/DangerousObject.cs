using UnityEngine;

public class DangerousObject : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    public int DamageValue => damage;
}