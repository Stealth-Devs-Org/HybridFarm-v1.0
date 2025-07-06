using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorSpawn : MonoBehaviour
{
    private GameObject predatorToSpawn;

    public GameObject fox;
    public GameObject lynx;
    public GameObject couguar;
    public GameObject wolf;
    public GameObject snowleopard;


    public Vector2 spawnRange = new Vector2(3, 10);
    public Vector2 fieldRange = new Vector2(3, 1);
    public float moveSpeed = 40.0f; // Speed at which the object will move to the new position

    public float spawnDelay = 10.0f;


    bool canSpawn = true; // Flag to control cool down

    void Start()
    {

    }

    public void SpawnObject(int predatorIndex)
    {
        if (canSpawn)
        {
            getPredatorObject(predatorIndex);
            //StartCoroutine(SpawnCoolDown());

            // Generate random position within the spawn range
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnRange.x, spawnRange.x),
                Random.Range(spawnRange.y, spawnRange.y),
                6.7f
            );

            // Spawn the object at the generated position
            GameObject spawnedObject = Instantiate(predatorToSpawn, spawnPosition, Quaternion.identity);

            // Calculate the new position within the field range while maintaining x position
            Vector3 targetPosition = new Vector3(
                spawnedObject.transform.position.x, // Maintain x position
                Random.Range(-fieldRange.y, fieldRange.y), // New y position within field range
                spawnedObject.transform.position.z // Maintain z position
            );

            // Move the object to the new position with specified speed
            StartCoroutine(MoveObject(spawnedObject.transform, targetPosition, moveSpeed));
        }
    }


    IEnumerator MoveObject(Transform objectToMove, Vector3 targetPosition, float moveSpeed)
    {
        //  distance to move calculations
        float distance = Vector3.Distance(objectToMove.position, targetPosition);
        // Continue moving until the object reaches the target position
        while (distance > Mathf.Epsilon)
        {
            // Move towards the target position
            objectToMove.position = Vector3.MoveTowards(objectToMove.position, targetPosition, moveSpeed * Time.deltaTime);

            // Recalculate the distance to the target position
            distance = Vector3.Distance(objectToMove.position, targetPosition);

            // Yield until the next frame
            yield return null;
        }
    }

    // Get the predator object for the predator index
    private void getPredatorObject(int predatorIndex)
    {
        if (predatorIndex == 1)
        {
            predatorToSpawn = fox;
        }
        else if (predatorIndex == 2)
        {
            predatorToSpawn = lynx;
        }
        else if (predatorIndex == 3)
        {
            predatorToSpawn = couguar;
        }
        else if (predatorIndex == 4)
        {
            predatorToSpawn = wolf;
        }
        else
        {
            predatorToSpawn = snowleopard;
        }
    }

    public void StartSpawnCoolDown()
    {
        canSpawn = false;
        StartCoroutine(SpawnCoolDown());
    }

    IEnumerator SpawnCoolDown()
    {
        spawnDelay = Random.Range(15.0f, 25.0f); // Randomize the spawn delay between 5 to 10 seconds
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }
}
