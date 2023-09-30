using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static void startGame()
    {
        Character player = new Character("Hero", 100, 5, 30, 10, new int[2, 2] { { 1, 2 }, { 3, 4 } });
        Monster enemy = new Monster("Dragon", 200, 5, 30, 10, new int[2, 2] { { 1, 2 }, { 3, 4 } }, "Fire-breathing", 20);

        // Game logic can go here
        enemy.Roar();
    }
}
