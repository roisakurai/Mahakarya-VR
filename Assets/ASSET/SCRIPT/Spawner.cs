using System.Linq;
using HurricaneVR.Framework.Core.Player;
using UnityEngine;

namespace HurricaneVR.TechDemo.Scripts
{
    public class Spawner : MonoBehaviour
    {
        public GameObject Barrier1;
        public GameObject Barrier2;
        public GameObject Barrier3;
        public GameObject Barrier4;
        public GameObject Barrier5;

        public Transform Spawner0;
        public Transform Spawner1;
        public Transform Spawner2;
        public Transform Spawner3;
        public Transform Spawner4;
        public Transform Spawner5;
        public HVRTeleporter Teleporter { get; set; }

        public void Start() 
        {
            Teleporter = GameObject.FindObjectOfType<HVRTeleporter>();
        }

        public void Teleport()
        {
            if (!Barrier5 || !Barrier5.gameObject.activeInHierarchy) 
            {
                if (Teleporter && Spawner5) 
                {
                    Teleporter.Teleport(Spawner5.position, Spawner5.forward);
                    //end the program
                    return;
                }
            }
            if (!Barrier4 || !Barrier4.gameObject.activeInHierarchy) 
            {
                if (Teleporter && Spawner4) 
                {
                    Teleporter.Teleport(Spawner4.position, Spawner4.forward);
                    //end the program
                    return;
                }
            }
            if (!Barrier3 || !Barrier3.gameObject.activeInHierarchy) 
            {
                if (Teleporter && Spawner3) 
                {
                    Teleporter.Teleport(Spawner3.position, Spawner3.forward);
                    //end the program
                    return;
                }
            }
            if (!Barrier2 || !Barrier2.gameObject.activeInHierarchy) 
            {
                if (Teleporter && Spawner2) 
                {
                    Teleporter.Teleport(Spawner2.position, Spawner2.forward);
                    //end the program
                    return;
                }
            }
            if (!Barrier1 || !Barrier1.gameObject.activeInHierarchy) 
            {
                if (Teleporter && Spawner1) 
                {
                    Teleporter.Teleport(Spawner1.position, Spawner1.forward);
                    //end the program
                    return;
                }
            }
            
            // Default case if all barriers are active
            if (Teleporter && Spawner0)
            {
                Teleporter.Teleport(Spawner0.position, Spawner0.forward);
            }
        }
    }
}
