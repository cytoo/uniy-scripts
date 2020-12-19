using UnityEngine.SceneManagement;
using UnityEngine;

public class skyScript : MonoBehaviour
{
    void Update()
    {
        if(transform.position.y < 0f)SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
