using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Puck", menuName = "puck")]
public class puckData : ScriptableObject
{
    public int type; //1 for basic, 2 for moving, 3 for explosive
    public int value; //positive for good pucks, negative for bad
    public Sprite artwork;
}
