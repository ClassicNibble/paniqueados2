using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace Clases { 
    // Clase Trazo, que representa el recorrido que ha tenido un Jugador hasta un punto
    public class Trazo
    {
        //// ATRIBUTOS
            // Camino que ha recorrido el Jugador, en una cadena con los inputs
            string path;

            // Coordenadas del punto inicial del Trazo
            int inicioX;
            int inicioY;

            // Tamaño del sprite del trazo (es tanto largo como ancho)
            int tamanio;

        //// MÉTODOS
            // Constructor
            public Trazo(int tam)
            {
                this.path = "X";
                this.tamanio = tam;
            }
            
            // Métodos GET para obtener los atributos de la instancia
            public int getX() { return this.inicioX; }
            public int getY() { return this.inicioY; }
            public int getTam() { return this.tamanio; }
            public string getPath() { return this.path; }
            
            // Métodos SET para asignar valores a los atributos de la instancia
            public void setXInicial(int xNueva) { this.inicioX = xNueva; }
            public void setYInicial(int yNueva) { this.inicioY = yNueva; }
            public void setPath(string nuevo) { this.path = nuevo; }

            // Métodos ESPECÍFICOS de la instancia

                    // Regresa un paso hacia atrás, borrando él último movimiento registrado en el camino
                    public char backtrack() {
                        int length = this.path.Length - 1;
                        char res = this.path[length];
                        if (res != 'X') this.path = this.path.Remove(length); // El caracter X representa el inicio del camino.
                        return (res);
                    }
            
                    // Agrega una nueva dirección a la cadena del camino.
                    public void nuevaDireccion(string dir) {
                        this.path += dir;
                        if (this.path.Length > 1) this.borrarRedundantes();
                    }
            
                    // Elimina los inputs que se cancelen entre sí de la cadena con propósitos de acortarla y poder regresar borrando el exceso.
                    public void borrarRedundantes() {
                        string cadena1 = this.path;
                        // Regex de los conjuntos de caracteres redundantes
                        string patronRegex1 = "WS|SW";
                        string patronRegex2 = "AD|DA";
                        cadena1 = Regex.Replace(cadena1, patronRegex1, "");
                        string cadenaSinRedundancia = Regex.Replace(cadena1, patronRegex2, "");
                        this.path = cadenaSinRedundancia;
                    }

    }


}