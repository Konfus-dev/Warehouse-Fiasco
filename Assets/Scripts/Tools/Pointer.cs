
// how to use: https://stackoverflow.com/questions/1434840/c-copy-one-bool-to-another-by-ref-not-val
public class Pointer<T> where T : struct
{
    public T Value { get; set; }

    public bool CoolingDown { get; set; } = false;

    public Pointer() { }

    public Pointer(T value)
    {
        this.Value = value;
    }

    public static implicit operator T(Pointer<T> wrapper)
    {
        if (wrapper == null)
        {
            return default(T);
        }
        return wrapper.Value;
    }
}

