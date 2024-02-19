using UnityEngine;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    public static CoinSpawner Instance;

    public ObjectPool<Coin> Pool;
    public GameObject Prefab;

    private LevelCreator LevelCreator => LevelCreator.Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Pool = new ObjectPool<Coin>
            (CreateObject, OnTakeObjectFromPool, OnReturnObjectToPool, OnDestroyObject, true, 200, 500);
    }

    private Coin CreateObject()
    {
        GameObject obj = Instantiate(Prefab, transform.position, transform.rotation);
        Coin obstacle = obj.GetComponent<Coin>();
        obstacle.SetPool(Pool);
        return obstacle;
    }


    private void OnTakeObjectFromPool(Coin slideObstacle)
    {
        slideObstacle.transform.SetPositionAndRotation(LevelCreator.nextGoldSpawnPos, transform.rotation);
        slideObstacle.gameObject.SetActive(true);
    }

    private void OnReturnObjectToPool(Coin slideObstacle)
    {
        slideObstacle.gameObject.SetActive(false);
    }

    private void OnDestroyObject(Coin slideObstacle)
    {
        Destroy(slideObstacle.gameObject);
    }
}
