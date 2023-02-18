using UnityEngine;

[CreateAssetMenu(fileName = "DefaultName", menuName = "Data/Application")]
public class ApplicationData : ScriptableObject
{
    [SerializeField] private float value;
    [SerializeField] private float value2;
    [SerializeField] private string value3;

    public float Value => value;
    public float Value2 => value2;
    public string Value3 => value3;
}