namespace Assets.SimplePlanesReflection.Assets.Scripts.Parts.Targeting
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;

   public partial class GroundTarget : ProxyType<GroundTarget>
   {
      private static Property<float> _maxVisibilityRange = CreateProperty<float>("MaxVisibleRange");

      protected GroundTarget()
      {
      }

      public float MaxVisibleRange
      {
         get
         {
            return this.Get(_maxVisibilityRange);
         }

         set
         {
            this.Set(_maxVisibilityRange, value);
         }
      }
   }
}