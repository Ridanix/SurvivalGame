using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    int playerTestTeleported = 0;
    Player_Controller playerController;
    GameObject playableGameObject;
    private void Start()
    {
        playableGameObject = GameObject.Find("Model_GO");
        playerController = playableGameObject.GetComponent<Player_Controller>();
       
    }

    public void TestButtonTeleportClicked()
    {
        StartCoroutine("TestTeleportDev");
    }
    IEnumerator TestTeleportDev()
    {
        playerController.disabled = true;
        yield return new WaitForSeconds(1f);
        if (playerTestTeleported == 0)
        {
            gameObject.transform.position = new Vector3(0f, 1f, 0f);
            
        }
        if (playerTestTeleported == 1)
        {
            gameObject.transform.position = new Vector3(770f, 11f, 794f);

        }


        yield return new WaitForSeconds(1f);
        if (playerTestTeleported == 1)
        {
           playerTestTeleported = 0;
        }
        else
        {
            playerTestTeleported = 1;
        }
        playerController.disabled = false;
    }
}
