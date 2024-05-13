public class ShootComposite : SequenceComposite, IShootStrategy
{
    public BulletSpawner BulletSpawner { get; } = null;

    public ShootComposite(IStrategyPattern[] children) : base(children)
    {
    }
}
