using UnityEngine;

public class ObstacleModel : MonoBehaviour
{
    [SerializeField]
    GameObject[] models;

    public void ChangeModel(int modelIndex)
    {
        if (modelIndex < 0 || modelIndex >= models.Length)
            Debug.LogAssertion("modelIndex is out of Range.");

        for (int i = 0; i < models.Length; i++)
        {
            models[i].gameObject.SetActive(i == modelIndex);
        }
    }
}
