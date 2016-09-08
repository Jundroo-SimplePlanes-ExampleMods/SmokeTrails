namespace Assets.SimplePlanesReflection
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using UnityEngine;

   /// <summary>
   /// A proxy class that wraps an underlying SimplePlanes type and provides reflection based methods for interacting with that type.
   /// </summary>
   /// <typeparam name="T">The concrete type of the proxy class.</typeparam>
   public abstract class MonoBehaviourProxyType<T> : ProxyTypeBase<T, MonoBehaviour>
      where T : ProxyTypeBase<T, MonoBehaviour>
   {
      /// <summary>
      /// Adds the wrapped monobehaviour type as a component to the specified game object.
      /// </summary>
      /// <param name="gameObject">The game object to which to add the component.</param>
      /// <returns>The proxy wrapping the added component.</returns>
      public static T AddComponent(GameObject gameObject)
      {
         return ProxyTypeBase<T, MonoBehaviour>.Wrap(gameObject.AddComponent(ProxyTypeBase<T, MonoBehaviour>.RealType));
      }

      /// <summary>
      /// Returns a proxy to the component of specified type if the game object has one attached, <c>null</c> if it doesn't.
      /// </summary>
      /// <param name="gameObject">The game object from which to retrieve the component.</param>
      /// <returns>A proxy to the component of specified type if the game object has one attached, <c>null</c> if it doesn't.</returns>
      public static T GetComponent(GameObject gameObject)
      {
         var obj = gameObject.GetComponent(ProxyTypeBase<T, MonoBehaviour>.RealType) as MonoBehaviour;
         return obj == null ? null : ProxyTypeBase<T, MonoBehaviour>.Wrap(obj);
      }

      /// <summary>
      /// Returns a proxy to the component of specified type if the component object has one attached, <c>null</c> if it doesn't.
      /// </summary>
      /// <param name="component">The component object from which to retrieve the component.</param>
      /// <returns>A proxy to the component of specified type if the component object has one attached, <c>null</c> if it doesn't.</returns>
      public static T GetComponent(Component component)
      {
         var obj = component.GetComponent(ProxyTypeBase<T, MonoBehaviour>.RealType) as MonoBehaviour;
         return obj == null ? null : ProxyTypeBase<T, MonoBehaviour>.Wrap(obj);
      }

      /// <summary>
      /// Returns a proxy to the component of specified type attached to the game object or any of its children. <c>null</c> if the component could not be found.
      /// </summary>
      /// <param name="gameObject">The game object from which to retrieve the component.</param>
      /// <returns>A proxy to the component of specified type attached to the game object or any of its children. <c>null</c> if the component could not be found.</returns>
      public static T GetComponentInChild(GameObject gameObject)
      {
         var obj = gameObject.GetComponentInChildren(ProxyTypeBase<T, MonoBehaviour>.RealType) as MonoBehaviour;
         return obj == null ? null : ProxyTypeBase<T, MonoBehaviour>.Wrap(obj);
      }

      /// <summary>
      /// Returns a proxy to the component of specified type attached to the game object or any of its children. <c>null</c> if the component could not be found.
      /// </summary>
      /// <param name="gameObject">The game object from which to retrieve the component.</param>
      /// <param name="includeInactive">A value indicating if components on inactive game objects be included.</param>
      /// <returns>A proxy to the component of specified type attached to the game object or any of its children. <c>null</c> if the component could not be found.</returns>
      public static T GetComponentInChild(GameObject gameObject, bool includeInactive)
      {
         var obj = gameObject.GetComponentInChildren(ProxyTypeBase<T, MonoBehaviour>.RealType, includeInactive) as MonoBehaviour;
         return obj == null ? null : ProxyTypeBase<T, MonoBehaviour>.Wrap(obj);
      }

      /// <summary>
      /// Returns a proxy to the component of specified type attached to the component object or any of its children. <c>null</c> if the component could not be found.
      /// </summary>
      /// <param name="component">The component object from which to retrieve the component.</param>
      /// <returns>A proxy to the component of specified type attached to the component object or any of its children. <c>null</c> if the component could not be found.</returns>
      public static T GetComponentInChild(Component component)
      {
         var obj = component.GetComponentInChildren(ProxyTypeBase<T, MonoBehaviour>.RealType) as MonoBehaviour;
         return obj == null ? null : ProxyTypeBase<T, MonoBehaviour>.Wrap(obj);
      }

      /// <summary>
      /// Returns a proxy to the component of specified type attached to the component object or any of its children. <c>null</c> if the component could not be found.
      /// </summary>
      /// <param name="component">The component object from which to retrieve the component.</param>
      /// <param name="includeInactive">A value indicating if components on inactive game objects be included.</param>
      /// <returns>A proxy to the component of specified type attached to the component object or any of its children. <c>null</c> if the component could not be found.</returns>
      public static T GetComponentInChild(Component component, bool includeInactive)
      {
         var obj = component.GetComponentInChildren(ProxyTypeBase<T, MonoBehaviour>.RealType, includeInactive) as MonoBehaviour;
         return obj == null ? null : ProxyTypeBase<T, MonoBehaviour>.Wrap(obj);
      }

      /// <summary>
      /// Returns a proxy to the component of specified type attached to the game object or any of its parents. <c>null</c> if the component could not be found.
      /// </summary>
      /// <param name="gameObject">The game object from which to retrieve the component.</param>
      /// <returns>A proxy to the component of specified type attached to the game object or any of its parents. <c>null</c> if the component could not be found.</returns>
      public static T GetComponentInParent(GameObject gameObject)
      {
         var obj = gameObject.GetComponentInParent(ProxyTypeBase<T, MonoBehaviour>.RealType) as MonoBehaviour;
         return obj == null ? null : ProxyTypeBase<T, MonoBehaviour>.Wrap(obj);
      }

      /// <summary>
      /// Returns a proxy to the component of specified type attached to the component object or any of its parents. <c>null</c> if the component could not be found.
      /// </summary>
      /// <param name="component">The component object from which to retrieve the component.</param>
      /// <returns>A proxy to the component of specified type attached to the component object or any of its parents. <c>null</c> if the component could not be found.</returns>
      public static T GetComponentInParent(Component component)
      {
         var obj = component.GetComponentInParent(ProxyTypeBase<T, MonoBehaviour>.RealType) as MonoBehaviour;
         return obj == null ? null : ProxyTypeBase<T, MonoBehaviour>.Wrap(obj);
      }

      /// <summary>
      /// Returns proxies to all the components of specified type attached to the game object.
      /// </summary>
      /// <param name="gameObject">The game object from which to retrieve the components.</param>
      /// <returns>Proxies to all the components of specified type attached to the game object.</returns>
      public static T[] GetComponents(GameObject gameObject)
      {
         var components = gameObject.GetComponents(ProxyTypeBase<T, MonoBehaviour>.RealType);
         var results = new T[components.Length];

         for (int i = 0; i < results.Length; i++)
         {
            results[i] = ProxyTypeBase<T, MonoBehaviour>.Wrap(components[i]);
         }

         return results;
      }

      /// <summary>
      /// Returns proxies to all the components of specified type attached to the component object.
      /// </summary>
      /// <param name="component">The component object from which to retrieve the components.</param>
      /// <returns>Proxies to all the components of specified type attached to the component object.</returns>
      public static T[] GetComponents(Component component)
      {
         var components = component.GetComponents(ProxyTypeBase<T, MonoBehaviour>.RealType);
         var results = new T[components.Length];

         for (int i = 0; i < results.Length; i++)
         {
            results[i] = ProxyTypeBase<T, MonoBehaviour>.Wrap(components[i]);
         }

         return results;
      }

      /// <summary>
      /// Returns proxies to all the components of specified type attached to the game object or any of its children.
      /// </summary>
      /// <param name="gameObject">The game object from which to retrieve the components.</param>
      /// <returns>Proxies to all the components of specified type attached to the game object or any of its children.</returns>
      public static T[] GetComponentsInChildren(GameObject gameObject)
      {
         var components = gameObject.GetComponentsInChildren(ProxyTypeBase<T, MonoBehaviour>.RealType);
         var results = new T[components.Length];

         for (int i = 0; i < results.Length; i++)
         {
            results[i] = ProxyTypeBase<T, MonoBehaviour>.Wrap(components[i]);
         }

         return results;
      }

      /// <summary>
      /// Returns proxies to all the components of specified type attached to the game object or any of its children.
      /// </summary>
      /// <param name="gameObject">The game object from which to retrieve the components.</param>
      /// <param name="includeInactive">A value indicating if components on inactive game objects be included.</param>
      /// <returns>Proxies to all the components of specified type attached to the game object or any of its children.</returns>
      public static T[] GetComponentsInChildren(GameObject gameObject, bool includeInactive)
      {
         var components = gameObject.GetComponentsInChildren(ProxyTypeBase<T, MonoBehaviour>.RealType, includeInactive);
         var results = new T[components.Length];

         for (int i = 0; i < results.Length; i++)
         {
            results[i] = ProxyTypeBase<T, MonoBehaviour>.Wrap(components[i]);
         }

         return results;
      }

      /// <summary>
      /// Returns proxies to all the components of specified type attached to the component object or any of its children.
      /// </summary>
      /// <param name="component">The component object from which to retrieve the components.</param>
      /// <returns>Proxies to all the components of specified type attached to the component object or any of its children.</returns>
      public static T[] GetComponentsInChildren(Component component)
      {
         var components = component.GetComponentsInChildren(ProxyTypeBase<T, MonoBehaviour>.RealType);
         var results = new T[components.Length];

         for (int i = 0; i < results.Length; i++)
         {
            results[i] = ProxyTypeBase<T, MonoBehaviour>.Wrap(components[i]);
         }

         return results;
      }

      /// <summary>
      /// Returns proxies to all the components of specified type attached to the component object or any of its children.
      /// </summary>
      /// <param name="component">The component object from which to retrieve the components.</param>
      /// <param name="includeInactive">A value indicating if components on inactive game objects be included.</param>
      /// <returns>Proxies to all the components of specified type attached to the component object or any of its children.</returns>
      public static T[] GetComponentsInChildren(Component component, bool includeInactive)
      {
         var components = component.GetComponentsInChildren(ProxyTypeBase<T, MonoBehaviour>.RealType, includeInactive);
         var results = new T[components.Length];

         for (int i = 0; i < results.Length; i++)
         {
            results[i] = ProxyTypeBase<T, MonoBehaviour>.Wrap(components[i]);
         }

         return results;
      }

      /// <summary>
      /// Returns proxies to all the components of specified type attached to the game object or any of its parents.
      /// </summary>
      /// <param name="gameObject">The game object from which to retrieve the components.</param>
      /// <returns>Proxies to all the components of specified type attached to the game object or any of its parents.</returns>
      public static T[] GetComponentsInParent(GameObject gameObject)
      {
         var components = gameObject.GetComponentsInParent(ProxyTypeBase<T, MonoBehaviour>.RealType);
         var results = new T[components.Length];

         for (int i = 0; i < results.Length; i++)
         {
            results[i] = ProxyTypeBase<T, MonoBehaviour>.Wrap(components[i]);
         }

         return results;
      }

      /// <summary>
      /// Returns proxies to all the components of specified type attached to the game object or any of its parents.
      /// </summary>
      /// <param name="gameObject">The game object from which to retrieve the components.</param>
      /// <param name="includeInactive">A value indicating if components on inactive game objects be included.</param>
      /// <returns>Proxies to all the components of specified type attached to the game object or any of its parents.</returns>
      public static T[] GetComponentsInParent(GameObject gameObject, bool includeInactive)
      {
         var components = gameObject.GetComponentsInParent(ProxyTypeBase<T, MonoBehaviour>.RealType, includeInactive);
         var results = new T[components.Length];

         for (int i = 0; i < results.Length; i++)
         {
            results[i] = ProxyTypeBase<T, MonoBehaviour>.Wrap(components[i]);
         }

         return results;
      }

      /// <summary>
      /// Returns proxies to all the components of specified type attached to the component object or any of its parents.
      /// </summary>
      /// <param name="component">The component object from which to retrieve the components.</param>
      /// <returns>Proxies to all the components of specified type attached to the component object or any of its parents.</returns>
      public static T[] GetComponentsInParent(Component component)
      {
         var components = component.GetComponentsInParent(ProxyTypeBase<T, MonoBehaviour>.RealType);
         var results = new T[components.Length];

         for (int i = 0; i < results.Length; i++)
         {
            results[i] = ProxyTypeBase<T, MonoBehaviour>.Wrap(components[i]);
         }

         return results;
      }

      /// <summary>
      /// Returns proxies to all the components of specified type attached to the component object or any of its parents.
      /// </summary>
      /// <param name="component">The component object from which to retrieve the components.</param>
      /// <param name="includeInactive">A value indicating if components on inactive game objects be included.</param>
      /// <returns>Proxies to all the components of specified type attached to the component object or any of its parents.</returns>
      public static T[] GetComponentsInParent(Component component, bool includeInactive)
      {
         var components = component.GetComponentsInParent(ProxyTypeBase<T, MonoBehaviour>.RealType, includeInactive);
         var results = new T[components.Length];

         for (int i = 0; i < results.Length; i++)
         {
            results[i] = ProxyTypeBase<T, MonoBehaviour>.Wrap(components[i]);
         }

         return results;
      }
   }
}