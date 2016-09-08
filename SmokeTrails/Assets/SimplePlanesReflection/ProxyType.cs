namespace Assets.SimplePlanesReflection
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;

   /// <summary>
   /// A proxy class that wraps an underlying SimplePlanes type and provides reflection based methods for interacting with that type.
   /// </summary>
   /// <typeparam name="T">The concrete type of the proxy class.</typeparam>
   public abstract class ProxyType<T> : ProxyTypeBase<T, object>
      where T : ProxyTypeBase<T, object>
   {
      /// <summary>
      /// Creates an instance of the wrapped type, returning the proxy to that type.
      /// </summary>
      /// <returns>A proxy object wrapping the newly created instance of the real type.</returns>
      public static T Create()
      {
         return ProxyTypeBase<T, object>.Wrap(Activator.CreateInstance(ProxyTypeBase<T, object>.RealType, true));
      }

      /// <summary>
      /// Creates an instance of the wrapped type, returning the proxy to that type.
      /// </summary>
      /// <param name="constructorParameters">The constructor parameters.</param>
      /// <returns>A proxy object wrapping the newly created instance of the real type.</returns>
      public static T Create(params object[] constructorParameters)
      {
         return ProxyTypeBase<T, object>.Wrap(Activator.CreateInstance(ProxyTypeBase<T, object>.RealType, constructorParameters));
      }

      /// <summary>
      /// Creates an instance of the original real type.
      /// </summary>
      /// <returns>An instance of the original real type.</returns>
      public static object CreateUnwrapped()
      {
         return Activator.CreateInstance(ProxyTypeBase<T, object>.RealType, true);
      }

      /// <summary>
      /// Creates an instance of the original real type.
      /// </summary>
      /// <param name="constructorParameters">The constructor parameters.</param>
      /// <returns>An instance of the original real type.</returns>
      public static object CreateUnwrapped(params object[] constructorParameters)
      {
         return Activator.CreateInstance(ProxyTypeBase<T, object>.RealType, constructorParameters);
      }
   }
}