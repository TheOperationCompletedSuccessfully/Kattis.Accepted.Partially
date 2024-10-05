namespace Zeva
{
   public interface IGetIntValue<T> where T : class
   {
      int GetValue(T item);
   }
}
