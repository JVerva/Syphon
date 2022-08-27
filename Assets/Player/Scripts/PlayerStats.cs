using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    //all player stats, available to all scripts
    public static float walkSpeed = 1;
    public static float runSpeed = 1;
    public static float jumpHeight = 1;
    public static float playerHeight = 2;
    public static float playerRadius = 0.5f;
    public static float accelaration = 1f;
    public static float stopAccelaration = 2f;
    public static float arealControl = 0.2f;
    public static float turnSpeed = 2;
    public static float interactionReach = 5;
    public static float tiltFactor = 1 / 2;
    public static float tiltSpeed = 3;
    public static float maxWalkAngle = 45;
}
