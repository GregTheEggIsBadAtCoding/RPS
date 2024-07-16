using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enemyScript : MonoBehaviour
{
    [SerializeField] Interact inter;
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
    void healthChange(int eHealth, int pHealth){
        inter.health = inter.health - pHealth;
        health = health - (eHealth + (buff * damageActive));
        text.text = health.ToString();
    }
    void buffStart(){
        damageActive = 1;
        buff = 3;

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
            if (damageActive == 1){
                buff--;
            }
            /*
                                          FIXING THIS LATER, DON'T TOUCH PLEASE THANKS
            combo[counter] = inter.value;
            if (counter == 3){
            int result2 = (combo[1], combo[2], combo[3]) switch {
                (1,2,1) => 1,
                (2,2,3) => 2,
                (2,1,3) => 3,
                _ => 0
            }; 
            if (result2 == 1){
                buffStart();
                counter = 0;
            } else {
                counter = 0;
            }
            */
        }
            counter++;
            inter.value = 0;
        }
    }
