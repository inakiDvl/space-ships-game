using System.Collections.Generic;
using UnityEngine;

public class Updater : MonoBehaviour
{
    public static Updater Instance;

    private List<IUpdateable> updateables = new();
    private List<IUpdateable> addQueue = new();
    private List<IUpdateable> removeQueue = new();

    public void AddUpdateable(IUpdateable updateable)
    {
        addQueue.Add(updateable);
    }

    public void RemoveUpdateable(IUpdateable updateable)
    {
        removeQueue.Add(updateable);
    }

    public void UpdateUpdateables()
    {
        foreach (var updateable in removeQueue)
        {
            updateables.Remove(updateable);
        }

        removeQueue.Clear();

        float deltaTime = Time.deltaTime;

        foreach (var updateable in updateables)
        {
            updateable.DoUpdate(deltaTime);
        }

        foreach (var updateable in addQueue)
        {
            updateables.Add(updateable);
        }

        addQueue.Clear();
    }

    private void CreateInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        CreateInstance();
    }

    private void Update()
    {
        UpdateUpdateables();
    }
}

public interface IUpdateable
{
    public void DoUpdate(float deltaTime);
}
