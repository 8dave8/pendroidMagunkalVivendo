using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterMover : MonoBehaviour
{
    public Animator CharacterAnim;
    public bool buttonPressed = false;
    public Joystick joystick;
    public Text Saved;
    public Rigidbody2D rb2d;
    private bool hasKey = false;
    public bool inBarrel = false;
    public float movespeed;
    private float speed = 1f;
    private int saved = 0;
    void Start () {
    }
    void FixedUpdate () {
        Debug.Log(inBarrel);
        float userInputV; 
        float userInputH;
        if (false)
        {
            userInputV = Input.GetAxis ("Vertical");
            userInputH = Input.GetAxis ("Horizontal");
        }
        else{
            userInputH = joystick.Horizontal;
            userInputV = joystick.Vertical;
        }
        if(!inBarrel)
        {
            rb2d.velocity = new Vector2(userInputH,userInputV)*movespeed;
            CharacterAnim.SetFloat("Velocity",Math.Abs(rb2d.velocity.x)+ Math.Abs(rb2d.velocity.y));
        }
        if(inBarrel && (Input.GetButtonDown("Jump") || buttonPressed)){
            inBarrel = false;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
            gameObject.GetComponentInChildren<CircleCollider2D>().enabled = true;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {   
        if (col.gameObject.tag == "Barrel" && (Input.GetButtonDown("Jump") || buttonPressed) && !inBarrel)
        {
            getInBarrel(col.gameObject);
        }
        if(col.gameObject.tag == "People" && (Input.GetButtonDown("Jump") || buttonPressed))
        {
            Destroy(col.gameObject);
            saved++;
            Saved.text = "Saved: "+saved;
        }
        if(col.gameObject.tag == "key" && (Input.GetButtonDown("Jump") || buttonPressed))
        {
            Destroy(col.gameObject);
            hasKey = true;
        }
    }
    private void getInBarrel(GameObject col)
    {
        Debug.Log("go Into barrel");
        rb2d.velocity = Vector2.zero;
        gameObject.transform.position = col.transform.position;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.GetComponentInChildren<CircleCollider2D>().enabled = false;
        GameObject[] currentEnemis = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < currentEnemis.Length; i++)
        {
            StartCoroutine("wait", currentEnemis[i]);
        }
        inBarrel = true;
    }
    //void to activate button
    public void pressbutton()
    {
        buttonPressed = true;
        StartCoroutine("buttonOff");
    }
    IEnumerator wait(GameObject currentEnemy)
    {
        yield return new WaitForSeconds(0.5f);
        currentEnemy.GetComponent<EnemyAI>().isTargetingPlayer = false;
    }
    IEnumerator buttonOff()
    {
        yield return new WaitForSeconds(0.01f);
        buttonPressed = false;
    }
    public void Loadscene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}