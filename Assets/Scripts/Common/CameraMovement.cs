using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float damping = 1.5f;
    [SerializeField] private Vector2 targetOffset;

    private Transform _target;

    private bool _isFaceLeft;
    private int _lastX;
    private float _dynamicSpeed;
    
    public void Initialize(Transform target)
    {
        _target = target;
        FocusOnTarget();
    }

    private void FocusOnTarget()
    {
        transform.position = new Vector3(_target.position.x + targetOffset.x, _target.position.y + targetOffset.y, transform.position.z);
        _lastX = Mathf.RoundToInt(_target.position.x);
    }

    private void LateUpdate()
    {
        if (!_target) return;

        int currentX = Mathf.RoundToInt(_target.position.x);
        if (currentX > _lastX)
        {
            _isFaceLeft = false;
        }
        else if (currentX < _lastX)
        {
            _isFaceLeft = true;
        }

        _lastX = currentX;
        Vector3 targetPosition;
        if (_isFaceLeft)
        {
            targetPosition = new Vector3(_target.position.x - targetOffset.x, _target.position.y + targetOffset.y, transform.position.z);
        }
        else
        {
            targetPosition = new Vector3(_target.position.x + targetOffset.x, _target.position.y + targetOffset.y, transform.position.z);
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);
    }
}