using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterMover : MonoBehaviour
{
    public Animator CharacterAnim;
    public GameObject Visibility;
    public bool buttonPressed = false;
    public Joystick joystick;
    public Rigidbody2D rb2d;
    public bool inBarrel = false;
    public float movespeed;
    public string SceneneToLoad;
    private bool hasKey = false;
    private float speed = 1f;
    private int saved = 0;
    
    void FixedUpdate () {
        float userInputV; 
        float userInputH;
        if (false) // SET THIS TRUE TO SWITCH TO PC CONTROLL
        {
            userInputV = Input.GetAxis ("Vertical");
            userInputH = Input.GetAxis ("Horizontal");
        }
        else{
            userInputH = joystick.Horizontal;
            userInputV = joystick.Vertical;
        }
        if(inBarrel && (Math.Abs(userInputH) > 0.2f || Math.Abs(userInputV) > 0.2f)) getOutBarrel();
        if(!inBarrel)
        {
            rb2d.velocity = new Vector2(userInputH,userInputV)*movespeed;
            CharacterAnim.SetFloat("Velocity",Math.Abs(rb2d.velocity.x)+ Math.Abs(rb2d.velocity.y));
        }
        if(inBarrel && (Input.GetButtonDown("Jump") || buttonPressed)){
            getOutBarrel();
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {   
        if (col.gameObject.tag == "Barrel" && (Input.GetButtonDown("Jump") || buttonPressed) && !inBarrel)
        {
            getInBarrel(col.gameObject);
            PlayerPrefs.SetInt("gotIntoBarrel", PlayerPrefs.GetInt("gotIntoBarrel")+1);
        }
        if(col.gameObject.tag == "People" && (Input.GetButtonDown("Jump") || buttonPressed))
        {
            Destroy(col.gameObject);
            PlayerPrefs.SetInt("peopleSaved", PlayerPrefs.GetInt("peopleSaved")+1);
        }
        if(col.gameObject.tag == "key" && (Input.GetButtonDown("Jump") || buttonPressed))
        {                        
            PlayerPrefs.SetInt("keyPickedup", PlayerPrefs.GetInt("keyPickedup")+1);
            Destroy(col.gameObject);
            hasKey = true;
        }
        if(hasKey && col.gameObject.tag == "Door" && (Input.GetButtonDown("Jump") || buttonPressed))
        {
            Loadscene(SceneneToLoad);
        }
        if(col.gameObject.tag == "Potion" && (Input.GetButtonDown("Jump") || buttonPressed))
        {
            PlayerPrefs.SetInt("HearthPickedup", PlayerPrefs.GetInt("HearthPickedup")+1);
            Destroy(col.gameObject);
            GetComponent<HealthConroller>().addHealth();
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
        //Visibility.transform.localScale = new Vector3(4,4,0);
        Visibility.SetActive(false);
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
    public void getOutBarrel()
    {
        Debug.Log("Getoutbarrel");
        inBarrel = false;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        gameObject.GetComponentInChildren<CircleCollider2D>().enabled = true;
        //Visibility.transform.localScale = new Vector3(10,10,0);
        Visibility.SetActive(true);
    }
}