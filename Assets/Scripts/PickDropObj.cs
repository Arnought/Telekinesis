using UnityEngine;

public class PickDropObj : MonoBehaviour
{
    public float throwForce = 10f;
    public GameObject carryPoint; // Reference to the CarryPoint GameObject

    private bool isPickedUp = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.MiddleCenter;
        style.fontSize = 20;

        // Display pickup/drop text only when looking at the object
        if (isPickedUp)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 20, 200, 40), "Drop", style);
        }
        else if (IsLookingAtObject())
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 20, 200, 40), "Pick up", style);
        }
    }

    void Update()
    {
        if (!isPickedUp && Input.GetKeyDown(KeyCode.E) && IsLookingAtObject()) // Check if not picked up, E is pressed, and looking at object
        {
            PickUp();
        }
        else if (isPickedUp && Input.GetKeyDown(KeyCode.K)) // Check if picked up and K is pressed
        {
            ThrowObject();
        }

        if (isPickedUp)
        {
            UpdatePosition(); // Update position if picked up
        }
    }

    void PickUp()
    {
        isPickedUp = true;
        rb.isKinematic = true; // Disable physics while picked up
        transform.parent = carryPoint.transform; // Set the carryPoint as the parent
    }

    void UpdatePosition()
    {
        transform.localPosition = Vector3.zero; // Keep object at the local position of the carryPoint
    }

    void ThrowObject()
    {
        isPickedUp = false;
        rb.isKinematic = false; // Re-enable physics when thrown
        transform.parent = null; // Remove parent to prevent further attachment to carryPoint

        // Calculate throw direction based on player's forward direction
        Vector3 throwDirection = carryPoint.transform.forward;

        // Apply force to the object
        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
    }

    bool IsLookingAtObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject == gameObject;
        }

        return false;
    }
}
