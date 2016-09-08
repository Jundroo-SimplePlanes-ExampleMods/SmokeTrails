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
   /// A class used to create a fast get and or set function for a property on a reflected type.
   /// </summary>
   /// <typeparam name="T">The type of the property. May be a base class type or the actual type.</typeparam>
   public class Property<T>
   {
      /// <summary>
      /// The function used to retrieve the value of the property.
      /// </summary>
      public readonly Func<object, T> Get;

      /// <summary>
      /// The function used to set the value of the property.
      /// </summary>
      public readonly Action<object, T> Set;

      /// <summary>
      /// Initializes a new instance of the <see cref="Property{T}"/> class.
      /// </summary>
      /// <param name="ownerType">The type owning the property.</param>
      /// <param name="propertyName">The name of the property.</param>
      public Property(Type ownerType, string propertyName)
      {
         ////Debug.LogFormat("Initializing Property: {0}.{1}  -  {2}", ownerType.FullName, propertyName, typeof(T).FullName);

         // First grab the property via reflection
         var prop = ownerType.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
         if (prop == null)
         {
            var errorMessage = string.Format("Could not find property via reflection: {0}.{1}", ownerType.FullName, propertyName);

            Debug.LogError(errorMessage);
            this.Get = x => { throw new InvalidOperationException(errorMessage); };
            this.Set = (x, y) => { throw new InvalidOperationException(errorMessage); };

            return;
         }

         // Make sure the expected type is compatible with the actual type
         if (!typeof(T).IsAssignableFrom(prop.PropertyType))
         {
            var errorMessage = string.Format(
               "The property '{0}.{1}' of type '{2}' is not assignable to the expected type of '{3}'", 
               ownerType.FullName, 
               propertyName, 
               prop.PropertyType.FullName, 
               typeof(T).FullName);

            Debug.LogError(errorMessage);
            this.Get = x => { throw new InvalidOperationException(errorMessage); };
            this.Set = (x, y) => { throw new InvalidOperationException(errorMessage); };

            return;
         }

         var instanceExpression = Expression.Parameter(typeof(object), "instance");
         var typedInstanceExpression = Expression.TypeAs(instanceExpression, ownerType);

         // If the property has a getter, build a lambda statement for retrieving the property value, then compile it
         var propGetter = prop.GetGetMethod(true);
         if (propGetter != null)
         {
            var getPropertyExpression = Expression.Property(propGetter.IsStatic ? null : typedInstanceExpression, prop);
            var getLambda = Expression.Lambda<Func<object, T>>(getPropertyExpression, instanceExpression);

            this.Get = getLambda.Compile();
         }
         else
         {
            this.Get = x => { throw new InvalidOperationException("Property is not readable."); };
         }

         // If the property has a setter, build a lambda statement for setting the property value, then compile it
         var propSetter = prop.GetSetMethod(true);
         if (propSetter != null)
         {
            var valueExpression = Expression.Parameter(typeof(T), "value");

            var typedValueExpression = (Expression)valueExpression;
            if (typeof(T) != prop.PropertyType)
            {
               typedValueExpression = Expression.TypeAs(valueExpression, prop.PropertyType);
            }

            var setPropertyExpression = Expression.Call(propSetter.IsStatic ? null : typedInstanceExpression, propSetter, typedValueExpression);
            var setLambda = Expression.Lambda<Action<object, T>>(setPropertyExpression, instanceExpression, valueExpression);


            this.Set = setLambda.Compile();
         }
         else
         {
            this.Set = (x, y) => { throw new InvalidOperationException("Property is read only."); };
         }
      }
   }
}