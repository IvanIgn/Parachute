using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[RequireComponent(typeof(GameManager))] // kräv att det finns en GameManager på samma object
public class JumperSpawner : MonoBehaviour
{



    [SerializeField]
    ///////////Test/////////
    private List<GameObject> prefabs = new List<GameObject>();

    //public TextMeshPro score;

    // private GameObject tempPrefab;

    private GameObject curPref;

    ///////////////////////

    // GameObject jumperPrefab;


    ///////////////////////
    GameManager gameManager;
    float lastSpawnTime;

    [Range(0, 5)]
    public float spawnDelay = 3.0f;
    [Range(0, 2)]
    public float deltaRandomSpawn = 0.5f;

    public float spawnDelayDecreaseSpeed = 0.02f;

    private float randomSpawnDelay;
    private bool stop = false;

    private List<GameObject> jumpers = new List<GameObject>();



    private void Start()
    {
        RandomizePrefabs(prefabs);
        //////////
        // if (RandomizePrefab(prefabs) /*jumperPrefab */ == null)
        //   return;

        gameManager = GetComponent<GameManager>();

        randomSpawnDelay = spawnDelay;
        SpawnJumper();
    }

    private void Update()
    {
        if (!stop && Time.time > lastSpawnTime + randomSpawnDelay)
        {
            SpawnJumper();
            RandomizePrefabs(prefabs);
        }
/*
        if (score.text == "10")
        {
            spawnDelay = 4;
        }
       else if (score.text == "20")
        {
            spawnDelay = 3;
        }
        else if (score.text == "30")
        {
            spawnDelay = 2.5f;
        }
        */
    }

    private void SpawnJumper()
    {
        lastSpawnTime = Time.time;
		float delay = Mathf.Clamp( spawnDelay - ( spawnDelayDecreaseSpeed  * gameManager.Points()), deltaRandomSpawn , spawnDelay) ;
        randomSpawnDelay = Random.Range(delay - deltaRandomSpawn, delay + deltaRandomSpawn);
        GameObject jumper = Instantiate(curPref/*jumperPrefab*/);

		jumpers.Add(jumper);

        JumperController jumperController = jumper.GetComponentInChildren<JumperController>();

		jumperController.jumperSpawner = this;
	}

    public void DestroyJumper(GameObject jumper)
	{
		// ta bort jumper ur listan
		jumpers.Remove(jumper);

		// destroy jumper
		Destroy(jumper);
	}


    public void Stop()
	{
		stop = true;
        // gå igenom listan destroy all

        for(int i = jumpers.Count - 1; i >= 0; i-- )
		{
			DestroyJumper(jumpers[i]);
		} 
	}

    ///// Test  RandomPrefab function /////
    public void RandomizePrefabs(List<GameObject> myPrefabs)
    {
        for (var i = 0; i < myPrefabs.Count; i++)
        {
            var r = Random.Range(0, myPrefabs.Count);
            var tmp = myPrefabs[i];
            myPrefabs[i] = myPrefabs[r];
            myPrefabs[r] = tmp;
            curPref = tmp;
        }
    }

    /////////////////
}
