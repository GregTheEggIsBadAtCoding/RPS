using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class enemyScript : MonoBehaviour
{
    [SerializeField] Interact inter;
    [SerializeField] Image healthBar;
    int random;
    public int[] combo = new int[3];
    public int health;
    int counter = 0;
    int tempHealth = 0;
    int buff = 0;
    int damageActive = 0;
    int rerollActive = 0;
    [SerializeField] Text text;
    [SerializeField] Text playerHealth;

    //sprite stuff
    public Sprite[] enemySprites;
    void Start()
    {
        // This is just for grabbing variables from the Interact script
        text.text = health.ToString();
        
    }
    void enemyHealthBar(){
        healthBar.fillAmount = (health/100f)*3.3333333333f;
    }
    void healthChange(int eHealth, int pHealth){
        if (tempHealth < 1){
            inter.health = inter.health - pHealth;
            playerHealth.text = " -" + pHealth.ToString();
        } else {
            tempHealth = tempHealth - pHealth;
            playerHealth.text = " nullified " + pHealth.ToString();
        }
        health = health - (eHealth + (2 * damageActive));
        text.text = health.ToString() + "  -" + (eHealth + 2 * damageActive).ToString();
        inter.playerHealthBar();
        enemyHealthBar();
    }
    void damageBuffStart(){
        damageActive = 1;
        buff = 2;
    }

    // Update is called once per frame
    private void Update()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene("EndScene");
        }
    }
    public void enemyAttack(){

        if (inter.value > 0){
            random = Random.Range(1,4);
            if(rerollActive > 0){
                random = Random.Range(2,4);
                rerollActive--;
                Debug.Log("Rock rerolled and " + rerollActive + " left");
            }
            string result = (inter.value, random) switch
            {
                (1, 3) or (2, 1) or (3, 2) => "win",
                (1, 2) or (2, 3) or (3, 1) => "lose",
                (1, 1) or (2, 2) or (3, 3) => "tie",
                _ => "default case because I hate yellow squiggly lines"
            };

            inter.changePlayerSpriteResult(inter.value, random, result);

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
            } else {
                damageActive = 0;
            }


            Debug.Log(counter);
            combo[counter] = inter.value;
            if (counter == 2){
                int result2 = (combo[0], combo[1], combo[2]) switch {
                    (1,2,1) => 1, // Damage buff
                    (2,2,3) => 2, // Reroll rock
                    (2,1,3) => 3, // Temp health
                    _ => 0
                }; 
                Debug.Log(result2);
                if (result2 == 1 && damageActive != 1){
                    damageBuffStart();
                    Debug.Log("Damage Buff Activated");
                    counter = 0;
                } else if (result2 == 2 && rerollActive < 1) {
                    rerollActive = 2;
                    Debug.Log("Rock Removal Activated");
                } else if (result2 == 3 && tempHealth < 1){
                    tempHealth = Random.Range(4,6);
                    Debug.Log(tempHealth + " tempHealth added");
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
