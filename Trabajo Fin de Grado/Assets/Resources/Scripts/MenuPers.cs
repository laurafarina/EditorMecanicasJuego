﻿using UnityEngine;
using System.Collections;
using ListaC;
using UnityEngine.UI;
using Elementos;

public class MenuPers : MonoBehaviour {

    public GameObject reglasManager = null;
    public Transform menuPanel;
    public GameObject buttonPrefab;
    public Personaje persActual = null;
    public GameObject clave = null;
    public GameObject valor = null;
    public GameObject panel = null;
    public GameObject panelEscribir = null;
    public bool esRegla = false;
    public bool esAntecedente = false;

	// Use this for initialization
	void Start () {

        actualizar();
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void MostrarOcultar(GameObject panel)
    {
        if (panel.activeSelf == true)
        {
            panel.SetActive(false);
            panel.transform.localPosition = new Vector3(panel.transform.localPosition.x, -20, panel.transform.localPosition.z);
        }
        else panel.SetActive(true);
    }

    public void add()
    {
        if (persActual != null && clave.GetComponent<InputField>().text != "" && valor.GetComponent<InputField>().text != "")
        {
            persActual.addPropiedad(clave.GetComponent<InputField>().text, valor.GetComponent<InputField>().text);
            panelEscribir.GetComponentInChildren<Text>().text = UIController.Lista.PropiedadesToString(persActual);
        }
    }

    public void setPersAct(Personaje pa)
    {
        MostrarOcultar(panel);
        persActual = pa;
        if (!esRegla) panelEscribir.GetComponentInChildren<Text>().text = UIController.Lista.PropiedadesToString(persActual);
        else if (esRegla)
        {
            if (esAntecedente)
            {
                reglasManager.GetComponent<Reglas>().agregarAntec(pa);
            }
            else
            {
                reglasManager.GetComponent<Reglas>().agregarConsec(pa);
            }
        }
    }

    public void cerrar()
    {
        this.transform.position = new Vector3(1000, this.transform.position.y, 0);
    }

    public void abrir()
    {
        this.transform.position = new Vector3(0, this.transform.position.y, 0);
        actualizar();
    }

    public void actualizar()
    {
        foreach (Transform child in panel.transform) Destroy(child.gameObject);
        for (int i = 0; i < UIController.Lista.getNumPersonajes(); i++)
        {
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.GetComponentInChildren<Text>().text = UIController.Lista.getPersonaje(i).nombre;
            int index = i;
            button.GetComponent<Button>().onClick.AddListener(
                () => { setPersAct(UIController.Lista.getPersonajeByName(button.GetComponentInChildren<Text>().text)); }
            );
            button.transform.parent = menuPanel;
        }
    }
}
