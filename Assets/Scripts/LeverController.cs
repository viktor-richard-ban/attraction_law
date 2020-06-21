using UnityEngine;

public class LeverController : MonoBehaviour
{
    public bool isOn = false;
    private bool _canPull = false;
    private Transform _lever;

    public AudioSource switchEffect;

    private void Start()
    {
        _lever = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (_canPull && Input.GetKeyDown(KeyCode.E)){
            isOn = !isOn;
            switchEffect.Play();
        }
        if ((isOn && _lever.localScale.x > 0) || (!isOn && _lever.localScale.x < 0))
        {
            Vector3 theScale = _lever.localScale;
            theScale.x *= -1;
            _lever.localScale = theScale;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collide with Player");
            _canPull = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collide with Player");
            _canPull = false;
        }
    }
}