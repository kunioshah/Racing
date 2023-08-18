using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Driver : MonoBehaviour
{

    [SerializeField] float turnSpeed = -80f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] GameObject startLine;
    [SerializeField] GameObject midPoint;
    [SerializeField] GameObject endLine;
    [SerializeField] Text timeText; 
    [SerializeField] Text bestTimeText; 

    public bool hasRaceStarted = false;
    public bool isRaceHalfway = false;
    public bool hasRaceEnded = false;
    public bool isBoostActive = false;

    public float raceTime = 0;
    public float bestRaceTime = 1000;
    public float boostTime = 0;

    // see when somebody starts and finishes a race (halfway is to make sure that they
    // don't just go over the starting line and then reverse into the finish line)
    // and when somebody hits a boost

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name + " : " + gameObject.name + " : " + UnityEngine.Time.time);

        if (collision.gameObject.name == startLine.name) {
            hasRaceStarted = true;
            Debug.Log("the race has begun!");
        }

        if (collision.gameObject.name == midPoint.name) {
            isRaceHalfway = true;
            Debug.Log(collision.gameObject.name);
            Debug.Log("halfway!");
        }

         if (collision.gameObject.name == endLine.name) {
            hasRaceEnded = true;
            Debug.Log("the race is over!!");
        }

         if (collision.gameObject.tag == "boost") {
            Boost();
        }
    }

    // increases player speed and records the time so that the boost can stop in 1 second

    void Boost() {
        moveSpeed = 20f;
        boostTime = Time.time;
        isBoostActive = true;
    }

    // tells when the race has ended and records the time, and ends the boost after 1 second

    void StartRace() {
        
        if (isRaceHalfway && hasRaceEnded) {
            hasRaceStarted = false;
            isRaceHalfway = false;
            hasRaceEnded = false;

            if (bestRaceTime > raceTime) {
                bestRaceTime = raceTime;
                bestTimeText.text = "Best Time:" + raceTime;
            }
            raceTime = 0;
        }

        if (hasRaceStarted) {
            raceTime += UnityEngine.Time.deltaTime;
            timeText.text = "Time:" + raceTime;
        }

        if (isBoostActive) {
            Debug.Log(boostTime);
            if (boostTime + 1 <= Time.time)
            {
                moveSpeed = 10f;
            }
        }
    }

    // controls to move and turn the player

    void Update()
    {
        float steerInput = (Input.GetAxis("Horizontal"))*(turnSpeed)*UnityEngine.Time.deltaTime;
        float moveInput = (Input.GetAxis("Vertical"))*(moveSpeed)*UnityEngine.Time.deltaTime;
        transform.Rotate(0, 0, steerInput);
        transform.Translate(0, moveInput, 0);

        StartRace();
        
    }
}
