using UnityEngine;

public class PlatformReachTrigger : MonoBehaviour
{
    public int platformNumber;
    public PlatformReveal platformReveal;

    private void Start()
    {
        if (platformReveal == null)
        {
            platformReveal = FindFirstObjectByType<PlatformReveal>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Only react to the Player
        if (!collision.gameObject.CompareTag("Player"))
            return;

        Debug.Log("PLAYER COLLIDED WITH PLATFORM " + platformNumber);

        if (platformReveal != null)
        {
            platformReveal.PlayerReachedPlatform(platformNumber);
        }
    }
}