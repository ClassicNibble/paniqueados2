namespace Clases { 

    public class Jugador
    {
        Trazo trazo;
        int posX;
        int posY;
        int tamanio;

        public Jugador(Trazo t, int x, int y, int tam)
        {
            this.trazo = t;
            this.posX = x;
            this.posY = y;
            this.tamanio = tam;
        }

        public int getX()
        {
            return this.posX;
        }

        public int getY()
        {
            return this.posY;
        }

        public Trazo getTrazo()
        {
            return this.trazo;
        }

        public void setX(int x)
        {
            this.posX = x;
        }

        public void setY(int y)
        {
            this.posY = y;
        }


        public int getTam() {
            return this.tamanio;
        }

    }

}