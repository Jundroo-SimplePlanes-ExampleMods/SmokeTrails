namespace Assets.SimplePlanesReflection.Assets.Game.AircraftIo
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;

   public partial class Aircraft : ProxyType<Aircraft>
   {
      private static EventInfo _aircraftGenerated = GetEvent("AircraftGenerated");

      private static Property<string> _name = CreateProperty<string>("Name");

      protected Aircraft()
      {
      }

      public static event EventHandler AircraftGenerated
      {
         add
         {
            AddEventStatic(_aircraftGenerated, value);
         }

         remove
         {
            RemoveEventStatic(_aircraftGenerated, value);
         }
      }

      public string Name
      {
         get
         {
            return this.Get(_name);
         }

         set
         {
            this.Set(_name, value);
         }
      }
   }
}