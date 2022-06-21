using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Clases
{
    // Clase Trazo, que representa el recorrido que ha tenido un Jugador hasta un punto
    public class Trazo
    {
        //// ATRIBUTOS
        // Camino que ha recorrido el Jugador, en una cadena con los inputs
        string path;

        // Coordenadas del punto inicial del Trazo
        int inicioX;
        int inicioY;

        // Tama�o del sprite del trazo (es tanto largo como ancho)
        int tamanio;

        //// M�TODOS
        // Constructor
        public Trazo(int tam, int x, int y)
        {
            this.path = "X";
            this.tamanio = tam;
            this.inicioX = x;
            this.inicioY = y;
        }

        // M�todos GET para obtener los atributos de la instancia
        public int getX() { return this.inicioX; }
        public int getY() { return this.inicioY; }
        public int getTam() { return this.tamanio; }
        public string getPath() { return this.path; }

        // M�todos SET para asignar valores a los atributos de la instancia
        public void setXInicial(int xNueva) { this.inicioX = xNueva; }
        public void setYInicial(int yNueva) { this.inicioY = yNueva; }
        public void setPath(string nuevo) { this.path = nuevo; }

        // M�todos ESPEC�FICOS de la instancia

        // Regresa un paso hacia atr�s, borrando �l �ltimo movimiento registrado en el camino
        public char backtrack()
        {
            int length = this.path.Length - 1;
            char res = this.path[length];
            if (res != 'X') this.path = this.path.Remove(length); // El caracter X representa el inicio del camino.
            return (res);
        }

        // Agrega una nueva direcci�n a la cadena del camino.
        public void nuevaDireccion(string dir)
        {
            this.path += dir;
            if (this.path.Length > 1) this.borrarRedundantes();
        }

        // Elimina los inputs que se cancelen entre s� de la cadena con prop�sitos de acortarla y poder regresar borrando el exceso.
        public void borrarRedundantes()
        {
            string cadena1 = this.path;
            // Regex de los conjuntos de caracteres redundantes
            string patronRegex1 = "WS|SW";
            string patronRegex2 = "AD|DA";
            cadena1 = Regex.Replace(cadena1, patronRegex1, "");
            string cadenaSinRedundancia = Regex.Replace(cadena1, patronRegex2, "");
            this.path = cadenaSinRedundancia;
        }

        public void dibujarTrazo(SpriteBatch _spriteBatch, Texture2D pixel) {
            int x = this.inicioX;
            int y = this.inicioY;
            bool dibujar;
            for(int i = 0; i < path.Length; i++ ) {
                dibujar = true;
                switch(path[i]) {
                    case 'X':
                        dibujar = false;
                        break;
                    case 'W':
                        y -= this.tamanio;
                        break;
                    case 'A':
                        x -= this.tamanio;
                        break;
                    case 'S':
                        y += this.tamanio;
                        break;
                    case 'D':
                        x += this.tamanio;
                        break;
                }
                if(dibujar) _spriteBatch.Draw(pixel, new Rectangle(x, y, this.tamanio, this.tamanio), Color.Red);
            }
        }


    }


}