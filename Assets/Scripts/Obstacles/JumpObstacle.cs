using UnityEngine.Pool;

public class JumpObstacle : Obstacle
{
    private ObjectPool<JumpObstacle> _pool;

    public void SetPool(ObjectPool<JumpObstacle> pool)
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
