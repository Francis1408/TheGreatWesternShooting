using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinAnimatorManager : AnimationManager
{
    private List<string> prefixes;

    private void Start()
    {
        prefixes = new List<string> {"Blonde"};
    }

    public override string ConcatenatePrefixes(string movementType) // PART STYLE + BODY PART + TYPE OF MOVEMENT 
    {
        return prefixes[0] + "Hair" + movementType;
    }
}