﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthConroller : MonoBehaviour
{
    public CapsuleCollider2D closeCol;
    public Rigidbody2D rb;
    public Sprite FullHearth;
    public Sprite EmptyHearth;
    public GameObject hearth1;
    public GameObject hearth2;
    public GameObject hearth3;
    public static int health;
    private bool takingDamage = false;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(11,9);
        health = 3;
    }
    public void takeDamage()
    {
        PlayerPrefs.SetInt("dmgTaken",PlayerPrefs.GetInt("dmgTaken")+1);
        health--;
        if (health == 2)      hearth3.GetComponent<SpriteRenderer>().sprite = EmptyHearth;
        else if (health == 1) hearth2.GetComponent<SpriteRenderer>().sprite = EmptyHearth;
        else if (health == 0) hearth1.GetComponent<SpriteRenderer>().sprite = EmptyHearth;
        if (health <= 0) Death();
    }
    public void addHealth()
    {
        if(health <= 0) Death();
        if(health != 3) health++;

        if(health == 3) hearth3.GetComponent<SpriteRenderer>().sprite = FullHearth;
        else if(health == 2) hearth2.GetComponent<SpriteRenderer>().sprite = FullHearth;
    }
    void OnCollisionStay2D(Collision2D col)
    {   
        //Debug.Log(col.gameObject.GetComponent<CapsuleCollider2D>()+"----"+closeCol);
        if (col.gameObject.tag == "Enemy" && col.gameObject.GetComponent<CapsuleCollider2D>().isTrigger == closeCol.isTrigger)
        {
            if(takingDamage) return;
            StartCoroutine("wait");            
        }
    }
    IEnumerator wait()
    {
        takingDamage = true;
        takeDamage();
        yield return new WaitForSeconds(1f);
        takingDamage = false;
    }
    private void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("died",PlayerPrefs.GetInt("died")+1);
    }
}
