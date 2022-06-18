// No se usa por el momento

using System;
using System.Collections.Generic;

namespace Clases { 

    public class Tile
    {
        List<int[]> puntos;

        public Tile(List<int[]> origen)
        {
            this.puntos = origen;
        }

        public void agregarPuntos(List<int> nuevosPuntos)
        {

        }

        public List<int[]> getPuntos()
        {
            return puntos;
        }
    }

}