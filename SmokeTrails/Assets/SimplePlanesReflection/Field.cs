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
            // Setting the field is a little different...
            // Unfortunately, we don't have access to Expression.Assign(), so setting up a lambda statement for this is a no-go.
            // We therefore fall back to creating a dynamic method via Reflection.Emit.
            var setFieldMethod = new DynamicMethod("SetField", typeof(void), new Type[] { typeof(object), typeof(T) }, true);
            var g = setFieldMethod.GetILGenerator();

            // If this is not a static field, load the instance argument and cast it to the appropriate type (from object)
            if (!field.IsStatic)
            {
               g.Emit(OpCodes.Ldarg_0);
               g.Emit(OpCodes.Castclass, ownerType);
            }

            // Load the value argument and if not the exact type, cast it to the exact type
            g.Emit(OpCodes.Ldarg_1);
            if (field.FieldType != typeof(T))
            {
               g.Emit(OpCodes.Castclass, field.FieldType);
            }

            // Set the field and return
            g.Emit(field.IsStatic ? OpCodes.Stsfld : OpCodes.Stfld, field);
            g.Emit(OpCodes.Ret);

            // Create the delegate for our set function
            this.Set = (Action<object, T>)setFieldMethod.CreateDelegate(typeof(Action<object, T>));
         }
         else
         {
            this.Set = (x, y) => { throw new InvalidOperationException("Field is read only."); };
         }
      }
   }
}