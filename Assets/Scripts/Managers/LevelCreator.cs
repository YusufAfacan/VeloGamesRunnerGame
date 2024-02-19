using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public static LevelCreator Instance { get; private set; }

    [HideInInspector] public Vector3 nextSpawnPos;
    [HideInInspector] public Vector3 nextGoldSpawnPos;
    [SerializeField] private float _nextSpawnTime;
    [SerializeField] private float _spawnTime;
    [SerializeField] private float _nextGoldSpawnTime;
    [SerializeField] private float _goldSpawnTime;

    BlockObstacleSpawner BlockObstacleSpawner => BlockObstacleSpawner.Instance;
    JumpObstacleSpawner JumpObstacleSpawner => JumpObstacleSpawner.Instance;
    MovingObstacleSpawner MovingObstacleSpawner => MovingObstacleSpawner.Instance;
    SlideObstacleSpawner SlideObstacleSpawner => SlideObstacleSpawner.Instance;
    TrainBodyObstacleSpawner TrainBodyObstacleSpawner => TrainBodyObstacleSpawner.Instance;
    TrainObstacleSpawner TrainObstacleSpawner => TrainObstacleSpawner.Instance;
    CoinSpawner CoinSpawner => CoinSpawner.Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        _spawnTime += Time.deltaTime;
        _goldSpawnTime += Time.deltaTime;

        if (_spawnTime >= _nextSpawnTime)
        {
            int i = Random.Range(0, 6);
            int j = Random.Range(0, 3);

            if (j == 0) nextSpawnPos = new Vector3(-2, 0, 70);
            if (j == 1) nextSpawnPos = new Vector3(0, 0, 70);
            if (j == 2) nextSpawnPos = new Vector3(2, 0, 70);

            if (i == 0) BlockObstacleSpawner.Pool.Get();
            if (i == 1) JumpObstacleSpawner.Pool.Get();
            if (i == 2) MovingObstacleSpawner.Pool.Get();
            if (i == 3) SlideObstacleSpawner.Pool.Get();
            if (i == 4) TrainBodyObstacleSpawner.Pool.Get();
            if (i == 5) TrainObstacleSpawner.Pool.Get();

            _spawnTime = 0;
        }

        if (_goldSpawnTime >= _nextGoldSpawnTime)
        {
            int j = Random.Range(0, 6);

            if (j == 0) nextGoldSpawnPos = new Vector3(-2, 0, 70);
            if (j == 1) nextGoldSpawnPos = new Vector3( 0, 0, 70);
            if (j == 2) nextGoldSpawnPos = new Vector3( 2, 0, 70);
            if (j == 3) nextGoldSpawnPos = new Vector3(-2, 4, 70);
            if (j == 4) nextGoldSpawnPos = new Vector3( 0, 4, 70);
            if (j == 5) nextGoldSpawnPos = new Vector3( 2, 4, 70);

            CoinSpawner.Pool.Get();

            _goldSpawnTime = 0;
        }
    }   
}
