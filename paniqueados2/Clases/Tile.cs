using System;
using System.Collections.Generic;

namespace Clases { 

    public class Tile {
        int tam;
        int limX;
        int limY;
        int areaRevelada;
        int cantX;
        int cantY;
        List<char[]> matriz = new List<char[]>();
        List<Jugador> listaJugadores = new List<Jugador>();

        public Tile(int tam, int limX, int limY, int areaRevelada) {
            this.areaRevelada = areaRevelada;
            this.tam = tam;
            this.limX = limX;
            this.limY = limY;
            this.cantX = limX / tam;
            this.cantY = limY / tam;

            char[] linea = new char[cantX];
            
            int anchoInicial = -1;
            int altoInicial = -1;
            Random r = new Random();

            while(anchoInicial%2 != 0) {
                anchoInicial = r.Next(2, (areaRevelada/2));
            }
            
            
            altoInicial = areaRevelada/anchoInicial;

            int posicionInicialX = r.Next(0,this.cantX-anchoInicial);
            int posicionInicialY = r.Next(0,this.cantY-altoInicial);
            
            for (int i = 0; i < cantY; i++) {
                for (int j = 0; j < cantX; j++) {
                    if( ((j >= posicionInicialX) && (i >= posicionInicialY)) && ((j <= posicionInicialX + anchoInicial) && (i <= posicionInicialY + altoInicial)) ) {
                        linea[j] = 'C';
                    }
                    else {
                        linea[j] = 'X';
                    }
                }
                this.matriz.Add(linea);
                linea = new char[cantX];
            }

            this.agregarJugador();

        }

        public List<char[]> getMatriz() {
            return this.matriz;
        }

        public int getTam() {
            return this.tam;
        }

        public void resetParaTrazo() {
            
            int cantX = this.limX / this.tam;
            int cantY = this.limY / this.tam;

             this.rellenarTrazo();
             for (int i = 0; i < cantY; i++) {
                char[] linea = this.matriz[i];
                for (int j = 0; j < cantX; j++) {
                    if (linea[j] == 'A') linea[j] = 'X';
                }
             }
        }

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
                    for (int j = 0; j < cantX; j++) {
                        if(linea[j] == 'A') linea[j] = 'C';
                        }
                }
                
                this.listaJugadores[0].getTrazo().setPath("X");
                this.listaJugadores[0].getTrazo().setXInicial(this.listaJugadores[0].getX());
                this.listaJugadores[0].getTrazo().setYInicial(this.listaJugadores[0].getY());
            }
        }
        
        public void pintarTiles(char estado, int xIni, int y, int cant) {
            
            int colocadas = 0;
            while(colocadas < cant) {
                this.matriz[y][xIni + colocadas] = estado;
            }
        }

        public void trazoATiles(Trazo trazo) {
            string camino = trazo.getPath();
            int x = trazo.getX() / this.tam;
            int y = trazo.getY() / this.tam;

            this.resetParaTrazo();
            
            for(int i = 0; i < camino.Length; i++) {
                switch (camino[i]) {
                    case 'X':
                    break;

                    case 'W':
                        y--;
                    break;

                    case 'A':
                        x--;
                    break;

                    case 'S':
                        y++;
                    break;

                    case 'D':
                        x++;
                    break;
                }

                if(x > this.cantX-1) x = this.cantX-1;
                if(y > this.cantY-1) y = this.cantY-1;
                if(x < 0) x = 0;
                if(y < 0) y = 0;
                
                if (this.matriz[y][x] == 'X') this.matriz[y][x] = 'A';
                
            }
        }

        public void agregarJugador() {
            
            if(listaJugadores.Count == 0) {
                bool colocado = false;
                while(!colocado) {
                    Random r = new Random();
                    int posX = r.Next(0,this.cantX);
                    int posY = r.Next(0,this.cantY);
                    if(this.matriz[posY][posX] == 'C') {
                        this.matriz[posY][posX] = 'H';
                        listaJugadores.Add(new Jugador(new Trazo(this.tam), posX * this.tam, posY * this.tam, this.tam));
                        colocado = true;
                    }
                }
            }

           
        }

        public List<Jugador> getJugadores() {
            return this.listaJugadores;
        }
        }
}