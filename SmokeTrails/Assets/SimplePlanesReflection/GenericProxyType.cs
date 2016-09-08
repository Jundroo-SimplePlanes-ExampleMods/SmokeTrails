namespace Assets.SimplePlanesReflection
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;

   /// <summary>
   /// A proxy class that wraps an underlying SimplePlanes type and provides reflection based methods for interacting with that type.
   /// </summary>
   /// <typeparam name="TProxy">The concrete type of the proxy class.</typeparam>
   /// <typeparam name="TRealObject">The type of the real object being wrapped.
   /// This does not need to be the exact subclass. It may be a high level base class such as MonoBehaviour.</typeparam>
   public abstract class GenericProxyType<TProxy, TRealObject> : ProxyTypeBase<TProxy, TRealObject>
      where TProxy : ProxyTypeBase<TProxy, TRealObject>
   {
      /// <summary>
      /// Creates an instance of the wrapped type, returning the proxy to that type.
      /// </summary>
      /// <returns>A proxy object wrapping the newly created instance of the real type.</returns>
      public static TProxy Create()
      {
         return ProxyTypeBase<TProxy, TRealObject>.Wrap(Activator.CreateInstance<TRealObject>());
      }
   }
}