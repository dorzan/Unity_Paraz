using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    PlayerManager playerManager;
    string nextScene;
    private Scene scene;
    bool firstTimeLobby = true;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        playerManager = GetComponent<PlayerManager>();

    }

    void OnLevelWasLoaded(int level)
    {
        scene = SceneManager.GetActiveScene();

        if (scene.name == "Lobby" && firstTimeLobby == false)
        {
            playerManager.onLobbySceneLoad();

        }

        else if (firstTimeLobby == true)
            firstTimeLobby = false;

        if (scene.name == "Race")
        {
            Destroy(GameObject.Find("Llama"));
            onRaceLoad();
            playerManager.onRaceSceneLoad();
        }

        if (playerManager.enabled == false)
            playerManager.enabled = true;
    }

    public string getSceneName()
    {
        scene = SceneManager.GetActiveScene();
        return scene.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    public void ChangeScene(string scene)
    {
        nextScene = scene;

        if (scene == "Race")
        {
            Invoke("loadNextScene", 3);
        }
        if (scene == "Lobby")
            SceneManager.LoadScene(nextScene);

    }

    private void loadNextScene()
    {
        StartCoroutine(loadNextSceneWithTeleports());
    }

    IEnumerator loadNextSceneWithTeleports()
    {
        for (int i = 0; i < playerManager.getNumOfPlayers(); i++)
        {
            if(playerManager.PlayersList[i].paraz.GetComponent<AndroidMovement>().enabled)
                playerManager.PlayersList[i].paraz.GetComponent<AndroidMovement>().onSceneChange();
            else
                playerManager.PlayersList[i].paraz.GetComponent<PythonMovement>().onSceneChange();

        }
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene(nextScene);
    }

  

    public void cancleNextSceneInvoke()
    {
        CancelInvoke();
    }


    void onRaceLoad()
    {

    }

}
