using UnityEngine;

public interface IExpirable
{
    bool Expired();
}

public class ExpiableProperty<T>: IExpirable where T : new()
{
    private T mValue;
    private T defaultValue;
    private float mTimeout;
    private float mTimer;
    
    public ExpiableProperty(float timeout = 0, T defaultValue = default(T), T value = default(T))
    {
        mTimeout = timeout;
        mTimer = Time.time;
        this.defaultValue = defaultValue;
        mValue = value;
    }

    public T Value
    {
        get
        {
            if (Expired())
            {
                mValue = defaultValue;
            }
            return mValue;
        }
        set
        {
            mValue = value;
            mTimer = Time.time;
        }
    }
    
    public void SetWithTimeout(T value, float timeout)
    {
        mValue = value;
        mTimer = Time.time;
        mTimeout = timeout;
    }

    public bool Expired()
    {
        return Time.time - mTimer > mTimeout;
    }
}
