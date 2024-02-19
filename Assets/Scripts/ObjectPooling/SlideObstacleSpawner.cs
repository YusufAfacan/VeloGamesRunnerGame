using UnityEngine;
using UnityEngine.Pool;

public class SlideObstacleSpawner : MonoBehaviour
{
    public static SlideObstacleSpawner Instance;

    public ObjectPool<SlideObstacle> Pool;
    public GameObject Prefab;

    private LevelCreator LevelCreator => LevelCreator.Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Pool = new ObjectPool<SlideObstacle>
            (CreateObject, OnTakeObjectFromPool, OnReturnObjectToPool, OnDestroyObject, true, 200, 500);
    }

    private SlideObstacle CreateObject()
    {
        GameObject obj = Instantiate(Prefab, transform.position, transform.rotation);
        SlideObstacle obstacle = obj.GetComponent<SlideObstacle>();
        obstacle.SetPool(Pool);
        return obstacle;
    }


    private void OnTakeObjectFromPool(SlideObstacle slideObstacle)
    {
        slideObstacle.transform.SetPositionAndRotation(LevelCreator.nextSpawnPos, transform.rotation);
        slideObstacle.gameObject.SetActive(true);
    }

    private void OnReturnObjectToPool(SlideObstacle slideObstacle)
    {
        slideObstacle.gameObject.SetActive(false);
    }

    private void OnDestroyObject(SlideObstacle slideObstacle)
    {
        Destroy(slideObstacle.gameObject);
    }
}
