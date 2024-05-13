public interface IStrategyPattern
{
    void Use();
}

public interface IShootStrategy : IStrategyPattern
{
    BulletSpawner BulletSpawner { get;}
}

public interface IAbilityStrategy : IStrategyPattern
{
}
