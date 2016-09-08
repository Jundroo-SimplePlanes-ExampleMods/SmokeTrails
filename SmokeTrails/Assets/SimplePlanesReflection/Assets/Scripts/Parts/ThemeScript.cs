namespace Assets.SimplePlanesReflection.Assets.Scripts.Parts
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using Game.AircraftIo;

   public partial class ThemeScript : ProxyType<ThemeScript>
   {
      private static Property<object> _theme = CreateProperty<object>("Theme");

      protected ThemeScript()
      {
      }

      public Theme Theme
      {
         get
         {
            return Theme.Wrap(this.Get(_theme));
         }
      }
   }
}