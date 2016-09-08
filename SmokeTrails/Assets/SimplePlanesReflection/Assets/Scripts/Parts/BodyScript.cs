namespace Assets.SimplePlanesReflection.Assets.Scripts.Parts
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;

   public partial class BodyScript : MonoBehaviourProxyType<BodyScript>
   {
      private static EventInfo _newBodyCreated = GetEvent("NewBodyCreated");

      private static EventInfo _partDisconnectExplosion = GetEvent("PartDisconnectExplosion");

      protected BodyScript()
      {
      }

      public event EventHandler NewBodyCreated
      {
         add
         {
            this.AddEvent(_newBodyCreated, value);
         }

         remove
         {
            this.RemoveEvent(_newBodyCreated, value);
         }
      }

      public event EventHandler PartDisconnectExplosion
      {
         add
         {
            this.AddEvent(_partDisconnectExplosion, value);
         }

         remove
         {
            this.RemoveEvent(_partDisconnectExplosion, value);
         }
      }
   }
}