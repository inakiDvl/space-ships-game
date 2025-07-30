using UnityEngine;

[CreateAssetMenu(fileName = "Global Variables", menuName = "Scriptable Objects/GlobalVariables")]
public class GlobalVariablesSO : ScriptableObject
{
    [field: SerializeField] public float ScreenMargin { get; private set; }

    [field: SerializeField] public float MaxX { get; private set; }
    [field: SerializeField] public float MaxY { get; private set; }

    private void SetMaxPositions()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera == null)
            return;

        float mainCameraZ = Mathf.Abs(mainCamera.transform.position.z);

        MaxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, mainCameraZ)).x;
        MaxY = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1, mainCameraZ)).y;
    }

    private void OnEnable()
    {
        SetMaxPositions();
    }
}
