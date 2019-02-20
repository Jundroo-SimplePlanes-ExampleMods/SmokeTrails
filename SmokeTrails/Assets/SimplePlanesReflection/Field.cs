namespace Assets.SimplePlanesReflection
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Linq.Expressions;
   using System.Reflection;
   using System.Reflection.Emit;
   using System.Text;
   using UnityEngine;

   /// <summary>
   /// A class used to create a fast get and or set function for a field on a reflected type.
   /// </summary>
   /// <typeparam name="T">The type of the field. May be a base class type or the actual type.</typeparam>
   public class Field<T>
   {
      /// <summary>
      /// The function used to retrieve the value of the field.
      /// </summary>
      public readonly Func<object, T> Get;

      /// <summary>
      /// The function used to set the value of the field.
      /// </summary>
      public readonly Action<object, T> Set;

      /// <summary>
      /// Initializes a new instance of the <see cref="Field{T}"/> class.
      /// </summary>
      /// <param name="ownerType">The type owning the field.</param>
      /// <param name="fieldName">The name of the field.</param>
      public Field(Type ownerType, string fieldName)
      {
         ////Debug.LogFormat("Initializing Field: {0}.{1}  -  {2}", ownerType.FullName, fieldName, typeof(T).FullName);

         // First grab the field via reflection
         var field = ownerType.GetField(fieldName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
         if (field == null)
         {
            var errorMessage = string.Format("Could not find field via reflection: {0}.{1}", ownerType.FullName, fieldName);

            Debug.LogError(errorMessage);
            this.Get = x => { throw new InvalidOperationException(errorMessage); };
            this.Set = (x, y) => { throw new InvalidOperationException(errorMessage); };

            return;
         }

         // Make sure the expected type is compatible with the actual type
         if (!typeof(T).IsAssignableFrom(field.FieldType))
         {
            var errorMessage = string.Format(
               "The field '{0}.{1}' of type '{2}' is not assignable to the expected type of '{3}'",
               ownerType.FullName,
               fieldName, 
               field.FieldType.FullName,
               typeof(T).FullName);

            Debug.LogError(errorMessage);
            this.Get = x => { throw new InvalidOperationException(errorMessage); };
            this.Set = (x, y) => { throw new InvalidOperationException(errorMessage); };

            return;
         }

         // Build a lambda statement for retrieving the field value
         var instanceExpression = Expression.Parameter(typeof(object), "instance");
         var typedInstanceExpression = Expression.TypeAs(instanceExpression, ownerType);

         var getFieldExpression = Expression.Field(field.IsStatic ? null : typedInstanceExpression, field);
         var getLambda = Expression.Lambda<Func<object, T>>(getFieldExpression, instanceExpression);

         // Compile the lambda statement for our get function
         this.Get = getLambda.Compile();

         if (!field.IsInitOnly)
         {
            // Build a lambda statement for setting the field value
            var valueExpression = Expression.Parameter(typeof(T), "value");
            var typedValueExpression = typeof(T) != field.FieldType ? Expression.TypeAs(valueExpression, field.FieldType) : (Expression)valueExpression;

            BinaryExpression assignExpression = Expression.Assign(getFieldExpression, typedValueExpression);

            var setLambda = Expression.Lambda<Action<object, T>>(assignExpression, instanceExpression, valueExpression);

            // Compile the lambda statement for our set function
            this.Set = setLambda.Compile();
         }
         else
         {
            this.Set = (x, y) => { throw new InvalidOperationException("Field is read only."); };
         }
      }
   }
}