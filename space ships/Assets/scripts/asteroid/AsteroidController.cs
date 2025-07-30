using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour, IUpdateable
{
    [SerializeField] private GlobalVariablesSO globalVariables;
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private int startingAmount;
    [SerializeField] private int spawnRateAmount;
    [SerializeField] private float spawnRate;
    [SerializeField] private float minAsteroidSpeed;
    [SerializeField] private float maxAsteroidSpeed;

    private float maxX;
    private float maxY;

    private List<Asteroid> asteroids = new();

    public void DoUpdate(float deltaTime)
    {
        MoveAsteoroids();
    }

    private void InstantiateAsteroids()
    {
        Vector3 startingPosition = new(maxX, 0, 0);

        List<float> randomYPositions = GetRandomYPositions(startingAmount, -maxY, maxY);

        for (int i = 0; i < startingAmount; i++)
        {
            startingPosition.y = randomYPositions[i];

            var asteroidInstance = Instantiate(asteroidPrefab, startingPosition, Quaternion.identity);

            asteroids.Add(new Asteroid
            {
                Instance = asteroidInstance,
                Speed = Random.Range(minAsteroidSpeed, maxAsteroidSpeed)
            });
        }
    }

    private List<float> GetRandomYPositions(int count, float minY, float maxY)
    {
        float spacing = (maxY - minY) / count;
        List<float> positions = new();

        for (int i = 0; i < count; i++)
        {
            float baseY = minY + spacing * i;
            float randomOffset = Random.Range(0, spacing);
            positions.Add(baseY + randomOffset);
        }

        return positions;
    }

    private void MoveAsteoroids()
    {
        foreach (var asteroid in asteroids)
        {
            Transform asteroidTranform = asteroid.Instance.transform;
            asteroidTranform.position += asteroid.Speed * Time.deltaTime * -asteroidTranform.right;
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
        InstantiateAsteroids();
    }
    
    private void Start()
    {
        UpdateManager.Instance.AddUpdateable(this);
    }
}
