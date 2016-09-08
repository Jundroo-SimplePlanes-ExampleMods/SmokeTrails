namespace Assets.SimplePlanesReflection.Assets.Scripts.Parts
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using Game.AircraftIo;

   public partial class AircraftScript : MonoBehaviourProxyType<AircraftScript>
   {
      private static Property<object> _aircraft = CreateProperty<object>("Aircraft");

      private static Property<object> _theme = CreateProperty<object>("Theme");

      protected AircraftScript()
      {
      }

      public Aircraft Aircraft
      {
         get
         {
            return Aircraft.Wrap(this.Get(_aircraft));
         }
      }

      public ThemeScript Theme
      {
         get
         {
            return ThemeScript.Wrap(this.Get(_theme));
         }
      }
   }
}