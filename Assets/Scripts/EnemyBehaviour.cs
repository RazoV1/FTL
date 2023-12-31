using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyBehaviour : MonoBehaviour
{
    
    //Возьму за основу скрипт игрока и на его основных функциях напишу скрипт поведения врага
    [Header("Basa")]
    public int Hull;
    private int Sum;
    public BasicPart[] parts;
    public EnergyStorage energy;
    [Header("Weaponary")]
    public BasicPart Weaponary;
    
    [Header("Shield")]
    public BasicPart Shields;
    public int ProtLayers;
    private int LayersInCooldown;
    public bool Is_protected;
    public int ShieldTimeOut;
    [Header("Engine")]
    public BasicPart Engine;
    [Header("Controll")]
    public BasicPart ControllRoom;
    public int EvasionChance;
    //public TextMeshProUGUI evasionVis;

    private IEnumerator Restore_Shield()
    {
        
        yield return new WaitForSeconds(4);
        ProtLayers++;
        LayersInCooldown--;
        
    }

    private void UpdateHull()
    {
        Sum = 0;
        foreach (BasicPart part in parts)
        {
            Sum += part.HP;
        }
        Hull = Sum;
    }
    
    private void UpdateShields()
    {
        
        if (Shields.UsingEnergy % 2 == 0 || Shields.HP<=0)
        {
            ProtLayers = (Shields.UsingEnergy / 2) - LayersInCooldown;
            
        }
        else
        {
            ProtLayers = ((Shields.UsingEnergy - 1) / 2) - LayersInCooldown;
        }

    }

    /*private void UpdateEvasion()
    {
        EvasionChance = (ControllRoom.UsingEnergy + Engine.UsingEnergy);
        if (EvasionChance == 0)
        {
            evasionVis.color = Color.red;
        }
        else
        {
            evasionVis.color = Color.green;
        }
        evasionVis.text = "evasion: "+EvasionChance*10f+"%";
    }*/

    public void TakeDamage(BasicPart part,bool through_shields, int damage)
    {
        if (Random.Range(1, 11) == EvasionChance)
        {
            Debug.Log("Evaded!");
            return;
        }
        else
        {
            if (ProtLayers > 0 && !through_shields)
            {
                LayersInCooldown++;
                ProtLayers--;
                StartCoroutine(Restore_Shield());
            }
            else
            {
                if (part.gameObject.name == "Shields")
                {
                    part.HP -= 2;
                    if (part.MaxEnergy >= 2)
                    {
                        
                        part.MaxEnergy -= 2;

                        if (part.UsingEnergy >= 2)
                        {
                            if (part.UsingEnergy % 2 == 0)
                            {
                                part.UsingEnergy -= 2;
                                energy.FreePoints += 2;
                                foreach (GameObject g in energy.powerVisualiser)
                                {
                                    if (!g.active)
                                    {
                                        g.SetActive(true);
                                        break;
                                    }
                                }
                                foreach (GameObject g in energy.powerVisualiser)
                                {
                                    if (!g.active)
                                    {
                                        g.SetActive(true);
                                        break;
                                    }
                                }
                                for (int i = part.powers.Length - 1; i >= 0; i--)
                                {
                                    if (part.powers[i].active)
                                    {
                                        part.powers[i].SetActive(false);
                                        break;
                                    }
                                }
                                for (int i = part.powers.Length - 1; i >= 0; i--)
                                {
                                    if (part.powers[i].active)
                                    {
                                        part.powers[i].SetActive(false);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                part.UsingEnergy --;
                                energy.FreePoints ++;
                                foreach (GameObject g in energy.powerVisualiser)
                                {
                                    if (!g.active)
                                    {
                                        g.SetActive(true);
                                        break;
                                    }
                                }
                                for (int i = part.powers.Length - 1; i >= 0; i--)
                                {
                                    if (part.powers[i].active)
                                    {
                                        part.powers[i].SetActive(false);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    part.HP -= damage;
                }
            }
        }
        
    }
    public void Update()
    {
        //UpdateEvasion();
        UpdateHull();
        UpdateShields();
    }
}
