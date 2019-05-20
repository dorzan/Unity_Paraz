using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageManager : MonoBehaviour
{
    GameObject parazWinner;
    Vector3 moveCamera;
    Vector3 moveCameraX;
    Vector3 moveCameraY;
    PlayerManager playerManager;
    // Start is called before the first frame update
    void Start()
    {
        moveCamera = new Vector3(0.4f, 0.4f, 0);
        moveCameraX = new Vector3(0.4f, 0, 0);
        moveCameraY = new Vector3(0, 0.4f, 0);
        playerManager = GameObject.Find("GameManager").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onWin (GameObject paraz)
    {
        //parazWinner = playerManager.PlayersList[parazId].paraz;
        parazWinner = paraz;
        Debug.Log("onwin");
        //parazWinner.GetComponent<AndroidRaceMovement>().enabled = false;  //do to all parazim
        for (int i = 0; i < playerManager.getNumOfPlayers(); i++)
            playerManager.PlayersList[i].paraz.GetComponent<AndroidRaceMovement>().enabled = false;
        StartCoroutine("cameraZoom");
        
    } 

    IEnumerator cameraZoom()
    {
        Destroy(GameObject.Find("Obstacels"));
        float posX = parazWinner.transform.position.x;
        float posY = parazWinner.transform.position.y;
        Debug.Log(posX+ " " + posY);
        GameObject camera = GameObject.Find("Camera");
        while (posX < camera.transform.position.x && posY < camera.transform.position.y)
        {
            yield return new WaitForSeconds(0.01f);
            camera.transform.position -= moveCamera;
            camera.GetComponent<Camera>().orthographicSize -= 0.15f;
        }

        while (posX < camera.transform.position.x )
        {
            yield return new WaitForSeconds(0.01f);
            camera.transform.position -= moveCameraX;
            camera.GetComponent<Camera>().orthographicSize -= 0.15f;
        }
        while (posY < camera.transform.position.y)
        {
            yield return new WaitForSeconds(0.01f);
            camera.transform.position -= moveCameraY;
            camera.GetComponent<Camera>().orthographicSize -= 0.15f;
        }
        AudioManager.Instace.playSFX("won");

        yield return new WaitForSeconds(4f);
        GameObject.Find("GameManager").GetComponent<SceneManage>().ChangeScene("Lobby");
    }
}
