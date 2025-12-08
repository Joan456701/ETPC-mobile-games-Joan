using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GSVictory", menuName = "GameStates/GSVictory", order = 1)]
public class GSVictory : GameState
{
    public override void OnEnter()
    {
        Time.timeScale = 0.0f;
        UIVictory pause = FindObjectOfType<UIVictory>(true);
        pause.gameObject.SetActive(true);
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        Time.timeScale = 1.0f;
        UIVictory pause = FindObjectOfType<UIVictory>();
        pause.gameObject.SetActive(false);
    }

    public void NextLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "SampleScene")
        {
            SceneManager.LoadScene("SampleScene_Level2");
        }
        else if (currentScene == "SampleScene_Level2")
        {
            SceneManager.LoadScene("MainMenu");
        }

        GameStateManager.Instance.ChangeGameState(GameState.StateType.GAMEPLAY);
    }

    public void ReloadScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
        GameStateManager.Instance.ChangeGameState(GameState.StateType.GAMEPLAY);
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        GameStateManager.Instance.ChangeGameState(GameState.StateType.MAINMENU);
    }
}
