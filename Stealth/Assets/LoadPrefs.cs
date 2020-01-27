using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadPrefs : MonoBehaviour
{
    public Text dmgTaken, death, barrel, keyPick, hpPick, peopleSaved;
    void Awake()
    {
        try
        {
            PlayerPrefs.GetInt("dmgTaken");
            PlayerPrefs.GetInt("died");
            PlayerPrefs.GetInt("peopleSaved");
            PlayerPrefs.GetInt("gotIntoBarrel");
            PlayerPrefs.GetInt("keyPickedup");
            PlayerPrefs.GetInt("HearthPickedup");
        }
        catch (System.Exception)
        {
            PlayerPrefs.SetInt("died", 0);
            PlayerPrefs.SetInt("dmgTaken", 0);
            PlayerPrefs.SetInt("HearthPickedup", 0);
            PlayerPrefs.SetInt("gotIntoBarrel", 0);
            PlayerPrefs.SetInt("peopleSaved", 0);
            PlayerPrefs.SetInt("keyPickedup", 0);
            throw;
        }

    }
    void Start()
    {
        dmgTaken.text = PlayerPrefs.GetInt("dmgTaken").ToString();
        death.text = PlayerPrefs.GetInt("death").ToString();
        barrel.text = PlayerPrefs.GetInt("gotIntoBarrel").ToString();
        keyPick.text = PlayerPrefs.GetInt("keyPickedup").ToString();
        hpPick.text = PlayerPrefs.GetInt("HearthPickedup").ToString();
        peopleSaved.text = PlayerPrefs.GetInt("peopleSaved").ToString();
    }
    public void Loadscene()
    {
        SceneManager.LoadScene("Map1");
    }
}
