using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    // Variables
    private int hp; // Health Points
    private int movementSpeed; // Movement Speed
    private int initialHP;
    private int initialMovementSpeed;
    private int initialPowerCombat;
    private int initialSpeedCombat;
    private int[,] initialSize;

    private int powerCombat { get; set; }
    private int speedCombat { get; set; }
    private int[,] size { get; set; }

    public string Name { get; set; }


    //new int[xVectore, yVecotre];
    // Properties
    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }

    public int MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }

    // Constructor
    public Character(string name, int initialHP, int initialMovementSpeed, int initialPowerCombat, int initialSpeedCombat, int[,] initialSize)
    {
        Name = name;
        HP = initialHP;
        MovementSpeed = initialMovementSpeed;
        powerCombat = initialPowerCombat;
        speedCombat = initialSpeedCombat;
        size = initialSize;

    }

    public Character(int initialHP, int initialMovementSpeed, int initialPowerCombat, int initialSpeedCombat, int[,] initialSize)
    {
        this.initialHP = initialHP;
        this.initialMovementSpeed = initialMovementSpeed;
        this.initialPowerCombat = initialPowerCombat;
        this.initialSpeedCombat = initialSpeedCombat;
        this.initialSize = initialSize;
    }
}
