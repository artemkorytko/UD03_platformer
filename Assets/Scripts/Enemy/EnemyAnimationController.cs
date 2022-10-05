using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private const string SPEED = "Speed";

    private static readonly int Speed = Animator.StringToHash(SPEED);

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void SetSpeed(int value)
    {
        _animator.SetInteger(Speed, value);
    }
}