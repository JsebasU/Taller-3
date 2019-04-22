using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    

    
    namespace Enemy
    {

        public class Zombie : MonoBehaviour
        {
            /// <summary>
            ///declaramos 2 enumeradores uno para el estado del zombie y otro para la comida preferida
            /// </summary>

            float t;

            public enum Gustos
            {
                Cerebelo,
                Muslo,
                Riñon,
                Brazo,
                CostilitasALaBQ
            }

            public enum Estado
            {
                idel,
                moving,
                rotating
            }

            Gustos comida;
            Estado estado;
            /// <summary>
            /// le otorgamos un rigidboy como en las otras dos clases para efectuar la colisiones
            /// se determina de que color sera el zombie de manera aleatoria en generador
            /// y se posicionea en un lugar random del mundo entre 10 y menos 10 de los ejes "z" y "x"
            /// </summary>
            public void Infectado(string olor)
            {
                Rigidbody rb;
                comida = (Gustos)Random.Range(0, 5);
                rb = this.gameObject.AddComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeAll;
                rb.useGravity = false;
                if (olor == "Verde")
                {

                    Renderer color = this.gameObject.GetComponent<Renderer>();
                    color.material.color = Color.green;
                    this.gameObject.name = "Zombie";
                }
                if (olor == "Cyan")
                {

                    Renderer color = this.gameObject.GetComponent<Renderer>();
                    color.material.color = Color.cyan;
                    this.gameObject.name = "Zombie";

                }
                if (olor == "Magenta")
                {

                    Renderer color = this.gameObject.GetComponent<Renderer>();
                    color.material.color = Color.magenta;
                    this.gameObject.name = "Zombie";

                }
                float x = Random.Range(-10, 10);
                float z = Random.Range(-10, 10);
                this.gameObject.transform.position = new Vector3(x, 0, z);
            }
            /// <summary>
            /// almacenamos en el struct del zombie Sus gustos 
            /// </summary>
            public InfoZomb GetInfo()
            {
                InfoZomb infoZomb = new InfoZomb();
                infoZomb.gusto = comida.ToString();
                return infoZomb;
            }
            /// <summary>
            /// determinar el tiempo entre cada estado del zombie 
            /// y acceder al siguiente estado de manera aleatorea cada 3 segundo
            /// </summary>
            private void Update()
            {
                t += Time.deltaTime;
                if (t >= 3)
                {
                    estado = (Estado)Random.Range(0, 3);

                    t = 0;
                }
                switch (estado)
                {
                    case Estado.idel:
                        break;
                    case Estado.moving:

                        this.gameObject.transform.Translate(transform.forward * 0.02f);
                        break;
                    case Estado.rotating:
                        this.gameObject.transform.Rotate(0, Random.Range(1f, 15f), 0, 0);
                        //this.gameObject.transform.rotation = new Quaternion(0, Random.Range(1, 361), 0,0);
                        break;
                    default:
                        break;
                }
             
            }
        }
    }
}


