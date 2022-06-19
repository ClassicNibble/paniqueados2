namespace Clases { 

    // Clase Jugador, representa el "cursor" de cada jugador en la partida

    public class Jugador
    {
        //// ATRIBUTOS
            // Trazo del jugador que contiene los movimientos grabados
            Trazo trazo; 

            // Posiciones del jugador en ambos ejes, tanto iniciales como actuales
            int iniX;
            int posX;
            int iniY;
            int posY;

            // Tamaño del cursor del jugador (es tanto largo como ancho)
            int tam;

        //// MÉTODOS
            // Constructor
            public Jugador(int x, int y, int tam) {
                // Se inicializan atributos de la instancia
                this.trazo = new Trazo(tam);
                this.iniX = x;
                this.iniY = y;
                this.posX = x;
                this.posY = y;
                this.tam = tam;
            }

            // Metodos GET para obtener los atributos de la instancia
            public int getX() { return this.posX; }
            public int getY() { return this.posY; }
            public Trazo getTrazo() { return this.trazo; }
            public int getTam() { return this.tam; }

            // Metodos SET para asignar valores a los atributos de la instancia
            public void setX(int x) { this.posX = x; }
            public void setY(int y) { this.posY = y; }
            public void setTrazo(Trazo t) { this.trazo = t; }
            public void setTam(int t) { this.tam = t; }

            // Métodos ESPECÍFICOS de la instancia
                // Reiniciar la posicion del Jugador a la inicial
                public void reiniciarPos() { this.posX = this.iniX; this.posY = this.iniY; }
    }
}