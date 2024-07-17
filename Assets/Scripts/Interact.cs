using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Interact : MonoBehaviour
{
    [SerializeField] Image healthBar;
    public int value = 0;
    public int health = 30;
    [SerializeField] Text text;

    [SerializeField] enemyScript enemy;

    //Sprite controller
    public Sprite[] spriteSheet;
    [SerializeField] GameObject Hero;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = Hero.GetComponent<SpriteRenderer>();
    }
    void Update(){
        text.text = health.ToString();
        if (health <= 0){
            // if we add a death animation, we can make it pause here for it to play
            SceneManager.LoadScene("StartMenu");
        }
    }
    public void RPS(int valueChange){
        value = valueChange;
        changePlayerSpriteAttack(value);
        StartCoroutine(Waiting());
        
    }
    public void playerHealthBar(){
        healthBar.fillAmount = (health/100f)*3.3333333333f;
    }
    
    //changed when the button is clicked
    public void changePlayerSpriteAttack(int attackType)
    {
        //Rock: 1, Paper: 2, Scissors: 3
        
        switch (attackType) 
        {
            case 1:
                spriteRenderer.sprite = spriteSheet[5];
                break;
            case 2:
                spriteRenderer.sprite = spriteSheet[4];
                break;
            case 3:
                spriteRenderer.sprite = spriteSheet[6];
                break;
            default:
                break;
        }
    }
    //changes when the results of the fight come through
    public void changePlayerSpriteResult(int playerAttack, int enemyAttack, string result)
    {
        switch (result)
        {
            case ("win"):
                spriteRenderer.sprite = spriteSheet[7];
                break;
            case ("lose"):
                switch (playerAttack, enemyAttack)
                {
                    case (1,2):
                        //enemy Paper beats player Rock
                        spriteRenderer.sprite = spriteSheet[1];
                        break;
                    case (2,3):
                        //enemy Scissors beats player Paper
                        spriteRenderer.sprite = spriteSheet[0];
                        break;
                    case (3,1):
                        //enemy Rock beats player Scissors
                        spriteRenderer.sprite = spriteSheet[2];
                        break;
                }
                break;
            default: 
                spriteRenderer.sprite = spriteSheet[3];
                break;
        }
    }

    IEnumerator Waiting()
    {
        Debug.Log("waiting");
        yield return new WaitForSeconds(1f);
        enemy.enemyAttack();
    }

}
