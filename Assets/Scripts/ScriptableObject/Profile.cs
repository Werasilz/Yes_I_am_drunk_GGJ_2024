using UnityEngine;

[CreateAssetMenu(fileName = "Profile", menuName = "Profile", order = 0)]
public class Profile : ScriptableObject
{
    public string profileName;
    public string dialogue;
    public Sprite sprite;
}
