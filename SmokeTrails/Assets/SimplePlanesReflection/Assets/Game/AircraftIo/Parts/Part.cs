namespace Assets.SimplePlanesReflection.Assets.Game.AircraftIo.Parts
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;

   public partial class Part : ProxyType<Part>
   {
      private static Property<int> _id = CreateProperty<int>("Id");

      private static Property<List<int>> _materialIds = CreateProperty<List<int>>("MaterialIds");

      protected Part()
      {
      }

      public int Id
      {
         get
         {
            return this.Get(_id);
         }
      }

      public List<int> MaterialIds
      {
         get
         {
            return this.Get(_materialIds);
         }
      }
   }
}