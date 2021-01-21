namespace HitIt.Ecs
{
    public interface IIterator<T>
    {
        T Next();
        bool HasNext();
    }
}