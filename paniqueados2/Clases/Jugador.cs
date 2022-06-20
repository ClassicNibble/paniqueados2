using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Clases
{

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

        // Tama�o del cursor del jugador (es tanto largo como ancho)
        int tam;

        //// M�TODOS
        // Constructor
        public Jugador(int x, int y, int tam)
        {
            // Se inicializan atributos de la instancia
            this.trazo = new Trazo(tam);
            this.iniX = x;
            this.iniY = y;
            this.posX = x;
            this.posY = y;
            this.tam = tam;
        }

    public bool LimitMap(int LimitX,int LimitY)
        {
            bool res = false;

            if (this.getX() < 0)
            {
                res = true;
                this.setX(0);
            }
            else if (this.getX() >= LimitX)
            {
                res = true;
                this.setX(LimitX - this.getTam());
            }
            if (this.getY() < 0)
            {
                res = true;
                this.setY(0);
            }
            else if (this.getY() >= LimitY)
            {
                res = true;
                this.setY(LimitY - this.getTam());
            }

            return res;
        }
        public void update(Vector2 posicionPlayer,Tablero tablero,int LimitX,int LimitY)
        {
            posicionPlayer = new Vector2(this.getX(), this.getY());
 
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                this.setX(this.getX() + this.getTrazo().getTam());
                if (!LimitMap(LimitX,LimitY)) { this.getTrazo().nuevaDireccion("D"); }

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                this.setX(this.getX() - this.getTrazo().getTam());
                if (!LimitMap(LimitX,LimitY)) { this.getTrazo().nuevaDireccion("A"); }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                this.setY(this.getY() - this.getTrazo().getTam());
                if (!LimitMap(LimitX,LimitY)) { this.getTrazo().nuevaDireccion("W"); }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                this.setY(this.getY() + this.getTrazo().getTam());
                if (!LimitMap(LimitX,LimitY)) { this.getTrazo().nuevaDireccion("S"); }
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                char ultimaDir = this.getTrazo().backtrack();

                switch (ultimaDir)
                {
                    case 'W':
                        this.setY(this.getY() + this.getTrazo().getTam());
                        break;
                    case 'A':
                        this.setX(this.getX() + this.getTrazo().getTam());
                        break;
                    case 'S':
                        this.setY(this.getY() - this.getTrazo().getTam());
                        break;
                    case 'D':
                        this.setX(this.getX() - this.getTrazo().getTam());
                        break;
                }
            }




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

        // M�todos ESPEC�FICOS de la instancia
        // Reiniciar la posicion del Jugador a la inicial
        public void reiniciarPos() { this.posX = this.iniX; this.posY = this.iniY; }
    }
}