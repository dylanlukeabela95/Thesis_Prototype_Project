using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SumCube
{
    private string textOnCube;
    private Material materialOnCube;
    private int uniqueId;

    public string TextOnCube
    {
        get => textOnCube;
        set => textOnCube = value;
    }

    public Material MaterialOnCube
    {
        get => materialOnCube;
        set => materialOnCube = value;
    }

    public int UniqueId
    {
        get => uniqueId;
        set => uniqueId = value;
    }

    public SumCube()
    {
        
    }
    
    public SumCube(string textOnCube, Material materialOnCube, int uniqueId)
    {
        TextOnCube = textOnCube;
        MaterialOnCube = materialOnCube;
        UniqueId = uniqueId;
    }
}
