using UnityEngine;

/// <summary> Provides a centralized, globally accessible API to handle common system requests. </summary>
public class Game : MonoBehaviour
{
    [SerializeField] GameAPI elements = new();

    // A singleton of the partial API class, which is split into separate files based on system type
    public static GameAPI Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            // Destroy this instance of GameAPI if there is already an Instance singleton
            elements = null;
            Destroy(gameObject);
        }
        else
        {
            // Otherwise assign elements to the global Instance, initialize, and prevent destroy on scene load
            Instance = elements;
            Instance.InitializeAPI();
            DontDestroyOnLoad(gameObject);
        }
    }
}