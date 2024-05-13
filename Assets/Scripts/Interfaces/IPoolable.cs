using System;

public interface IPoolable
{
    void SetDisableCallbackAction(Action<IPoolable> callback);
}
