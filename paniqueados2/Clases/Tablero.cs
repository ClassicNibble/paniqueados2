using System;
using System.Collections.Generic;

namespace Clases { 
    // Clase Tablero, que representa un nivel en el que juega el Jugador
    public class Tablero {
        //// ATRIBUTOS
            // Tamaño de los tiles a utilizar
            int tam;
            string estado = "";

            // Los limites del tablero, en pixeles
            int limX;
            int limY;
            
            // Tiles que han sido revelados en el tablero
            int areaRevelada;
            
            // Los limites del tablero, en tiles
            int cantX;
            int cantY;

            // Matriz que representa el estado de cada tile con un char
            List<char[]> matriz = new List<char[]>();
            
            // Lista de jugadores jugando en el tablero
            // Multiplayer aun no implementado pero esto lo hará más sencillo
            List<Jugador> listaJugadores = new List<Jugador>();

        //// MÉTODOS
            // Constructor
            public Tablero(int tam, int limX, int limY, int areaRevelada) {
                this.areaRevelada = areaRevelada;
                this.tam = tam;
                this.limX = limX;
                this.limY = limY;
                this.cantX = limX / tam;
                this.cantY = limY / tam;

                char[] linea = new char[cantX];

                bool jColocado = false;
                while(!jColocado) { 
                try {
                int anchoInicial = -1;
                int altoInicial = -1;
                Random r = new Random();

                while (anchoInicial % 2 != 0)
                { anchoInicial = r.Next(2, (areaRevelada / 2));
                    altoInicial = areaRevelada / anchoInicial;
                    if (altoInicial > this.cantY || anchoInicial > this.cantX) anchoInicial = -1;
                }

                int posicionInicialX = r.Next(0, this.cantX - anchoInicial);
                int posicionInicialY = r.Next(0, this.cantY - altoInicial);

                for (int i = 0; i < cantY; i++)
                {
                    for (int j = 0; j < cantX; j++)
                    {
                        if (((j >= posicionInicialX) && (i >= posicionInicialY)) && ((j <= posicionInicialX + anchoInicial) && (i <= posicionInicialY + altoInicial))) linea[j] = 'C';
                        else linea[j] = 'X';
                    }
                    this.matriz.Add(linea);
                    linea = new char[cantX];
                }

                this.agregarJugador();
                jColocado = true;
                } catch(Exception e) { this.estado = e.ToString();  }

                }
            }

            // Métodos GET para obtener los atributos de la instancia
            public List<Jugador> getJugadores() { return this.listaJugadores; }
            public List<char[]> getMatriz() { return this.matriz; }
            public int getTam() { return this.tam; }
            public string getEstado() { return this.estado; }
            
            // Métodos ESPECÍFICOS de la instancia
                // Se utiliza para limpiar el trazo de la pantalla, para volver a trazar con el camino actualizado en cada llamada
                public void resetParaTrazo() {
                    int cantX = this.limX / this.tam;
                    int cantY = this.limY / this.tam;
                        this.rellenarTrazo();
                        for (int i = 0; i < cantY; i++) {
                            char[] linea = this.matriz[i];
                            for (int j = 0; j < cantX; j++) { if (linea[j] == 'A') linea[j] = 'X'; }
                        }
                }
                
                // Se utiliza para cambiar el estado de los tiles que formen parte del camino.
                // TODO: Rellenar los tiles encerrados entre los tiles del camino
                public void rellenarTrazo() {
                    int cantX = this.limX / this.tam;
                    int cantY = this.limY / this.tam;

                    bool rellenar = false;
                    for (int i = 0; i < cantY; i++) {
                        char[] linea = this.matriz[i];
                        for (int j = 0; j < cantX; j++) {
                            if (this.listaJugadores[0].getX() / this.tam == j && this.listaJugadores[0].getY() / this.tam == i && linea[j] == 'C') {
                                rellenar = true;
                                break;
                            }
                        }
                    }

                    if (rellenar == true) {
                        for (int i = 0; i < cantY; i++) {
                            char[] linea = this.matriz[i];
                            for (int j = 0; j < cantX; j++) { if(linea[j] == 'A') linea[j] = 'C';  }
                        }
                        this.listaJugadores[0].getTrazo().setPath("X");
                        this.listaJugadores[0].getTrazo().setXInicial(this.listaJugadores[0].getX());
                        this.listaJugadores[0].getTrazo().setYInicial(this.listaJugadores[0].getY());
                    }
                }
                
                // Este método actualiza los estados de los tiles que forman parte del camino actual
                public void trazoATableros(Trazo trazo) {
                    string camino = trazo.getPath();
                    int x = trazo.getX() / this.tam;
                    int y = trazo.getY() / this.tam;

                    this.resetParaTrazo();
            
                    for(int i = 0; i < camino.Length; i++) {
                        switch (camino[i]) {
                            case 'W': y--;
                                break; 
                            case 'A': x--;
                                break; 
                            case 'S': y++;
                                break; 
                            case 'D': x++;
                                break;
                        }

                        if (x > this.cantX-1) x = this.cantX-1;
                        if (y > this.cantY-1) y = this.cantY-1;
                        if (x < 0) x = 0;
                        if (y < 0) y = 0;
                        if (this.matriz[y][x] == 'X') this.matriz[y][x] = 'A'; // Actualizar el estado si el camino pasa por tiles vacios.
                    }
                }
                
                // Agregar un nuevo Jugador en un tile no ocupado
                public void agregarJugador() { 
                    if(listaJugadores.Count == 0) {
                        bool colocado = false;
                        while(!colocado) {
                            Random r = new Random();
                            int posX = r.Next(0,this.cantX);
                            int posY = r.Next(0,this.cantY);
                            if(this.matriz[posY][posX] == 'C') {
                                listaJugadores.Add(new Jugador(posX * this.tam, posY * this.tam, this.tam));
                                this.matriz[posY][posX] = listaJugadores.Count.ToString()[0];
                                colocado = true;
                            }
                        }
                    }
                } 
    }
}