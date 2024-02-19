using UnityEngine;
using UnityEngine.Pool;

public class MovingObstacleSpawner : MonoBehaviour
{
    public static MovingObstacleSpawner Instance;

    public ObjectPool<MovingObstacle> Pool;
    public GameObject Prefab;

    private LevelCreator LevelCreator => LevelCreator.Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Pool = new ObjectPool<MovingObstacle>
            (CreateObject, OnTakeObjectFromPool, OnReturnObjectToPool, OnDestroyObject, true, 200, 500);
    }

    private MovingObstacle CreateObject()
    {
        GameObject obj = Instantiate(Prefab, transform.position, transform.rotation);
        MovingObstacle obstacle = obj.GetComponent<MovingObstacle>();
        obstacle.SetPool(Pool);
        return obstacle;
    }


    private void OnTakeObjectFromPool(MovingObstacle slideObstacle)
    {
        slideObstacle.transform.SetPositionAndRotation(LevelCreator.nextSpawnPos, transform.rotation);
        slideObstacle.gameObject.SetActive(true);
    }

    private void OnReturnObjectToPool(MovingObstacle slideObstacle)
    {
        slideObstacle.gameObject.SetActive(false);
    }

    private void OnDestroyObject(MovingObstacle slideObstacle)
    {
        Destroy(slideObstacle.gameObject);
    }
}
