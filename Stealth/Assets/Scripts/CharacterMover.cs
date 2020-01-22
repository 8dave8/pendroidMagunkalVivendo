using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMover : MonoBehaviour
{
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
        float userInputV = Input.GetAxis ("Vertical");
        float userInputH = Input.GetAxis ("Horizontal");
        if(!inBarrel)
        {
            rb2d.velocity = new Vector2(userInputH,userInputV)*movespeed;
        }
        if(inBarrel && Input.GetButtonDown("Jump")){
            inBarrel = false;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
            gameObject.GetComponentInChildren<CircleCollider2D>().enabled = true;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {   
        if (col.gameObject.tag == "Barrel" && Input.GetButtonDown("Jump") && !inBarrel)
        {
            Debug.Log("go Into barrel");
            rb2d.velocity = Vector2.zero;
            gameObject.transform.position = col.gameObject.transform.position;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.GetComponentInChildren<CircleCollider2D>().enabled = false;
            GameObject[] currentEnemis = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < currentEnemis.Length; i++)
            {
                StartCoroutine("wait", currentEnemis[i]);
            }
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
    IEnumerator wait(GameObject currentEnemy)
    {
        yield return new WaitForSeconds(0.5f);
        currentEnemy.GetComponent<EnemyAI>().isTargetingPlayer = false;
    }
}