using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enemyScript : MonoBehaviour
{
    [SerializeField] Interact inter;
    [SerializeField] Image healthBar;
    int random;
    public int[] combo = new int[3];
    public int health;
    int counter = 0;
    int buff = 0;
    int damageActive = 0;
    [SerializeField] Text text;
    void Start()
    {
        // This is just for grabbing variables from the Interact script
        text.text = health.ToString();
        
    }
    void enemyHealthBar(){
        healthBar.fillAmount = (health/100f)*3.3333333333f;
    }
    void healthChange(int eHealth, int pHealth){
        inter.health = inter.health - pHealth;

        health = health - (eHealth + (2 * damageActive));
        text.text = health.ToString();
        inter.playerHealthBar();
        enemyHealthBar();
    }
    void damageBuffStart(){
        Debug.Log("damage+ buff active");
        damageActive = 1;
        buff = 2;

    }

    // Update is called once per frame
    void Update(){
        if (inter.value > 0){
            random = Random.Range(1,4);
            string result = (inter.value, random) switch
            {
                (1, 3) or (2, 1) or (3, 2) => "win",
                (1, 2) or (2, 3) or (3, 1) => "lose",
                (1, 1) or (2, 2) or (3, 3) => "tie",
                _ => "default case because I hate yellow squiggly lines"
            };
            Debug.Log("Player picked " + inter.value + " and AI picked " + random + " the result is a " + result);
            if (result == "win"){
                healthChange(3,1);
            }  else if (result == "lose"){
                healthChange(1,3);
            } else {
                healthChange(2,2);
            }
            if (damageActive == 1 && buff > 0){
                buff--;
                Debug.Log("Damage buff counter = " + buff);
            } else {
                Debug.Log("Damage Disabled");
                damageActive = 0;
            }
            Debug.Log(counter);
            combo[counter] = inter.value;
            if (counter == 2){
                int result2 = (combo[0], combo[1], combo[2]) switch {
                    (1,2,1) => 1, // Damage buff
                    (2,2,3) => 2, // Reroll rock
                    (2,1,3) => 3,
                    _ => 0
                }; 
                Debug.Log(result2);
                if (result2 == 1 && damageActive != 1){
                    damageBuffStart();
                    counter = 0;
                } else {
                    counter = 0;
                }
            } else {
                counter++;
            }
            inter.value = 0;
        }
    }
}
