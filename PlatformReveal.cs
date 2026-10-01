using UnityEngine;

public class PlatformReveal : MonoBehaviour
{
    [Header("Platforms 2 to 7")]
    public GameObject[] platforms;

    [Header("Torch")]
    public GameObject torchLight;

    private int currentPlatform = 1;
    private int nextPlatform = 2;

    // True after a platform has been revealed,
    // until the player actually reaches it.
    private bool waitingForPlayer = false;

    private void Start()
    {
        // Hide Platforms 2-7 at the beginning
        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i] == null)
                continue;

            HidePlatform(platforms[i]);
        }

        if (torchLight != null)
        {
            torchLight.SetActive(false);
        }

        Debug.Log("START: Player is on Platform 1");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // E always toggles the torch
            if (torchLight != null)
            {
                torchLight.SetActive(!torchLight.activeSelf);
            }

            Debug.Log("E PRESSED");

            // Do NOT reveal another platform until
            // the player has reached the current platform.
            if (waitingForPlayer)
            {
                Debug.Log("WAITING: Player must reach Platform "
                    + (nextPlatform - 1));

                return;
            }

            // Reveal only the next platform
            if (nextPlatform == currentPlatform + 1)
            {
                RevealPlatform(nextPlatform);
            }
        }
    }

    private void HidePlatform(GameObject platform)
    {
        // Hide graphics
        SpriteRenderer[] sprites =
            platform.GetComponentsInChildren<SpriteRenderer>(true);

        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.enabled = false;
        }

        // Disable solid collision
        Collider2D[] colliders =
            platform.GetComponentsInChildren<Collider2D>(true);

        foreach (Collider2D collider in colliders)
        {
            if (!collider.isTrigger)
            {
                collider.enabled = false;
            }
        }
    }

    private void RevealPlatform(int platformNumber)
    {
        int index = platformNumber - 2;

        if (index < 0 || index >= platforms.Length)
            return;

        GameObject platform = platforms[index];

        if (platform == null)
            return;

        // Show graphics
        SpriteRenderer[] sprites =
            platform.GetComponentsInChildren<SpriteRenderer>(true);

        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.enabled = true;
        }

        // Enable solid collision
        Collider2D[] colliders =
            platform.GetComponentsInChildren<Collider2D>(true);

        foreach (Collider2D collider in colliders)
        {
            if (!collider.isTrigger)
            {
                collider.enabled = true;
            }
        }

        Debug.Log("PLATFORM " + platformNumber + " IS NOW VISIBLE");

        // Player must reach this platform before
        // another platform can be revealed.
        waitingForPlayer = true;

        nextPlatform++;
    }

    // Called when Player collides with a platform
    public void PlayerReachedPlatform(int platformNumber)
    {
        // Only accept the platform we are currently waiting for
        if (platformNumber != nextPlatform - 1)
        {
            return;
        }

        currentPlatform = platformNumber;
        waitingForPlayer = false;

        Debug.Log(
            "PLAYER REACHED PLATFORM " + platformNumber
        );
    }
}