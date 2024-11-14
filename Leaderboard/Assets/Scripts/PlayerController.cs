using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rig;

    private float startTime;
    private float timeTaken;

    private int CollectabledPicked;
    public int maxCollectables = 10;

    public GameObject playButton;
    public TextMeshProUGUI curTimeText;

    private bool isPlaying;

    void Awake ()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Update ()
    {
        if(!isPlaying)
           return;

        float x = Input.GetAxis("Vertical") * speed;
        float z = Input.GetAxis("Horizontal") * speed;

        rig.velocity = new Vector3(x, rig.velocity.y, z);

        curTimeText.text = (Time.time - startTime).ToString();
    }

    public void Begin ()
    {
        startTime = Time.time;
        isPlaying = true;
        playButton.SetActive(false);
    }

    void End ()
    {
        timeTaken = Time.time - startTime; 
        isPlaying = false; 
        Leaderboard.instance.SetLeaderboardEntry(-Mathf.RoundToInt(timeTaken * 1000.0f));
        playButton.SetActive(true);
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.CompareTag("Collectable"))
        {
            CollectabledPicked++;
            Destroy(other.gameObject);
            if(CollectabledPicked == maxCollectables)
                End();
        }
    }
}
