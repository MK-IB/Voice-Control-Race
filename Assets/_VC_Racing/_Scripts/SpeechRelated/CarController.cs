using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10f;
    private bool isMoving = false;

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    public void StartCar()
    {
        isMoving = true;
        Debug.Log("Car Started!");
    }

    public void StopCar()
    {
        isMoving = false;
        Debug.Log("Car Stopped!");
    }
}