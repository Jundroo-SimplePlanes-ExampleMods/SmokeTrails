namespace Assets.SimplePlanesReflection
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;

   /// <summary>
   /// A simple lookup utility for getting and caching the real type object of a simpleplanes type.
   /// </summary>
   /// <typeparam name="T">The proxy type for which to do the real type lookup.</typeparam>
   public static class RealTypes<T>
   {
      /// <summary>
      /// The real type represented by this class.
      /// </summary>
      private static Type _realType;

      /// <summary>
      /// Gets the real type represented by this class.
      /// </summary>
      /// <value>
      /// The real type represented by this class.
      /// </value>
      public static Type RealType
      {
         get
         {
            if (_realType == null)
            {
               var typeFullName = typeof(T).FullName.Substring(30);
               _realType = ReflectionHelper.AssemblyCSharp.GetType(typeFullName, true);
            }

            return _realType;
         }
      }
   }
}