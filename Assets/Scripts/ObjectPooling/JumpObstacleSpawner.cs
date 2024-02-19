using UnityEngine;
using UnityEngine.Pool;

public class JumpObstacleSpawner : MonoBehaviour
{
    public static JumpObstacleSpawner Instance;

    public ObjectPool<JumpObstacle> Pool;
    public GameObject Prefab;

    private LevelCreator LevelCreator => LevelCreator.Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Pool = new ObjectPool<JumpObstacle>
            (CreateObject, OnTakeObjectFromPool, OnReturnObjectToPool, OnDestroyObject, true, 200, 500);
    }

    private JumpObstacle CreateObject()
    {
        GameObject obj = Instantiate(Prefab, transform.position, transform.rotation);
        JumpObstacle obstacle = obj.GetComponent<JumpObstacle>();
        obstacle.SetPool(Pool);
        return obstacle;
    }


    private void OnTakeObjectFromPool(JumpObstacle slideObstacle)
    {
        slideObstacle.transform.SetPositionAndRotation(LevelCreator.nextSpawnPos, transform.rotation);
        slideObstacle.gameObject.SetActive(true);
    }

    private void OnReturnObjectToPool(JumpObstacle slideObstacle)
    {
        slideObstacle.gameObject.SetActive(false);
    }

    private void OnDestroyObject(JumpObstacle slideObstacle)
    {
        Destroy(slideObstacle.gameObject);
    }
}
