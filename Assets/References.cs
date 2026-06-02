using System.Collections.Generic;
using UnityEngine;

public static class References
{
    public static GameObject thePlayer;
    public static GameObject canvas;
    public static Screenshake screenshake;
    public static LevelManager levelManager;

    public static LayerMask wallsLayer = LayerMask.GetMask("Walls");
    public static LayerMask enemiesLayer = LayerMask.GetMask("Enemies");

    public static List<NavPoint> navPoints = new();
    public static List<EnemySpawner> spawners = new();
    public static List<EnemyBehaviour> allEnemies = new();

    public const float maxDistanceInALevel = 1000;
}
