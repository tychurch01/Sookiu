using UnityEngine;
using UnityEngine.UI; // Namespace for UI components

public class ColorUpdater : MonoBehaviour
{
    public GameObject backgroundCircle; // Assign in Inspector
    public GameObject[] fruitPrefabs; // Assign fruit prefabs in Inspector

    void Start()
    {
        UpdateCircleColors();
    }

    void UpdateCircleColors()
    {
        if (backgroundCircle == null || fruitPrefabs == null)
        {
            Debug.LogError("BackgroundCircle or FruitPrefabs not assigned.");
            return;
        }

        for (int i = 0; i < backgroundCircle.transform.childCount; i++)
        {
            // Ensure we don't exceed the bounds of the fruitPrefabs array
            if (i >= fruitPrefabs.Length) break;

            GameObject childCircle = backgroundCircle.transform.GetChild(i).gameObject;
            Image circleImage = childCircle.GetComponent<Image>();
            SpriteRenderer fruitRenderer = fruitPrefabs[i].GetComponent<SpriteRenderer>();

            if (circleImage != null && fruitRenderer != null)
            {
                // Update the color of the UI Image to match the corresponding fruit
                circleImage.color = fruitRenderer.color;
            }
            else
            {
                Debug.LogError("Missing Image or SpriteRenderer component.");
            }
        }
    }
}