using UnityEngine.Pool;

public class SlideObstacle : Obstacle
{
    private ObjectPool<SlideObstacle> _pool;

    public void SetPool(ObjectPool<SlideObstacle> pool)
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
