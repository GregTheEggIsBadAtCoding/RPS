using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public int value = 0;
    public int health = 3;
    [SerializeField] Text text;
    void Update(){
        text.text = health.ToString();
    }
    public void RPS(int valueChange){
        value = valueChange;
    }
}
