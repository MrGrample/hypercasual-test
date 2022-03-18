using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class WaypointManager : MonoBehaviour
{

    [SerializeField] Waypoint[] waypoints;
    [SerializeField] int currentWaypoint = 0;
    [SerializeField] GameObject player;
    [SerializeField] GameObject winText;
    [SerializeField] float levelRestartTime = 5.0f;

    private void Start()
    {
        currentWaypoint = 0;
    }

    void Update()
    {
        if (currentWaypoint < waypoints.Length - 1)
        {
            if (waypoints[currentWaypoint].EnemyCount == 0)
            {
                currentWaypoint++;
                player.GetComponent<PlayerMovement>().MovePlayer(waypoints[currentWaypoint].transform.position);
            }
        }
        else
        {
            if (waypoints[currentWaypoint].IsExit && waypoints[currentWaypoint].EnemyCount == 0)
            {
                Win();
            }
        }
    }

    public Waypoint GetCurrentWaypoint()
    {
        return waypoints[currentWaypoint];
    }

    void Win()
    {
        winText.SetActive(true);
        StartCoroutine(RestartLevel());
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(levelRestartTime);
        SceneManager.LoadScene(0);
    }
}
