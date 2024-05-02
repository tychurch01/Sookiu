using UnityEngine;

public class DropController : MonoBehaviour
{
    private GameObject leftWall;
    private GameObject rightWall;
    private GameObject dropMarker;
    private SpriteRenderer fruitPreview; // Sprite renderer for previewing the fruit

    public GameObject[] fruitPrefabs;

    private float timeSinceLastSpawn;
    private float spawnCooldown = 1f;
    private int nextFruitIndex; // Index of the next fruit to be spawned

    // Start is called before the first frame update
    void Start()
    {
        leftWall = GameObject.FindGameObjectWithTag("leftWall");
        rightWall = GameObject.FindGameObjectWithTag("rightWall");
        dropMarker = GameObject.FindGameObjectWithTag("dropMarker");
        fruitPreview = dropMarker.transform.GetChild(0).GetComponent<SpriteRenderer>(); // Assuming the first child is the preview object

        if (fruitPrefabs == null || fruitPrefabs.Length == 0)
        {
            Debug.LogError("Fruit prefabs are not assigned in the Inspector.");
        }

        timeSinceLastSpawn = spawnCooldown;
        UpdateFruitPreview(); // Update the preview at the start
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        float leftWallScreenX = Camera.main.WorldToScreenPoint(leftWall.transform.position).x;
        float rightWallScreenX = Camera.main.WorldToScreenPoint(rightWall.transform.position).x;

        if (mousePos.x > leftWallScreenX + 50 && mousePos.x < rightWallScreenX - 50)
        {
            dropMarker.transform.position = new Vector3(worldMousePos.x, dropMarker.transform.position.y, dropMarker.transform.position.z);
        }

        // Update the timer
        timeSinceLastSpawn += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && timeSinceLastSpawn >= spawnCooldown)
        {
            SpawnRandomFruit();
            timeSinceLastSpawn = 0f;
            UpdateFruitPreview(); 
        }

    }
    
    void UpdateFruitPreview()
    {
        int maxIndex = Mathf.Min(fruitPrefabs.Length, 5); 
        nextFruitIndex = Random.Range(0, maxIndex);

        SpriteRenderer prefabRenderer = fruitPrefabs[nextFruitIndex].GetComponent<SpriteRenderer>();
    
        if (prefabRenderer != null)
        {
            // Update the preview sprite
            fruitPreview.sprite = prefabRenderer.sprite;

            // Update the preview color
            fruitPreview.color = prefabRenderer.color;

            // Update the preview scale to match the prefab
            fruitPreview.transform.localScale = fruitPrefabs[nextFruitIndex].transform.localScale;

            Debug.Log("Preview updated to: " + fruitPrefabs[nextFruitIndex].name);
        }
        else
        {
            Debug.LogError("SpriteRenderer on prefab " + fruitPrefabs[nextFruitIndex].name + " is missing.");
        }
    }


    void SpawnRandomFruit()
    {
        if (fruitPrefabs.Length > 0)
        {
            GameObject fruitToSpawn = fruitPrefabs[nextFruitIndex];
            Instantiate(fruitToSpawn, dropMarker.transform.position, Quaternion.identity);
            Debug.Log("Spawned fruit: " + fruitToSpawn.name);
        }
    }
}
