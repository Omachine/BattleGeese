using UnityEngine;
using UnityEngine.SceneManagement;

public class FromComicToGame : MonoBehaviour
{
    [SerializeField] private string sceneName = "Lobby"; // Default scene name

    void Update() //desculpa z� sei q n � eficiente mas n tenho tempo agr
    {
        if (Input.GetKey(KeyCode.E))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
