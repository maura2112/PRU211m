
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceController : IntroController
{
    public static ScenceController instance;
    [SerializeField] Animator transitionAnim;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            PlayerPrefs.SetInt("HighScore1P", PlayerPrefs.GetInt("Score"));
        }
        if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            PlayerPrefs.SetInt("HighScore2P", PlayerPrefs.GetInt("Score"));
        }
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 7)
        {
            PlayerPrefs.SetInt("TotalMoney", 0);
            PlayerPrefs.SetInt("Score", 0);
        }
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {

        StartCoroutine(LoadLevel());
    }


    public void LoadScene(string sceneName)
    {

        SceneManager.LoadSceneAsync(sceneName);

    }

    IEnumerator LoadLevel()
    {

        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        //if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        //{
        //    SceneManager.LoadSceneAsync(6);
        //}
        //else

            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);


        transitionAnim.SetTrigger("Start");


    }
}
