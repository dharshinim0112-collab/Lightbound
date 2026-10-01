using UnityEngine;

public class SceneCompletePanel : MonoBehaviour
{
    public GameObject panel;
    public MonoBehaviour playerController;

    private bool completed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (completed)
            return;

        if (!other.CompareTag("Player"))
            return;

        completed = true;

        // Stop player input
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Stop player movement
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false;
        }

        // SHOW PANEL AND KEEP IT VISIBLE
        if (panel != null)
        {
            panel.SetActive(true);
        }

        Debug.Log("PLAYER REACHED GOAL - PANEL OPEN");
    }
}