using UnityEngine;

public class FairyController : MonoBehaviour
{
    private Transform _target;
    private Transform _position;
    public float speed = 0.1f;
    private Animator _animator;

    void Start()
    {
        _position = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (_target != null)
        {
            Vector3 newPos = Vector3.MoveTowards(_position.position, _target.position, speed);
            _position.position = newPos;
            _animator.SetBool("reached", false);
            _animator.SetBool("isMoveSide", true);
            if (Vector3.Distance(_position.position, _target.position) < 1.0f)
            {
                _animator.SetBool("reached", true);
                _animator.SetBool("isMoveSide", false);
                _target = null;
            }
        }
    }

    public void SetTarget(GameObject gameObject)
    {
        _target = gameObject.transform;
    }
}