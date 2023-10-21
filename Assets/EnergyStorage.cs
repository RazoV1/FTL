using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyStorage : MonoBehaviour
{
    public int MaxPoints;
    public int FreePoints;
    
    public void POWER(BasicPart part)
    {
        if (part.MaxEnergy != 0 && part.UsingEnergy < part.MaxEnergy && FreePoints > 0)
        {
            part.UsingEnergy++;
            FreePoints--;
        }
    }
}
