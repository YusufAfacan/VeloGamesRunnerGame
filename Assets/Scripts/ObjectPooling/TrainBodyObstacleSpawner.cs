using UnityEngine;
using UnityEngine.Pool;

public class TrainBodyObstacleSpawner : MonoBehaviour
{
    public static TrainBodyObstacleSpawner Instance;

    public ObjectPool<TrainBodyObstacle> Pool;
    public GameObject Prefab;

    private LevelCreator LevelCreator => LevelCreator.Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Pool = new ObjectPool<TrainBodyObstacle>
            (CreateObject, OnTakeObjectFromPool, OnReturnObjectToPool, OnDestroyObject, true, 200, 500);
    }

    private TrainBodyObstacle CreateObject()
    {
        GameObject obj = Instantiate(Prefab, transform.position, transform.rotation);
        TrainBodyObstacle obstacle = obj.GetComponent<TrainBodyObstacle>();
        obstacle.SetPool(Pool);
        return obstacle;
    }


    private void OnTakeObjectFromPool(TrainBodyObstacle slideObstacle)
    {
        slideObstacle.transform.SetPositionAndRotation(LevelCreator.nextSpawnPos, transform.rotation);
        slideObstacle.gameObject.SetActive(true);
    }

    private void OnReturnObjectToPool(TrainBodyObstacle slideObstacle)
    {
        slideObstacle.gameObject.SetActive(false);
    }

    private void OnDestroyObject(TrainBodyObstacle slideObstacle)
    {
        Destroy(slideObstacle.gameObject);
    }
}
