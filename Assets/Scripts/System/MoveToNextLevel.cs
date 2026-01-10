using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextLevel : MonoBehaviour
{
    [SerializeField] int levelId;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            int currentLevel = GameManager.instance.GetCurrentLevel();
            int next = currentLevel + 1;

            GameManager.instance.SetCurrentLevel(next);

            GameManager.instance.SetUnlockedLevel(next);

            int total = SceneManager.sceneCountInBuildSettings;

            if (next <= 3 )
            {
                GameManager.instance.SetUnlockedLevel(next);
                SceneManager.LoadScene("Gameplay");
            }
            else if (next == 4)
            {
                GameManager.instance.SetUnlockedLevel(next);
                SceneManager.LoadScene("BossLevel");
            }
            else
            {
                SceneManager.LoadScene("MainScreen");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
