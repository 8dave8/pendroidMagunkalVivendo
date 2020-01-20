using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupController : MonoBehaviour
{
    public Text coinText;
    private bool colliding;
    void Start()
    {
        colliding = false;
        int coins = PlayerPrefs.GetInt("coins");
        if (coins <= 9) coinText.text = "00"+coins;
        else if (coins <= 99) coinText.text = "0"+coins;
        else coinText.text = coins+"";
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("collided");
        if(col.gameObject.CompareTag("Coin") && gameObject.layer == 0 && colliding == false)
        {  
            colliding = true;
            StartCoroutine("wait");
            Destroy(col.gameObject);
            int coins = PlayerPrefs.GetInt("coins") + 1;
            PlayerPrefs.SetInt("coins", coins);
            if (coins <= 9) coinText.text = "00"+coins;
            else if (coins <= 99)  coinText.text = "0"+coins;
            else coinText.text = coins+"";
        }
        else if(col.gameObject.CompareTag("Hearth") && gameObject.layer == 0 && colliding == false)
        {
            Debug.Log("Health");
            colliding = true;
            StartCoroutine("wait");
            Destroy(col.gameObject);
            GetComponent<HealthConroller>().addHealth();
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.01f);
        colliding = false;
    }

}
