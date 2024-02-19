using UnityEngine.Pool;

public class TrainBodyObstacle : Obstacle
{
    private ObjectPool<TrainBodyObstacle> _pool;

    public void SetPool(ObjectPool<TrainBodyObstacle> pool)
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
