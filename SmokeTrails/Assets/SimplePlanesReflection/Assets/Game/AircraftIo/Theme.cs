namespace Assets.SimplePlanesReflection.Assets.Game.AircraftIo
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;

   public partial class Theme : ProxyType<Theme>
   {
      private static Property<object> _materials = CreateProperty<object>("Materials");

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
   }
}