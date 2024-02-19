using UnityEngine;
using UnityEngine.Pool;

public class TrainObstacleSpawner : MonoBehaviour
{
    public static TrainObstacleSpawner Instance;

    public ObjectPool<TrainObstacle> Pool;
    public GameObject Prefab;

    private LevelCreator LevelCreator => LevelCreator.Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Pool = new ObjectPool<TrainObstacle>
            (CreateObject, OnTakeObjectFromPool, OnReturnObjectToPool, OnDestroyObject, true, 200, 500);
    }

    private TrainObstacle CreateObject()
    {
        GameObject obj = Instantiate(Prefab, transform.position, transform.rotation);
        TrainObstacle obstacle = obj.GetComponent<TrainObstacle>();
        obstacle.SetPool(Pool);
        return obstacle;
    }


    private void OnTakeObjectFromPool(TrainObstacle slideObstacle)
    {
        slideObstacle.transform.SetPositionAndRotation(LevelCreator.nextSpawnPos, transform.rotation);
        slideObstacle.gameObject.SetActive(true);
    }

    private void OnReturnObjectToPool(TrainObstacle slideObstacle)
    {
        slideObstacle.gameObject.SetActive(false);
    }

    private void OnDestroyObject(TrainObstacle slideObstacle)
    {
        Destroy(slideObstacle.gameObject);
    }
}
