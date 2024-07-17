using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    [SerializeField] Image healthBar;
    public int value = 0;
    public int health = 30;
    [SerializeField] Text text;
    void Update(){
        text.text = health.ToString();
    }
    public void RPS(int valueChange){
        value = valueChange;
    }
    public void playerHealthBar(){
        healthBar.fillAmount = (health/100f)*3.3333333333f;
    }
}
