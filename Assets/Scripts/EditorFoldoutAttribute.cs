using UnityEngine;

public class EditorFoldoutAttribute : PropertyAttribute
{
    public readonly string label;

    public EditorFoldoutAttribute(string label)
    {
        this.label = label;
    }
}