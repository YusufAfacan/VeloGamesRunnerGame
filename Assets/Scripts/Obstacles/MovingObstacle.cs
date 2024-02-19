using UnityEngine.Pool;

public class MovingObstacle : Obstacle
{
    private ObjectPool<MovingObstacle> _pool;

    public void SetPool(ObjectPool<MovingObstacle> pool)
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
