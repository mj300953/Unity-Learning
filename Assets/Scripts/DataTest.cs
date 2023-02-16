using UnityEngine;

public class DataTest : MonoBehaviour
{
    [SerializeField] private ApplicationData data;

    private void Start()
    {
        Debug.Log(data.Value);
    }
}