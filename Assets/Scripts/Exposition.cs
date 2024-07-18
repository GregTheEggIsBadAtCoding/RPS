using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exposition : MonoBehaviour
{
    [SerializeField] Text text;
    int progress = 0;
    void Start()
    {
        text.text = "After Chronobridge’s rise in biotechnology, many gangs rose to power with enhanced abilities and weaponry. They profited off of fighting rings and whenever a gang member came across someone from another gang, an all-out gang war would start. Violence was at an all-time high, and it seemed there would be no end to it.";
    }
    void expoNext(){
        string result = progress switch{
            0 => "After ten years of non-stop fighting, a new group rose to power called the Cybernetic Collective. They swiftly took control over Chronobridge and imposed new laws and new ways of enforcing them. This included the ban of any civilian fighting whatsoever. With no way to make a profit, and therefore losing much of their power, gangs turned to the only other way they knew how to establish power over one another:",
            1 => "A classic game of...",
            2 => "Trigger"
        } ;
        if (result != "Trigger"){
            progress++;
            text.text = result;
        } else {
        }
    }
    void Update()
    {
        
    }
}
