using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    // Use this for initialization

    public GameObject currentCheckpoint;
    public PlayerController player;
    public GameObject deathParticle;
    public GameObject respawnParticle;
    public float respawnDelay;
    private CameraController camera;

    public LevelManager()
    {
    }

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        camera = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RespawnPlayer()
    {
        StartCoroutine("RespawnPlayerCO");
    }

    public IEnumerator RespawnPlayerCO()
    {
        Instantiate(deathParticle, player.transform.position, player.transform.rotation);
        player.enabled = false;
        player.GetComponent<Renderer>().enabled = false;
        camera.isFollowing = false;
        Debug.Log("LevelManager / Player Respawn");
        yield return new WaitForSeconds(respawnDelay);
        player.transform.position = currentCheckpoint.transform.position;
        player.enabled = true;
        player.GetComponent<Renderer>().enabled = true;
        camera.isFollowing = true;
        Instantiate(respawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
    }
}
