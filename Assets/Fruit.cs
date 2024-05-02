using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int size;
    public GameObject nextSizeFruitPrefab;
    public GameObject mergeParticleEffect; // Particle effect prefab
    public int points; // Points for this fruit
    private bool isMerging;

    public enum State
    {
        Falling,
        Resting
    }
    
    public State currentState = State.Falling;

    
    // Call this method when the fruit comes to rest
    public void SetToResting()
    {
        currentState = State.Resting;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the ground or other fruits
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Fruit"))
        {
            SetToResting();
        }
        
        if (isMerging) return;

        Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();

        if (otherFruit != null && otherFruit.size == this.size)
        {
            ScoreManager.instance.AddPoints(this.points + otherFruit.points);
            if (nextSizeFruitPrefab != null)
            {
                GameObject newFruit = Instantiate(nextSizeFruitPrefab, transform.position, Quaternion.identity);
                StartMergeEffect(newFruit.transform);
            }

            isMerging = true;
            otherFruit.isMerging = true;

            Destroy(otherFruit.gameObject);
            Destroy(gameObject);
        }
        
    }
    

    void StartMergeEffect(Transform newFruitTransform)
    {
        // Start particle effect
        if (mergeParticleEffect != null)
        {
            Instantiate(mergeParticleEffect, transform.position, Quaternion.identity);
        }

        // Start scale animation
        StartCoroutine(ScaleOverTime(newFruitTransform, 1f)); // 1 second for example
    }

    System.Collections.IEnumerator ScaleOverTime(Transform targetTransform, float duration)
    {
        Vector3 originalScale = targetTransform.localScale;
        Vector3 destinationScale = originalScale * 1.2f; // Slightly larger for effect

        float currentTime = 0.0f;

        do
        {
            targetTransform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        while (currentTime <= duration);

        // Return to original scale
        targetTransform.localScale = originalScale;
    }
}