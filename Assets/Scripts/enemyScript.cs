using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class enemyScript : MonoBehaviour
{
    [SerializeField] Interact inter;
    [SerializeField] Image healthBar;
    [SerializeField] GameObject RPS;
    
    //Attack choices
    int random;
    public int[] combo = new int[3];
    public int[] enemyCombo = new int[3];
    int[] comboChoice = new int[3];
    int comboDecision = 0;
    int choice = 0;
    int counter = 0;
    int Ecounter = 0;

    //Enemy health
    public int health;

    //Enemy buff
    int enemyDamageActive = 0;
    int enemyBuff = 0;
    public int enemyRerollActive = 0;
    int enemyTempHealth = 0;
    int tempHealth = 0;
    int buff = 0;
    int damageActive = 0;
    int rerollActive = 0;

    //Access external objects
    [SerializeField] Text text;
    [SerializeField] Text playerHealth;
    [SerializeField] AudioClip countdown;

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
    void arrayPlacement(int a, int aa, int aaa, int[] ar){
        ar[0] = a;
        ar[1] = aa;
        ar[2] = aaa;
    }
    public int enemyDecision(){
        if (choice == 3) {
            comboDecision = 0;
        }
        if (comboDecision == 0){
            choice = 0;
            if (health < 20 && tempHealth < 1){
                comboDecision = 1; // Add temp health
            } else {
                int temp = Random.Range(1,3);
                if (temp == 1){
                    comboDecision = 2; // Remove Rock
                } else {
                    comboDecision = 3; // Damage buff
                }
            }
        }
        if (comboDecision == 1){
            arrayPlacement(2,1,3,comboChoice); // health+
            choice++;
            return comboChoice[choice-1];
        } else if (comboDecision == 2){
            arrayPlacement(2,2,3,comboChoice); // reroll rock
            choice++;
            return comboChoice[choice-1];
        } else{
            arrayPlacement(1,2,1,comboChoice); // damage
            choice++;
            return comboChoice[choice-1];
        }
    }
    void healthChange(int eHealth, int pHealth)
    {
        //displays the player health with markers of how much was lost each round
        if (tempHealth < 1){
            inter.health = inter.health - (pHealth + (2*enemyDamageActive));
            playerHealth.text = " -" + (pHealth+(2*enemyDamageActive)).ToString();
        } else {
            tempHealth = tempHealth - (pHealth + (2*enemyDamageActive));
            playerHealth.text = " nullified " + pHealth+(2*enemyDamageActive).ToString();
        }
        if (enemyTempHealth < 1){
            health = health - (eHealth + (2 * damageActive));
            text.text = health.ToString() + "  -" + (eHealth + 2 * damageActive).ToString();
        } else {
            enemyTempHealth = enemyTempHealth - (eHealth + (2 * damageActive));
            text.text = health.ToString() + "  nullified " + (eHealth + 2 * damageActive).ToString();
        }
        
        //change the health bars accordingly
        inter.playerHealthBar();
        enemyHealthBar();
    }
    void damageBuffStart(){
        damageActive = 1;
        buff = 2;
    }
    void enemyDamageBuffStart(){
        enemyDamageActive = 1;
        enemyBuff = 2;
    }

    // Update is called once per frame
    private void Update()
    {
        //makes sure that the game doesnt continue past 0 health
        if (health <= 0)
        {
            text.text = "0";
            StartCoroutine(Waiting());            
        }
    }

    public void enemyAttack() {

        //picks an attack based on enemyDecision 
        if (inter.value > 0) {
            random = enemyDecision();
            if (rerollActive > 0) {
                random = Random.Range(2, 4);
                rerollActive--;
                Debug.Log("Rock rerolled and " + rerollActive + " left");
            } rerollActive--;
            Debug.Log(random);
            enemyCombo[Ecounter] = random;
            if (Ecounter == 2){
                int result3 = (enemyCombo[0], enemyCombo[1], enemyCombo[2]) switch {
                    (1,2,1) => 1, // Damage buff
                    (2,2,3) => 2, // Reroll rock
                    (2,1,3) => 3, // Temp health
                    _ => 0
                }; 
                Debug.Log(result3);
                if (result3 == 1 && enemyDamageActive != 1){
                    enemyDamageBuffStart();
                    Debug.Log("Enemy Damage Buff Activated");
                    Ecounter = 0;
                } else if (result3 == 2 && enemyRerollActive < 1) {
                    enemyRerollActive = 1;
                    Debug.Log("Enemy Rock Removal Activated");
                } else if (result3 == 3 && enemyTempHealth < 1){
                    enemyTempHealth = Random.Range(4,6);
                    Debug.Log(tempHealth + " enemyTempHealth added");
                } else {
                    Ecounter = 0;
                }
            } else {
                Ecounter++;
            }
            
        }
        
        //and switches the sprite accordingly
        switch (random)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = enemySprites[4];
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = enemySprites[6];
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = enemySprites[9];
                break;
        }
    }
    public void roundResult() {

        //Calls an IEnumator that plays the count down sound and waits a few seconds before showing the results
        StartCoroutine(playCountdown());
    }

    public void roundResultparttwo() {
        //determines the result of the round
        string result = (inter.value, random) switch
        {
            (1, 3) or (2, 1) or (3, 2) => "win",
            (1, 2) or (2, 3) or (3, 1) => "lose",
            (1, 1) or (2, 2) or (3, 3) => "tie",
            _ => "default case because I hate yellow squiggly lines"
        };

        //changes Player and enemy sprites accordingly
        inter.changePlayerSpriteResult(inter.value, random, result);

       //changes Player and Enemy health accordingly
        Debug.Log("Player picked " + inter.value + " and AI picked " + random + " the result is a " + result);
        if (result == "win"){
            healthChange(3,1);
        }  else if (result == "lose"){
            healthChange(1,3);
        } else {
            healthChange(2,2);
        }

        // modifies damage buff as needed
        if (damageActive == 1 && buff > 0){
            buff--;
        } else {
            damageActive = 0;
        }
        if (enemyDamageActive == 1 && enemyBuff > 0){
            enemyBuff--;
        } else {
            enemyDamageActive = 0;
        }

        //Manages the buff
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
                rerollActive = 1;
                Debug.Log("Rock Removal Activated");
                counter = 0;
            } else if (result2 == 3 && tempHealth < 1){
                tempHealth = Random.Range(4,6);
                Debug.Log(tempHealth + " tempHealth added");
                counter = 0;
            } else {
                counter = 0;
            }
        } else {
            counter++;
        }
        inter.value = 0;
    }

    IEnumerator playCountdown()
    {
        RPS.SetActive(true);
        AudioSource.PlayClipAtPoint(countdown, Camera.main.transform.position);
        yield return new WaitForSeconds(3.6f);
        RPS.SetActive(false);
        enemyAttack();
        yield return new WaitForSeconds(0.5f);
        roundResultparttwo();
    }
    IEnumerator Waiting()
    {
        Debug.Log("waiting");


        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("EndScene");


    }
}

