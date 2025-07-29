using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour, IUpdateable
{
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private int startingAmount;
    [SerializeField] private int spawnRateAmount;
    [SerializeField] private float spawnRate;
    [SerializeField] private float minAsteroidSpeed;
    [SerializeField] private float maxAsteroidSpeed;

    private float screenBorderX;

    private List<Asteroid> asteroids = new();
    
    public void DoUpdate(float deltaTime)
    {
        MoveAsteoroids();
    }

    private void InstantiateAsteroids()
    {
        Vector3 startingPosition = new(screenBorderX, 0, 0);

        for (int i = 0; i < startingAmount; i++)
        {
            var asteroidInstance = Instantiate(asteroidPrefab, startingPosition, Quaternion.identity);

            asteroids.Add(new Asteroid
            {
                Instance = asteroidInstance,
                Speed = Random.Range(minAsteroidSpeed, maxAsteroidSpeed)
            });
        }
    }

    private void MoveAsteoroids()
    {
        foreach (var asteroid in asteroids)
        {
            Transform asteroidTranform = asteroid.Instance.transform;
            asteroidTranform.position += asteroid.Speed * Time.deltaTime * -asteroidTranform.right;
        }
    }

    private void CalculateScreenBorder()
    {
        Camera mainCamera = Camera.main;
        float mainCameraZ = Mathf.Abs(mainCamera.transform.position.z);
        screenBorderX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, mainCameraZ)).x;
    }

    private void Awake()
    {
        Updater.Instance.AddUpdateable(this);

        CalculateScreenBorder();
        InstantiateAsteroids();
    }
}
