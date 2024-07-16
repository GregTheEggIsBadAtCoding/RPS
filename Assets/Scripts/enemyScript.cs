using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enemyScript : MonoBehaviour
{
    Interact inter;
    int random;
    int[] combo;
    public int health;
    [SerializeField] Text text;
    void Start()
    {
        inter = FindObjectOfType<Interact>();
        // This is just for grabbing variables from the Interact script
        text.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
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
                health--;
                text.text = health.ToString();
            }  else if (result == "lose"){
                inter.health--;
            }
            inter.value = 0;
        }
    }
}
