using UnityEngine;

[CreateAssetMenu(fileName = "QuestionData", menuName = "QuestionData", order = 0)]
public class QuestionData : ScriptableObject
{
    [TextArea(2, 2)]
    public string[] questions;
}
