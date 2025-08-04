using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour, IUpdateable
{
    [SerializeField] private GlobalVariablesSO globalVariables;
    [SerializeField] private GameEventsSO gameEvents;
    [Space(10)]
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private int staringAsteroidCount = 10;
    [SerializeField] private float asteroidSpawnRate = 1;
    [SerializeField] private float asteroidSpeed = 1f;

    private float maxX;
    private float maxY;

    private List<GameObject> asteroids = new();
    private List<GameObject> movingAsteroids = new();
    private List<GameObject> removeMovingAsteroidQueue = new();

    private float spawnRateTimer;

    public void DoUpdate(float deltaTime)
    {
        SpawnAsteroid(deltaTime);
        MoveAsteoroids(deltaTime);
    }

    private void OnAsteroidHurt(GameObject asteroid)
    {
        removeMovingAsteroidQueue.Add(asteroid);
    }

    private void InstantiateStartingAsteroids()
    {
        for (int i = 0; i <= staringAsteroidCount; i++)
        {
            GameObject asteroidInstance = Instantiate(asteroidPrefab);
            asteroidInstance.SetActive(false);

            asteroids.Add(asteroidInstance);
        }
    }

    private void SpawnAsteroid(float deltaTime)
    {
        spawnRateTimer += deltaTime;

        while (spawnRateTimer >= asteroidSpawnRate && asteroids.Count > 0)
        {
            int lastIndex = asteroids.Count - 1;
            var asteroid = asteroids[lastIndex];

            asteroids.RemoveAt(lastIndex);
            movingAsteroids.Add(asteroid);

            asteroid.transform.position = new(maxX, 0, 0);
            asteroid.SetActive(true);

            spawnRateTimer = 0;
        }
    }

    private void MoveAsteoroids(float deltaTime)
    {
        foreach (var asteroid in removeMovingAsteroidQueue)
        {
            asteroid.SetActive(false);
            movingAsteroids.Remove(asteroid);
            asteroids.Add(asteroid);
        }

        removeMovingAsteroidQueue.Clear();

        foreach (var asteroid in movingAsteroids)
        {
            Transform asteroidTranform = asteroid.transform;
            asteroidTranform.position += asteroidSpeed * deltaTime * -asteroidTranform.right;

            if (asteroidTranform.position.x < -maxX)
                removeMovingAsteroidQueue.Add(asteroid);
        }
    }

    private void SetMaxPositions()
    {
        maxX = globalVariables.MaxX - globalVariables.ScreenMargin;
        maxY = globalVariables.MaxY - globalVariables.ScreenMargin;
    }

    private void Awake()
    {
        SetMaxPositions();
        InstantiateStartingAsteroids();

        gameEvents.OnAsteroidHurt += OnAsteroidHurt;
    }
    
    private void Start()
    {
        UpdateManager.Instance.AddUpdateable(this);
    }
}
