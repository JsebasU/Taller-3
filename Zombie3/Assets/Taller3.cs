using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ald = NPC.Ally;
using zom = NPC.Enemy;
using UnityEngine.UI;
using TMPro;


public class Taller3 : MonoBehaviour
{

    public TextMeshProUGUI zombies;
    public TextMeshProUGUI aldeanos;
    int aldeT;
    int zomT;
    GameObject[] infect, town;
   

    /// <summary>
    /// usamos un valor random para determinar cuantos GameObjects habran.
    /// de manera aleatorea se le otorgan componentes a estos GameObjects
    /// </summary>

    void Start()
    {
        Creator creator = new Creator(Random.Range(5, 15));
        infect = GameObject.FindGameObjectsWithTag("Zombie");
        town = GameObject.FindGameObjectsWithTag("Villager");
    }
    private void Update()
    {
        foreach (GameObject item in infect)
        {
            zomT = infect.Length;
            zombies.text = "Zombies Totales: " + zomT.ToString();
        }
        foreach (GameObject item in town)
        {
           aldeT =  town.Length;
           aldeanos.text = "Aldeanos Totales: " + aldeT.ToString();
        }
    }
}

public class Creator
{
    string Clor;
    const int MAX = 26;
    public readonly float probability;
    public Creator(float prob)
    {
        probability = prob;


        GameObject hero = GameObject.CreatePrimitive(PrimitiveType.Cube);
        hero.AddComponent<Hero>().Equip();
        int total =(int) Random.Range(probability, MAX);
        for (int i = 0; i < total ; i++)
        {

            int Crnd = Random.Range(1, 4);
            if (Crnd == 1)
            {
                Clor = "Cyan";
            }
            if (Crnd == 2)
            {
                Clor = "Magenta";
            }
            if (Crnd == 3)
            {
                Clor = "Verde";
            }
            GameObject zCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            zCube.AddComponent<Zom.Zombie>().Infectado(Clor);
            zCube.tag = "Zombie";
          
        }
        int aTotal = MAX - total;  
        for(int i = 0; i < aTotal; i++)
        {
            GameObject aCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            aCube.AddComponent<ald.Aldeano>().ID();
            aCube.tag = "Villager";
        }

    }
}
public class Hero : MonoBehaviour
{
    /// <summary>
    /// se realiza una referencia para los scripts de movimiento y mirar del Heroe (jugador)
    /// Agregamos los componentes necesarios para realizar collisiones y se desactiva el uso de graveda 
    /// tambien se activan todas los constrains para hacer que solo se pueda mover por el codigo y no por alguna colision
    /// </summary>
    Movement movement;
    Look look;


    public void Equip()
    {

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        gameObject.name = "Hero";
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        // se le agregan los scripts de movimiento y mirar al jugador 
        movement = gameObject.AddComponent<Movement>();
        look = gameObject.AddComponent<Look>();
        gameObject.AddComponent<Camera>();
        // determina una velocidad al azar para entregarcela al metodo de movimiento mas adelante

    }
    /// <summary>
    /// llama a los constructores en las clases de Movement y Look
    /// </summary>
    private void Update()
    {
        movement.Move();
        look.Arround();
    }
    /// <summary>
    /// se toman los valores almacendos en el estruc para imprimirlos cada que el heroe colisione con 
    /// el respectivo GameObject
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Zom.Zombie>())
        {
            InfoZomb info = collision.gameObject.GetComponent<Zom.Zombie>().GetInfo();
            Debug.Log("Waaar quiero comer " + info.gusto);
        }
        if (collision.gameObject.GetComponent<ald.Aldeano>())
        {
            InfoAlde info = collision.gameObject.GetComponent<ald.Aldeano>().GetInfo();
            Debug.Log("soy " + info.name + " y tengo " + info.edad);
        }
    }
}
