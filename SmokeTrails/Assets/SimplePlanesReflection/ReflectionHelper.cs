namespace Assets.SimplePlanesReflection
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;
   using UnityEngine;

   /// <summary>
   /// A helper class used for reflecting on various SimplePlanes related code.
   /// </summary>
   public static class ReflectionHelper
   {
      /// <summary>
      /// A value indicating whether or not proxies have been initialized.
      /// </summary>
      private static bool _proxiesInitialized = false;

      /// <summary>
      /// Initializes static members of the <see cref="ReflectionHelper"/> class.
      /// </summary>
      static ReflectionHelper()
      {
         AssemblyCSharp = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(x => x.GetName().Name.ToLower() == "assembly-csharp")
            .FirstOrDefault();

         if (AssemblyCSharp == null)
         {
            Debug.LogError("Could not find the default assembly for the game");
            return;
         }

         InitializeProxies();
      }

      /// <summary>
      /// Gets the default assembly for the game.
      /// </summary>
      /// <value>
      /// The default assembly for the game.
      /// </value>
      public static Assembly AssemblyCSharp { get; private set; }

      /// <summary>
      /// Initializes the proxies.
      /// </summary>
      public static void InitializeProxies()
      {
         if (_proxiesInitialized)
         {
            return;
         }

         _proxiesInitialized = true;

         var proxyTypes = new List<Type>();

         // Find all types in the assembly that inherit from the proxy base type
         var genericBaseType = typeof(ProxyTypeBase<,>);
         var types = typeof(ReflectionHelper).Assembly.GetTypes();
         foreach (var type in types)
         {
            if (!type.IsClass || type.IsAbstract)
            {
               continue;
            }

            var current = type;
            while (current != null)
            {
               if (current.IsGenericType && current.GetGenericTypeDefinition() == genericBaseType)
               {
                  proxyTypes.Add(type);
               }

               current = current.BaseType;
            }
         }

         // Create an instance of each type to force the initialization of their static fields
         foreach (var type in proxyTypes)
         {
            ////Debug.LogFormat("Initializing Type: {0}", type.FullName);
            Activator.CreateInstance(type, true);
         }

         // Do some final checking on the proxy types to ensure they are properly caching reflection info
         CheckProxiesForStaticCaching(proxyTypes);
      }

      /// <summary>
      /// Checks the proxies in this assembly for potential performance issues due to lack of static caching of reflection info.
      /// </summary>
      /// <param name="proxyTypes">The proxy types to be checked.</param>
      private static void CheckProxiesForStaticCaching(List<Type> proxyTypes)
      {
         // The list of types that should generally be static only properties / fields
         var typesToCache = new List<Type>()
         {
            typeof(MethodInfo),
            typeof(EventInfo),
            typeof(PropertyInfo),
            typeof(FieldInfo),
            typeof(Property<>),
            typeof(Field<>),
         };

         foreach (var type in proxyTypes)
         {
            // Check for non-static properties
            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var prop in props)
            {
               var propType = prop.PropertyType.IsGenericType ? prop.PropertyType.GetGenericTypeDefinition() : prop.PropertyType;
               if (typesToCache.Contains(propType))
               {
                  Debug.LogWarningFormat(
                     "Proxy '{0}' has a non-static property '{1}' of type '{2}'. " +
                     "In most cases, you want to statically cache properties of this nature for performance reasons. " +
                     "Consider making the property static.",
                     type.FullName,
                     prop.Name,
                     prop.PropertyType);
               }
            }

            // Check for non-static fields
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var field in fields)
            {
               var fieldType = field.FieldType.IsGenericType ? field.FieldType.GetGenericTypeDefinition() : field.FieldType;
               if (typesToCache.Contains(fieldType))
               {
                  Debug.LogWarningFormat(
                     "Proxy '{0}' has a non-static field '{1}' of type '{2}'. " +
                     "In most cases, you want to statically cache fields of this nature for performance reasons. " +
                     "Consider making the field static.",
                     type.FullName,
                     field.Name,
                     field.FieldType);
               }
            }
         }
      }
   }
}