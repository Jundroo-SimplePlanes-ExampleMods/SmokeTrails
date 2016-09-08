namespace Assets.SimplePlanesReflection.Assets.Scripts.Parts.Events
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using UnityEngine;

   public partial class BodyCreatedEventArgs : GenericProxyType<BodyCreatedEventArgs, EventArgs>
   {
      private static Property<MonoBehaviour> _newBodyScript = CreateProperty<MonoBehaviour>("NewBodyScript");

      private static Property<MonoBehaviour> _sourceBodyScript = CreateProperty<MonoBehaviour>("SourceBodyScript");

      protected BodyCreatedEventArgs()
      {
      }

      public BodyScript NewBodyScript
      {
         get
         {
            return BodyScript.Wrap(this.Get(_newBodyScript));
         }
      }

      public BodyScript SourceBodyScript
      {
         get
         {
            return BodyScript.Wrap(this.Get(_sourceBodyScript));
         }
      }
   }
}