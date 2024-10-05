namespace Zeva
{
   public interface IGetLongValue<T> where T : class
   {
      long GetValue(T item);
   }
}
