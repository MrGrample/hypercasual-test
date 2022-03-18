using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{

    [SerializeField] GameObject waypointManager;
    [SerializeField] Shooting player;

    void Start()
    {
        player.StopShooting();
        waypointManager.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            waypointManager.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
