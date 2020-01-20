using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMover : MonoBehaviour
{
    public Text Saved;
    public Rigidbody2D rb2d;
    private bool hasKey = false;
    private bool inBarrel = false;
    public float movespeed;
    private float speed = 1f;
    private int saved = 0;
    // Use this for initialization
    void Start () {
    }
    void FixedUpdate () {
        float userInputV = Input.GetAxis ("Vertical");
        float userInputH = Input.GetAxis ("Horizontal");
        //transform.Translate(userInputH*movespeed*Time.deltaTime,
        //userInputV *movespeed*Time.deltaTime, 0);
        //rb2d.AddForce(new Vector2(userInputH*movespeed,userInputV *movespeed),ForceMode2D.Force);
        if(!inBarrel)
        {
            rb2d.velocity = new Vector2(userInputH,userInputV)*movespeed;
        }
        if(inBarrel && Input.GetButtonDown("Jump")){
            inBarrel = false;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {   
        if (col.gameObject.tag == "Barrel" && Input.GetButtonDown("Jump") && !inBarrel)
        {
            Debug.Log("go Into barrel");
            rb2d.velocity = Vector2.zero;
            gameObject.transform.position = col.gameObject.transform.position;
            inBarrel = true;
        }
        if(col.gameObject.tag == "People" && Input.GetButtonDown("Jump"))
        {
            Destroy(col.gameObject);
            saved++;
            Saved.text = "Saved: "+saved;
        }
        if(col.gameObject.tag == "key" && Input.GetButtonDown("Jump"))
        {
            Destroy(col.gameObject);
            hasKey = true;
        }
    }
}