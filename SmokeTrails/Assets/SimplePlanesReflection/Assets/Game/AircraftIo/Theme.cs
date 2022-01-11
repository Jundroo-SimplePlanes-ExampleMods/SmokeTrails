namespace Assets.SimplePlanesReflection.Assets.Game.AircraftIo
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;

   public partial class Theme : ProxyType<Theme>
   {
      private static Property<object> _materials = CreateProperty<object>("Materials");

      private static MethodInfo _getMaterial = GetMethod("GetMaterial", new Type[] { typeof(int) });

      protected Theme()
      {
      }

      public List<PartMaterial> Materials
      {
         get
         {
            var list = new List<PartMaterial>();
            foreach (var partMaterial in (IEnumerable)this.Get(_materials))
            {
               list.Add(PartMaterial.Wrap(partMaterial));
            }

            return list;
         }
      }

      public PartMaterial GetMaterial(int materialId)
      {
         return PartMaterial.Wrap(_getMaterial.Invoke(this.RealObject, new object[] { materialId }));
      }
   }
}