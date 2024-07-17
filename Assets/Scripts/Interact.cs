using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{
    [SerializeField] Image healthBar;
    public int value = 0;
    public int health = 30;
    [SerializeField] Text text;
    void Update(){
        text.text = health.ToString();
        if (health <= 0){
            // if we add a death animation, we can make it pause here for it to play
            SceneManager.LoadScene("StartMenu");
        }
    }
    public void RPS(int valueChange){
        value = valueChange;
    }
    public void playerHealthBar(){
        healthBar.fillAmount = (health/100f)*3.3333333333f;
    }
    
}
