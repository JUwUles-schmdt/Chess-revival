using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement; // Nécessaire pour lire la scène active

public class NetworkStarter : MonoBehaviour
{
    async void Start()
    {
        NetworkRunner runner = gameObject.AddComponent<NetworkRunner>();
        var sceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>();

        await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared, 
            SessionName = "SalleTest",
            SceneManager = sceneManager,
            // NOUVELLE LIGNE : On indique à Photon de scanner la scène actuelle
            Scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex)
        });
    }
}