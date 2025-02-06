using UnityEngine;

public class InfiniteRoad : MonoBehaviour
{
    public GameObject road1;
    public GameObject road2;

    // Speed of the road movement
    public float roadSpeed = 5f;

    private float roadHeight;

    void Start()
    {
        // Get the height of the road sprite from the bounds
        roadHeight = road1.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        MoveRoads();
        CheckAndRepositionRoads();
    }

    public void EnableMovement()
    {
        //Debug.Log("CALLED FROM SPEECHHH...");
        roadSpeed += 1;
    }

    void MoveRoads()
    {
        // Move both roads downward
        road1.transform.Translate(Vector3.down * roadSpeed * Time.deltaTime);
        road2.transform.Translate(Vector3.down * roadSpeed * Time.deltaTime);
    }

    void CheckAndRepositionRoads()
    {
        // If road1 moves completely out of the screen
        if (road1.transform.position.y <= -roadHeight - 1.85f)
        {
            RepositionRoad(road1, road2);
        }

        // If road2 moves completely out of the screen
        if (road2.transform.position.y <= -roadHeight - 1.85f)
        {
            RepositionRoad(road2, road1);
        }
    }

    void RepositionRoad(GameObject roadToMove, GameObject referenceRoad)
    {
        // Perfectly reposition the road above the reference road
        float newYPosition = referenceRoad.transform.position.y + roadHeight - 0.01f; // Slight overlap to avoid gaps
        roadToMove.transform.position = new Vector3(referenceRoad.transform.position.x, newYPosition, referenceRoad.transform.position.z);
    }
}