using UnityEngine.Pool;

public class TrainObstacle : Obstacle
{
    private ObjectPool<TrainObstacle> _pool;

    public void SetPool(ObjectPool<TrainObstacle> pool)
    {
        _pool = pool;
    }

    private void Update()
    {
        if (transform.position.z < minZPos)
        {
            _pool.Release(this);
        }
    }
}
