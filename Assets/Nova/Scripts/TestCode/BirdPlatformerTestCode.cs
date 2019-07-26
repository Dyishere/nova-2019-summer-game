using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPlatformerTestCode : MonoBehaviour
{
    public GameObject Player;
    public float fallingX;
    // Start is called before the first frame update
    void Start()
    {
        GameObject copyPlayer = Instantiate(Player, Player.transform.position + Vector3.up * 2f + Vector3.right * fallingX, Player.transform.rotation);
        copyPlayer.GetComponent<PlayerController>().enabled = false;
    }

}
