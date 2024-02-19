using UnityEngine.Pool;

public class BlockObstacle : Obstacle
{
    private ObjectPool<BlockObstacle> _pool;

    public void SetPool(ObjectPool<BlockObstacle> pool)
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
