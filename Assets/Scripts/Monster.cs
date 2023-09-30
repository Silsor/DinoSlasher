using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Monster : Character
{
    public string Type { get; set; }
    public Monster(string v, int initialHP, int initialMovementSpeed, int initialPowerCombat, int initialSpeedCombat, int[,] initialSize, string type, int damage)
        : base(initialHP, initialMovementSpeed, initialPowerCombat, initialSpeedCombat, initialSize)
    {
        Type = type;
    
    }

    public void Roar()
    {
        Console.WriteLine($"{Name} the {Type} monster roars!");
    }
}
