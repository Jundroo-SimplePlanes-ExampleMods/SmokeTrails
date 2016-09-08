namespace Assets.SimplePlanesReflection.Assets.Scripts.Explosions
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;

   public partial class ExplosiveForceScript : MonoBehaviourProxyType<ExplosiveForceScript>
   {
      private static Field<float> _blastForce = CreateField<float>("BlastForce");

      private static Field<float> _blastRadius = CreateField<float>("BlastRadius");

      private static Field<float> _criticalBlastRadius = CreateField<float>("CriticalBlastRadius");

      protected ExplosiveForceScript()
      {
      }

      public float BlastForce
      {
         get
         {
            return this.Get(_blastForce);
         }

         set
         {
            this.Set(_blastForce, value);
         }
      }

      public float BlastRadius
      {
         get
         {
            return this.Get(_blastRadius);
         }

         set
         {
            this.Set(_blastRadius, value);
         }
      }

      public float CriticalBlastRadius
      {
         get
         {
            return this.Get(_criticalBlastRadius);
         }

         set
         {
            this.Set(_criticalBlastRadius, value);
         }
      }
   }
}