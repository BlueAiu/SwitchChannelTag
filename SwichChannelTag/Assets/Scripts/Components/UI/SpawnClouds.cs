using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public enum Direction
{
    Left,
    Right
}

public class SpawnClouds : MonoBehaviour
{

    [Tooltip("ê∂ê¨å≥ÇÃâÊëú")]
    [SerializeField] Sprite[] Clouds;
    [Tooltip("ê∂ê¨ä‘äu")]
    [SerializeField] float Spawn_interval;

    [SerializeField] float Max_position;
    [SerializeField] float Min_position;

    [Tooltip("à⁄ìÆë¨ìx")]
    [SerializeField] float Move_speed;

    [SerializeField] private Transform Left_SpawnPoint;
    [SerializeField] private Transform Right_SpawnPoint;

    private float Min_Y_Offset = -2f;
    private float Max_Y_Offset = 2f;

    private float Max_size = 1.5f;  
    private float Min_size = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

        if(Clouds == null || Clouds.Length == 0)
        {
            Debug.LogError("Image is not attached.");
            return;
        }

        StartCoroutine(Spawn_coroutine());
    }

    private void Spawn_Clouds()
    {
        bool Fromleft = Random.value < 0.5f;

        Transform spawnPoint = Fromleft ? Left_SpawnPoint : Right_SpawnPoint;

        if(spawnPoint == null)
        {
            Debug.LogError("spawnPoint is null");
            return;
        }

        Transform destroyPoint = Fromleft ? Right_SpawnPoint : Left_SpawnPoint;

        Direction dir = Fromleft ? Direction.Right : Direction.Left;

        GameObject cloud = CreateCloud();

        cloud.transform.position = Get_position(spawnPoint);

        cloud.AddComponent<MoveClouds>().Initialize(Move_speed, dir, destroyPoint);

    }

    private GameObject CreateCloud()
    {
        Sprite cloud_prefab = Clouds[Random.Range(0, Clouds.Length)];

        GameObject cloud = new GameObject("Cloud");

        SpriteRenderer rend = cloud.AddComponent<SpriteRenderer>();
        rend.sprite = cloud_prefab;

        Color c = rend.color;
        c.a = 0.7f;
        rend.color = c;

        float scale = Random.Range(Min_size, Max_size);
        cloud.transform.localScale = Vector3.one * scale;

        return cloud;
    }

    private Vector3 Get_position(Transform spawnPoint)
    {
        return new Vector3(
        spawnPoint.position.x,
        spawnPoint.position.y + Random.Range(Min_Y_Offset, Max_Y_Offset),
        spawnPoint.position.z
    );
    }

    private IEnumerator Spawn_coroutine()
    {
        while(true)
        {
            Spawn_Clouds();
            yield return new WaitForSeconds(Spawn_interval);
        }
    }
}
