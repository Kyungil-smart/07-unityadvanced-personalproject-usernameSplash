using UnityEngine;

public static class GameState
{
    public static Transform PlayerTransform;
    public static BulletManager BulletManagerInstance;
    public static PlayerLogic PlayerData;
    public static Vector3 Center = new Vector3(0, 0, 0);
}