using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    public int sensitivity = 2;
    public bool isPressed = false;
    public GameObject door;

    public AudioSource lockSrc;
    public AudioSource switchEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        switchEffect.Play();
        if (other.gameObject.CompareTag("Rabbit"))
            isPressed = true;
        if (other.gameObject.CompareTag("Player") &&
            other.gameObject.GetComponent<PlayerController>().GetNumberOfStickedObjects() >= sensitivity) {
            isPressed = true;
            door.gameObject.SetActive(false);
            lockSrc.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isPressed = false;
    }
}