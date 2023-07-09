using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Snake : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform player;
    public HeroControll hero;

    private float speed = 0.5f;
    private float patrolSpeed = 2f;
    private float chaseDistanceThreshold = 3f;
    
    private float scaleNum;

    private double demageDelay = 2.0;
    private double lastDamageTimeStamp;

    
    // Start is called before the first frame update
    void Start()
    {
        scaleNum = transform.localScale.x;
        lastDamageTimeStamp = GetTimeStamp();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        
        float distance = Vector2.Distance(player.position, transform.position);
        // Debug.Log(distance);

        if (distance < chaseDistanceThreshold)
        {
            Vector2 direction = player.position - transform.position;

            if (direction.x < 0)
                transform.localScale = new Vector3(-scaleNum, scaleNum, 1f);  
            else
                transform.localScale = new Vector3(scaleNum, scaleNum, 1f);
            rb.velocity = direction * speed;
        }
        else
        {
            if (patrolSpeed < 0)
                transform.localScale = new Vector3(-scaleNum, scaleNum, 1f);  
            else
                transform.localScale = new Vector3(scaleNum, scaleNum, 1f);
            rb.velocity = new Vector2(patrolSpeed, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D coll) //傳入碰撞對象，coll(自己取)
    {
        if (coll.gameObject.tag == "Map")
        {
            patrolSpeed *= -1;
        } 
    }

    void OnCollisionStay2D(Collision2D coll) //傳入碰撞對象，coll(自己取)
    {
        if (coll.gameObject.tag == "Player")
        {
            double currentTimeStamp = GetTimeStamp();
            if (currentTimeStamp - lastDamageTimeStamp >= demageDelay)
            {
                hero.Damage(1);
                lastDamageTimeStamp = currentTimeStamp;
            }
        }
    }

    double GetTimeStamp()
    {
        return (DateTime.Now.ToUniversalTime() - new DateTime (1970, 1, 1)).TotalSeconds;
    }
}
