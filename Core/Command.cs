namespace Core
{
    public class Command<T>
    {
        public static Action<T> Handler;

        public void Handle(T t)
        {
            Handler(t);
        }
    }
}