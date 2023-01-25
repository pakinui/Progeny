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
    
    public ExpiableProperty(float timeout, T defaultValue = default(T), T value = default(T))
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


    public bool Expired()
    {
        return Time.time - mTimer > mTimeout;
    }
}
