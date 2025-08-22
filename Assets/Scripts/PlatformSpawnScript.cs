using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRate = 1f;
    private float randomSpawnRate = 0;
    private float timer = 0;
    public float positiveHeightOffset = 6;
    public float negativeHeightOffset = 3;

    // Start is called before the first frame update
    void Start()
    {
        PipeSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < randomSpawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            PipeSpawn();
            timer = 0;
        }
    }

    void PipeSpawn()
    {
        float lowestPoint = transform.position.y - negativeHeightOffset;
        float highestPoint = transform.position.y + positiveHeightOffset;

        Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
}