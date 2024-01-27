using UnityEngine;

[CreateAssetMenu(fileName = "Profile", menuName = "Profile", order = 0)]
public class Profile : ScriptableObject
{
    public int ID;
    public string profileName;
    public string dialogue;
    public Sprite sprite;


    public int turnLimit;
    public int health;
}
