namespace Assets.SimplePlanesReflection
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Linq.Expressions;
   using System.Reflection;
   using System.Text;
   using UnityEngine;

   /// <summary>
   /// A proxy base class that wraps an underlying SimplePlanes type and provides reflection based methods for interacting with that type.
   /// </summary>
   /// <typeparam name="TProxy">The concrete type of the proxy class.</typeparam>
   /// <typeparam name="TRealObject">The type of the real object being wrapped.
   /// This does not need to be the exact subclass. It may be a high level base class such as MonoBehaviour.</typeparam>
   public abstract class ProxyTypeBase<TProxy, TRealObject>
      where TProxy : ProxyTypeBase<TProxy, TRealObject>
   {
      /// <summary>
      /// A collection of common binding flags usable for most reflection methods.
      /// </summary>
      private static readonly BindingFlags AllBindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

      /// <summary>
      /// The proxy factory method.
      /// </summary>
      private static Func<TProxy> _proxyFactory = GetProxyFactory();

      /// <summary>
      /// The real type represented by this proxy type.
      /// </summary>
      private static Type _realType;

      /// <summary>
      /// Initializes a new instance of the <see cref="ProxyTypeBase{TProxy, TRealObject}"/> class.
      /// </summary>
      protected ProxyTypeBase()
      {
      }

      /// <summary>
      /// Gets the real type represented by this proxy type.
      /// </summary>
      /// <value>
      /// The real type represented by this proxy type.
      /// </value>
      public static Type RealType
      {
         get
         {
            if (_realType == null)
            {
               var typeFullName = typeof(TProxy).FullName.Substring(30);
               _realType = ReflectionHelper.AssemblyCSharp.GetType(typeFullName, true);
            }

            return _realType;
         }
      }

      /// <summary>
      /// Gets or sets the real object being wrapped by this proxy.
      /// </summary>
      /// <value>
      /// The real object being wrapped by this proxy.
      /// </value>
      public TRealObject RealObject { get; protected set; }

      /// <summary>
      /// Gets the specified event.
      /// </summary>
      /// <param name="name">The name of the event to get.</param>
      /// <returns>The event info object.</returns>
      public static EventInfo GetEvent(string name)
      {
         var realType = RealType;

         var eventInfo = realType.GetEvent(name, AllBindings);
         if (eventInfo == null)
         {
            Debug.LogErrorFormat(
               "Could not find event via reflection: {0}.{1}",
               realType.FullName,
               name);
         }

         return eventInfo;
      }

      /// <summary>
      /// Gets the specified field.
      /// </summary>
      /// <param name="name">The name of the field to retrieve.</param>
      /// <returns>The field info object.</returns>
      public static FieldInfo GetField(string name)
      {
         var realType = RealType;

         var field = realType.GetField(name, AllBindings);
         if (field == null)
         {
            Debug.LogErrorFormat(
               "Could not find field via reflection: {0}.{1}",
               realType.FullName,
               name);
         }

         return field;
      }

      /// <summary>
      /// Gets the specified method info.
      /// </summary>
      /// <param name="name">The name of the method to retrieve.</param>
      /// <returns>The requested method info object.</returns>
      public static MethodInfo GetMethod(string name)
      {
         var realType = RealType;

         var method = realType.GetMethod(name, AllBindings);
         if (method == null)
         {
            Debug.LogErrorFormat(
               "Could not find method via reflection: {0}.{1}()",
               realType.FullName,
               name);
         }

         return method;
      }

      /// <summary>
      /// Gets the specified method info.
      /// </summary>
      /// <param name="name">The name of the method to retrieve.</param>
      /// <param name="parameters">The parameters types for the method.</param>
      /// <returns>The requested method info object.</returns>
      public static MethodInfo GetMethod(string name, params Type[] parameters)
      {
         var realType = RealType;

         var method = realType.GetMethod(name, AllBindings, null, parameters, null);
         if (method == null)
         {
            Debug.LogErrorFormat(
               "Could not find method via reflection: {0}.{1}({2})",
               realType.FullName,
               name,
               string.Join(", ", parameters.Select(x => x.FullName).ToArray()));
         }

         return method;
      }

      /// <summary>
      /// Gets the specified property.
      /// </summary>
      /// <param name="name">The name of the property to retrieve.</param>
      /// <returns>The property info object.</returns>
      public static PropertyInfo GetProperty(string name)
      {
         var realType = RealType;

         var field = realType.GetProperty(name, AllBindings);
         if (field == null)
         {
            Debug.LogErrorFormat(
               "Could not find property via reflection: {0}.{1}",
               realType.FullName,
               name);
         }

         return field;
      }

      /// <summary>
      /// Wraps the specified real object in a proxy object of this type.
      /// </summary>
      /// <param name="realObject">The real object to be wrapped by the proxy.</param>
      /// <returns>The proxy object wrapping the real object.</returns>
      public static TProxy Wrap(object realObject)
      {
         var proxy = _proxyFactory();
         proxy.RealObject = (TRealObject)realObject;
         return proxy;
      }

      /// <summary>
      /// Wraps the specified real object in a proxy object of this type.
      /// </summary>
      /// <param name="realObject">The real object to be wrapped by the proxy.</param>
      /// <returns>The proxy object wrapping the real object.</returns>
      public static TProxy Wrap(TRealObject realObject)
      {
         var proxy = _proxyFactory();
         proxy.RealObject = realObject;
         return proxy;
      }

      /// <summary>
      /// Subscribes to a static event on the class wrapped by the proxy.
      /// </summary>
      /// <param name="eventInfo">The event information.</param>
      /// <param name="handler">The event handler.</param>
      protected static void AddEventStatic(EventInfo eventInfo, EventHandler handler)
      {
         var eventDelegate = Delegate.CreateDelegate(eventInfo.EventHandlerType, handler.Target, handler.Method);
         eventInfo.AddEventHandler(null, eventDelegate);
      }

      /// <summary>
      /// Creates an object for getting and/or setting a field.
      /// </summary>
      /// <typeparam name="T">The type of the field. May be a base type or exact type.</typeparam>
      /// <param name="name">The name of the field.</param>
      /// <returns>An object for getting and/or setting a field.</returns>
      protected static Field<T> CreateField<T>(string name)
      {
         return new Field<T>(RealType, name);
      }

      /// <summary>
      /// Creates an object for getting and/or setting a property.
      /// </summary>
      /// <typeparam name="T">The type of the property. May be a base type or exact type.</typeparam>
      /// <param name="name">The name of the property.</param>
      /// <returns>An object for getting and/or setting a property.</returns>
      protected static Property<T> CreateProperty<T>(string name)
      {
         return new Property<T>(RealType, name);
      }

      /// <summary>
      /// Gets the value of a static property.
      /// </summary>
      /// <typeparam name="T">The type of the property. May be a base type or exact type.</typeparam>
      /// <param name="property">The property to retrieve.</param>
      /// <returns>The value of the property.</returns>
      protected static T GetStatic<T>(Property<T> property)
      {
         return property.Get(null);
      }

      /// <summary>
      /// Gets the value of a static field.
      /// </summary>
      /// <typeparam name="T">The type of the field. May be a base type or exact type.</typeparam>
      /// <param name="field">The field to retrieve.</param>
      /// <returns>The value of the field.</returns>
      protected static T GetStatic<T>(Field<T> field)
      {
         return field.Get(null);
      }

      /// <summary>
      /// Unsubscribes from a static event on the class wrapped by the proxy.
      /// </summary>
      /// <param name="eventInfo">The event information.</param>
      /// <param name="handler">The event handler.</param>
      protected static void RemoveEventStatic(EventInfo eventInfo, EventHandler handler)
      {
         var eventDelegate = Delegate.CreateDelegate(eventInfo.EventHandlerType, handler.Target, handler.Method);
         eventInfo.RemoveEventHandler(null, eventDelegate);
      }

      /// <summary>
      /// Sets the value of a static property.
      /// </summary>
      /// <typeparam name="T">The type of the property. May be a base type or exact type.</typeparam>
      /// <param name="property">The property to set.</param>
      /// <param name="value">The new value of the property.</param>
      protected static void SetStatic<T>(Property<T> property, T value)
      {
         property.Set(null, value);
      }

      /// <summary>
      /// Sets the value of a static field.
      /// </summary>
      /// <typeparam name="T">The type of the field. May be a base type or exact type.</typeparam>
      /// <param name="field">The field to set.</param>
      /// <param name="value">The new value of the field.</param>
      protected static void SetStatic<T>(Field<T> field, T value)
      {
         field.Set(null, value);
      }

      /// <summary>
      /// Subscribes to an event on the object wrapped by the proxy.
      /// </summary>
      /// <param name="eventInfo">The event information.</param>
      /// <param name="handler">The event handler.</param>
      protected void AddEvent(EventInfo eventInfo, EventHandler handler)
      {
         var eventDelegate = Delegate.CreateDelegate(eventInfo.EventHandlerType, handler.Target, handler.Method);
         eventInfo.AddEventHandler(this.RealObject, eventDelegate);
      }

      /// <summary>
      /// Gets the value of a property.
      /// </summary>
      /// <typeparam name="T">The type of the property. May be a base type or exact type.</typeparam>
      /// <param name="property">The property to retrieve.</param>
      /// <returns>The value of the property.</returns>
      protected T Get<T>(Property<T> property)
      {
         return property.Get(this.RealObject);
      }

      /// <summary>
      /// Gets the value of a field.
      /// </summary>
      /// <typeparam name="T">The type of the field. May be a base type or exact type.</typeparam>
      /// <param name="field">The field to retrieve.</param>
      /// <returns>The value of the field.</returns>
      protected T Get<T>(Field<T> field)
      {
         return field.Get(this.RealObject);
      }

      /// <summary>
      /// Unsubscribes from an event on the object wrapped by the proxy.
      /// </summary>
      /// <param name="eventInfo">The event information.</param>
      /// <param name="handler">The event handler.</param>
      protected void RemoveEvent(EventInfo eventInfo, EventHandler handler)
      {
         var eventDelegate = Delegate.CreateDelegate(eventInfo.EventHandlerType, handler.Target, handler.Method);
         eventInfo.RemoveEventHandler(this.RealObject, eventDelegate);
      }

      /// <summary>
      /// Sets the value of a property.
      /// </summary>
      /// <typeparam name="T">The type of the property. May be a base type or exact type.</typeparam>
      /// <param name="property">The property to set.</param>
      /// <param name="value">The new value of the property.</param>
      protected void Set<T>(Property<T> property, T value)
      {
         property.Set(this.RealObject, value);
      }

      /// <summary>
      /// Sets the value of a field.
      /// </summary>
      /// <typeparam name="T">The type of the field. May be a base type or exact type.</typeparam>
      /// <param name="field">The field to set.</param>
      /// <param name="value">The new value of the field.</param>
      protected void Set<T>(Field<T> field, T value)
      {
         field.Set(this.RealObject, value);
      }

      /// <summary>
      /// Gets the proxy factory.
      /// </summary>
      /// <returns>The proxy factory.</returns>
      private static Func<TProxy> GetProxyFactory()
      {
         var ctor = typeof(TProxy).GetConstructor(AllBindings, null, new Type[0], null);
         if (ctor == null)
         {
            Debug.LogErrorFormat("No parameterless constructor found for proxy type '{0}'", typeof(TProxy).FullName);
         }

         var expression = Expression.New(ctor);
         var lamda = Expression.Lambda<Func<TProxy>>(expression);

         return lamda.Compile();
      }
   }
}