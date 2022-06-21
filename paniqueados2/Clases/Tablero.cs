using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Clases
{
    // Clase Tablero, que representa un nivel en el que juega el Jugador
    public class Tablero
    {
        //// ATRIBUTOS
        // Tama�o de los tiles a utilizar
        int tam;

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
        // Multiplayer aun no implementado pero esto lo har� m�s sencillo
        List<Jugador> listaJugadores = new List<Jugador>();
        int numJugadores;

        // Estado representando errores
        string estado = "";

        //// M�TODOS
        // Constructor
        public Tablero(int tam, int limX, int limY, int areaRevelada, int numJug)
        {
            this.areaRevelada = areaRevelada;
            this.tam = tam;
            this.limX = limX;
            this.limY = limY;
            this.cantX = limX / tam;
            this.cantY = limY / tam;
            this.numJugadores = numJug;

            char[] linea = new char[cantX];

            Keys[] teclas = new Keys[5];
            for(int jColocar = 0; jColocar < numJug; jColocar++) {
                bool jColocado = false;
                if (jColocar == 0) teclas = new Keys[] {Keys.W, Keys.S, Keys.A, Keys.D, Keys.X};
                while (!jColocado)
                {
                    try
                    {
                        for (int i = 0; i < cantY; i++)
                        {
                            linea = new char[cantX];
                            for (int j = 0; j < cantX; j++)
                            {
                                if( ((j == 1 || j == cantX - 2) && (i>1 && i<= cantY-2 )) || ((j >= 1 && j <= cantX-2) && (i == 1 || i == cantY - 2))) linea[j] = 'C';
                                else linea[j] = 'X';
                            }
                            this.matriz.Add(linea);
                            
                        }

                        this.agregarJugador(teclas);
                        jColocado = true;
                    }
                    
                    catch (Exception e) { this.estado = e.ToString(); jColocado = true; }
                    }
                }
        }

        // M�todos GET para obtener los atributos de la instancia
        public List<Jugador> getJugadores() { return this.listaJugadores; }
        public List<char[]> getMatriz() { return this.matriz; }
        public int getTam() { return this.tam; }
        public string getEstado() { return this.estado; }

        // M�todos ESPEC�FICOS de la instancia
        // Se utiliza para limpiar el trazo de la pantalla, para volver a trazar con el camino actualizado en cada llamada
        public void resetParaTrazo()
        {
            int cantX = this.limX / this.tam;
            int cantY = this.limY / this.tam;
            this.rellenarTrazo();
            for (int i = 0; i < cantY; i++)
            {
                char[] linea = this.matriz[i];
                for (int j = 0; j < cantX; j++) { if (linea[j] == 'A') linea[j] = 'X'; }
            }
        }

        // Se utiliza para cambiar el estado de los tiles que formen parte del camino.
        // TODO: Rellenar los tiles encerrados entre los tiles del camino
        public void rellenarTrazo()
        {
            int cantX = this.limX / this.tam;
            int cantY = this.limY / this.tam;

            bool rellenar = false;

            for (int numJug = 0; numJug < listaJugadores.Count; numJug++) {
                for (int i = 0; i < cantY; i++)
                {
                    char[] linea = this.matriz[i];
                    for (int j = 0; j < cantX; j++)
                    {
                        if (this.listaJugadores[numJug].getX() / this.tam == j && this.listaJugadores[numJug].getY() / this.tam == i && linea[j] == 'C')
                        {
                            rellenar = true;
                            break;
                        }
                    }
                }

                if (rellenar == true && listaJugadores[numJug].getTrazo().getPath().Length > 2) {
                for (int i = 0; i < cantY; i++) {
                    char[] linea = this.matriz[i];
                    for (int j = 0; j < cantX; j++) { if (linea[j] == 'A') linea[j] = 'C'; }
                }
                this.listaJugadores[numJug].getTrazo().setPath("X");
                this.listaJugadores[numJug].getTrazo().setXInicial(this.listaJugadores[numJug].getX());
                this.listaJugadores[numJug].getTrazo().setYInicial(this.listaJugadores[numJug].getY());
                }

            }

            
        }

        // Este m�todo actualiza los estados de los tiles que forman parte del camino actual
        public void trazoATableros(Trazo trazo)
        {
            string camino = trazo.getPath();
            int x = trazo.getX() / this.tam;
            int y = trazo.getY() / this.tam;

            this.resetParaTrazo();

            for (int i = 0; i < camino.Length; i++)
            {
                switch (camino[i])
                {
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

                if (x > this.cantX - 1) x = this.cantX - 1;
                if (y > this.cantY - 1) y = this.cantY - 1;
                if (x < 0) x = 0;
                if (y < 0) y = 0;
                if (this.matriz[y][x] == 'X') this.matriz[y][x] = 'A'; // Actualizar el estado si el camino pasa por tiles vacios.
            }
        }


        // Agregar un nuevo Jugador en un tile no ocupado
        public void agregarJugador(Keys[] teclas)
        {
                bool colocado = false;
                while (!colocado)
                {
                    Random r = new Random();
                    int posX = r.Next(0, this.cantX);
                    int posY = r.Next(0, this.cantY);
                    if (this.matriz[posY][posX] == 'C')
                    {
                        listaJugadores.Add(new Jugador(posX * this.tam, posY * this.tam, this.tam, teclas));
                        this.matriz[posY][posX] = listaJugadores.Count.ToString()[0];
                        colocado = true;
                    }
                }
            
        }

        public void dibujarTablero(SpriteBatch _spriteBatch, Texture2D pixel) {
            this.trazoATableros(listaJugadores[0].getTrazo());
            listaJugadores[0].getTrazo().dibujarTrazo(_spriteBatch, pixel);

            for (int i = 0; i < matriz.Count; i++)
            {
                for (int j = 0; j < matriz[i].Length; j++)
                {
                   
                    if (this.matriz[i][j] == 'C') _spriteBatch.Draw(pixel, new Rectangle(j * this.tam, i * this.tam, this.tam, this.tam), Color.Blue);                    
                    if (this.matriz[i][j] == '1') _spriteBatch.Draw(pixel, new Rectangle(j * this.tam, i * this.tam, this.tam, this.tam), Color.Green);
                    if (this.matriz[i][j] == '2')  _spriteBatch.Draw(pixel, new Rectangle(j * this.tam, i * this.tam, this.tam, this.tam), Color.Yellow);
                    if (this.matriz[i][j] == '3')  _spriteBatch.Draw(pixel, new Rectangle(j * this.tam, i * this.tam, this.tam, this.tam), Color.Purple);;
                    

                }
            }



        }
    }
}