using UnityEngine;

public class Profile
{
    public string profileName;
    public Sprite sprite;
}
[CreateAssetMenu(fileName = "EnemyProfileData", menuName = "EnemyProfileData", order = 0)]
public class EnemyProfileData : ScriptableObject
{
    public Profile[] profiles;
}
