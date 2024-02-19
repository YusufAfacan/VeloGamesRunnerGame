using UnityEngine;
using UnityEngine.Pool;

public class BlockObstacleSpawner : MonoBehaviour
{
    public static BlockObstacleSpawner Instance;

    public ObjectPool<BlockObstacle> Pool;
    public GameObject Prefab;

    private LevelCreator LevelCreator => LevelCreator.Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Pool = new ObjectPool<BlockObstacle>
            (CreateObject, OnTakeObjectFromPool, OnReturnObjectToPool, OnDestroyObject, true, 200, 500);
    }

    private BlockObstacle CreateObject()
    {
        GameObject BlockObstacleObj = Instantiate(Prefab, transform.position, transform.rotation);
        BlockObstacle BlockObstacle = BlockObstacleObj.GetComponent<BlockObstacle>();
        BlockObstacle.SetPool(Pool);
        return BlockObstacle;
    }

    private void OnTakeObjectFromPool(BlockObstacle BlockObstacle)
    {
        BlockObstacle.transform.SetPositionAndRotation(LevelCreator.nextSpawnPos, transform.rotation);
        BlockObstacle.gameObject.SetActive(true);
    }

    private void OnReturnObjectToPool(BlockObstacle BlockObstacle)
    {
        BlockObstacle.gameObject.SetActive(false);
    }

    private void OnDestroyObject(BlockObstacle BlockObstacle)
    {
        Destroy(BlockObstacle.gameObject);
    }
}
