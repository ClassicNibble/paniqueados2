using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace Clases { 

    public class Trazo
    {
        string path;
        int inicioX;
        int inicioY;

        public Trazo()
        {
            this.path = "X";
        }

        public int getX()
        {
            return this.inicioX;
        }

        public int getY()
        {
            return this.inicioY;
        }

        public char backtrack()
        {
            int length = this.path.Length - 1;
            char res = this.path[length];
            if (res != 'X') { this.path = this.path.Remove(length); }
            return (res);
        }

        public void nuevaDireccion(string dir)
        {
            this.path += dir;
            if (this.path.Length > 1)
            {
                this.borrarRedundantes();
            }
        }

        public void borrarRedundantes()
        {
            string cadena1 = this.path;
            string patronRegex1 = "WS|SW";
            string patronRegex2 = "AD|DA";
            string vacio = "";
            cadena1 = Regex.Replace(cadena1, patronRegex1, vacio);
            string cadenaSinRedundancia = Regex.Replace(cadena1, patronRegex2, vacio);
            this.path = cadenaSinRedundancia;
        }

        public string getPath()
        {
            return this.path;
        }

        public Tile terminarTrazo(Tile tileModificado, List<int> listaNueva)
        {
            tileModificado.agregarPuntos(listaNueva);
            return tileModificado;
        }

        public void setXInicial(int xNueva)
        {
            this.inicioX = xNueva;
        }

        public void setYInicial(int yNueva)
        {
            this.inicioY = yNueva;
        }
    }

}