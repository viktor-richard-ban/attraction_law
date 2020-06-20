using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    public int sensitivity = 0;
    public bool isPressed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rabbit"))
            isPressed = true;
        if (other.gameObject.CompareTag("Player") &&
            other.gameObject.GetComponent<CharacterMovement>().GetNumberOfStickedObjects() > sensitivity)
            isPressed = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isPressed = false;
    }
}