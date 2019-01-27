using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Puck", menuName = "puck")]
public class puckData : ScriptableObject
{
    public int Value; //positive for good pucks, negative for bad
    public Sprite artwork;
}
